using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Turbulence.Discord.Models.DiscordChannel;
using static Turbulence.Discord.Models.DiscordChannel.MessageType;

namespace Turbulence.Desktop.Views.Main;

public partial class MessageView : UserControl
{
    public MessageView(Message message)
    {
        InitializeComponent();

        // TODO: Profile picture
        
        Author.Text = message.GetBestAuthorName();

        if (message.Author.Avatar is { } avatar)
        {
            Image.Source = Task.Run(async () =>
                await LoadFromWeb(new Uri($"https://cdn.discordapp.com/avatars/{message.Author.Id}/{avatar}.png?size=80"))).Result;
        }
        else
        {
            Image.Source = Task.Run(async () =>
                await LoadFromWeb(new Uri($"https://cdn.discordapp.com/embed/avatars/{(message.Author.Id >> 22) % 6}.png"))).Result!.CreateScaledBitmap(new PixelSize(80, 80));
        }
        
        
        // TODO: make timestamp relative?
        var localTime = message.Timestamp.ToLocalTime();
        Timestamp.Text = localTime.ToString("G");
        Timestamp.SetValue(ToolTip.TipProperty, localTime.ToString("F"));

        Content.Text = message.Type switch
        {
            THREAD_CREATED => $"{Author.Text} created thread \"{message.Content}\"",
            CALL => $"{Author.Text} started a voice call",
            _ => message.Content,
        };
    }

    private static async Task<Bitmap?> LoadFromWeb(Uri url)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsByteArrayAsync();
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
            return null;
        }
    }
}