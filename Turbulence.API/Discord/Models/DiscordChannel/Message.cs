using Turbulence.API.Discord.Models.DiscordApplication;
// using Turbulence.API.Discord.Models.DiscordMessageComponents;
using Turbulence.API.Discord.Models.DiscordReceivingAndResponding;
using Turbulence.API.Discord.Models.DiscordSticker;
using Turbulence.API.Discord.Models.DiscordUser;
using System.Text.Json.Serialization;
using Turbulence.API.Discord.Models.DiscordGuild;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// Represents a message sent in a channel within Discord.
/// 
/// See the <a href="https://discord.com/developers/docs/resources/channel#message-object">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#message-object">GitHub
/// </a>.
/// </summary>
public record Message {
	/// <summary>
	/// Snowflake ID of the message.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// Snowflake ID of the channel the message was sent in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Snowflake ChannelId { get; init; }

	/// <summary>
	/// The author of this message.
	///
	/// The author object follows the structure of the user object, but is only a valid user in the case where the
	/// message is generated by a user or bot user. If the message is generated by a webhook, the author object
	/// corresponds to the webhook's snowflake ID, username, and avatar. You can tell if a message is generated by a
	/// webhook by checking for the <see cref="WebhookId" /> on the message object.
	/// </summary>
	[JsonPropertyName("author")]
	public required User Author { get; init; }

	/// <summary>
	/// Contents of the message.
	/// </summary>
	[JsonPropertyName("content")]
	public required string Content { get; init; }

	/// <summary>
	/// When this message was sent.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public required DateTimeOffset Timestamp { get; init; }

	/// <summary>
	/// When this message was edited (or null if never).
	/// </summary>
	[JsonPropertyName("edited_timestamp")]
	public required DateTimeOffset? EditedTimestamp { get; init; }

	/// <summary>
	/// Whether this was a TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	public required bool Tts { get; init; }

	/// <summary>
	/// Whether this message mentions everyone.
	/// </summary>
	[JsonPropertyName("mention_everyone")]
	public required bool MentionEveryone { get; init; }

	/// <summary>
	/// Users specifically mentioned in the message.
	/// </summary>
	[JsonPropertyName("mentions")]
	public required User[] Mentions { get; init; }

	/// <summary>
	/// Roles specifically mentioned in this message.
	/// </summary>
	[JsonPropertyName("mention_roles")]
	public required Snowflake[] MentionRoles { get; init; }

	/// <summary>
	/// Channels specifically mentioned in this message.
	///
	/// Not all channel mentions in a message will appear in here. Only textual channels that are visible to everyone in
	/// a lurkable guild will ever be included. Only crossposted messages (via Channel Following) currently include this
	/// at all. If no mentions in the message meet these requirements, this field will not be sent.
	/// </summary>
	[JsonPropertyName("mention_channels")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ChannelMention[]? MentionChannels { get; init; }

	/// <summary>
	/// Any attached files.
	/// </summary>
	[JsonPropertyName("attachments")]
	public required Attachment[] Attachments { get; init; }

	/// <summary>
	/// Any embedded content.
	/// </summary>
	[JsonPropertyName("embeds")]
	public required Embed[] Embeds { get; init; }

	/// <summary>
	/// Reactions to the message.
	/// </summary>
	[JsonPropertyName("reactions")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Reaction[]? Reactions { get; init; }

	// TODO: Deserialize this into an actual type
	/// <summary>
	/// Used for validating a message was sent.
	/// </summary>
	[JsonPropertyName("nonce")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public /* integer or string */ dynamic? Nonce { get; init; }

	/// <summary>
	/// Whether this message is pinned.
	/// </summary>
	[JsonPropertyName("pinned")]
	public required bool Pinned { get; init; }

	/// <summary>
	/// If the message is generated by a webhook, this is the webhook's snowflake ID.
	/// </summary>
	[JsonPropertyName("webhook_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake? WebhookId { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#message-object-message-types">Type of message</a>.
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; } // TODO: Make enum

	/// <summary>
	/// Sent with Rich Presence-related chat embeds.
	/// </summary>
	[JsonPropertyName("activity")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public MessageActivity? Activity { get; init; }

	/// <summary>
	/// Sent with Rich Presence-related chat embeds.
	/// </summary>
	[JsonPropertyName("application")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Application? Application { get; init; }

	/// <summary>
	/// If the message is an
	/// <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object">
	/// Interaction</a> or application-owned webhook, this is the snowflake ID of the application.
	/// </summary>
	[JsonPropertyName("application_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake? ApplicationId { get; init; }

	/// <summary>
	/// Data showing the source of a crosspost, channel follow add, pin, or reply message.
	/// </summary>
	[JsonPropertyName("message_reference")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public MessageReference? MessageReference { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#message-object-message-flags">Message flags</a>
	/// combined as a <a href="https://en.wikipedia.org/wiki/Bit_field">bitfield</a>.
	/// </summary>
	[JsonPropertyName("flags")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? Flags { get; init; }

	/// <summary>
	/// The message associated with the <see cref="MessageReference"/>.
	///
	/// This field is only returned for messages with a type of <c>19 (REPLY)</c> or <c>21 (THREAD_STARTER_MESSAGE)</c>.
	/// If the message is a reply but this field is not present, the backend did not attempt to fetch the message that
	/// was being replied to, so its state is unknown. If the field exists but is null, the referenced message was
	/// deleted.
	/// </summary>
	[JsonPropertyName("referenced_message")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Message? ReferencedMessage { get; init; }

	/// <summary>
	/// Sent if the message is a response to an
	/// <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object">
	/// Interaction</a>.
	/// </summary>
	[JsonPropertyName("interaction")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public MessageInteraction? Interaction { get; init; }

	/// <summary>
	/// The thread that was started from this message, includes
	/// <a href="https://discord.com/developers/docs/resources/channel#thread-member-object">thread member</a> object.
	/// </summary>
	[JsonPropertyName("thread")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Channel? Thread { get; init; }

	/// <summary>
	/// Sent if the message contains components like buttons, action rows, or other interactive components.
	/// </summary>
	[JsonPropertyName("components")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public /*MessageComponent[]*/ dynamic? Components { get; init; } // TODO: Implement

	/// <summary>
	/// Sent if the message contains stickers.
	/// </summary>
	[JsonPropertyName("sticker_items")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public StickerItem[]? StickerItems { get; init; }

	/// <summary>
	/// The stickers sent with the message.
	/// </summary>
	[JsonPropertyName("stickers")]
	[Obsolete("Use StickerItems")]
	public Sticker[]? Stickers { get; init; }

	// TODO: Set <see cref=""> for total_message_sent
	/// <summary>
	/// A generally increasing integer (there may be gaps or duplicates) that represents the approximate position of the
	/// message in a thread, it can be used to estimate the relative position of the message in a thread in company with
	/// <c>total_message_sent</c> on parent thread.
	/// </summary>
	[JsonPropertyName("position")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? Position { get; init; }

	/// <summary>
	/// Data of the role subscription purchase or renewal that prompted this <c>ROLE_SUBSCRIPTION_PURCHASE</c> message.
	/// </summary>
	[JsonPropertyName("role_subscription_data")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public RoleSubscriptionData? RoleSubscriptionData { get; init; }
	
	#region CREATE_MESSAGE Extra fields
	// Maybe refactor this into a separate class or into event args
	
	/// <summary>
	/// ID of the guild the message was sent in, unless it is an ephemeral message.
	///
	/// Only available if this message was retrieved from a <c>MESSAGE_CREATED</c> or <c>MESSAGE_UPDATED</c> event.
	/// </summary>
	[JsonPropertyName("guild_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake? GuildId { get; init; }

	/// <summary>
	/// Member properties for this message's author. Missing for ephemeral messages and messages from webhooks.
	///
	/// Only available if this message was retrieved from a <c>MESSAGE_CREATED</c> or <c>MESSAGE_UPDATED</c> event.
	/// </summary>
	[JsonPropertyName("member")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public GuildMember? Member { get; init; }

	#endregion
}

/// <summary>
/// The type of the message.
///
/// See the
/// <a href="https://discord.com/developers/docs/resources/channel#message-object-message-types">Discord API
/// documentation</a>.
/// </summary>
public enum MessageType
{
	DEFAULT = 0,
	RECIPIENT_ADD = 1,
	RECIPIENT_REMOVE = 2,
	CALL = 3,
	CHANNEL_NAME_CHANGE = 4,
	CHANNEL_ICON_CHANGE = 5,
	CHANNEL_PINNED_MESSAGE = 6,
	USER_JOIN = 7,
	GUILD_BOOST = 8,
	GUILD_BOOST_TIER_1 = 9,
	GUILD_BOOST_TIER_2 = 10,
	GUILD_BOOST_TIER_3 = 11,
	CHANNEL_FOLLOW_ADD = 12,
	GUILD_DISCOVERY_DISQUALIFIED = 14,
	GUILD_DISCOVERY_REQUALIFIED = 15,
	GUILD_DISCOVERY_GRACE_PERIOD_INITIAL_WARNING = 16,
	GUILD_DISCOVERY_GRACE_PERIOD_FINAL_WARNING = 17,
	THREAD_CREATED = 18,
	REPLY = 19,
	CHAT_INPUT_COMMAND = 20,
	THREAD_STARTER_MESSAGE = 21,
	GUILD_INVITE_REMINDER = 22,
	CONTEXT_MENU_COMMAND = 23,
	AUTO_MODERATION_ACTION = 24,
	ROLE_SUBSCRIPTION_PURCHASE = 25,
	INTERACTION_PREMIUM_UPSELL = 26,
	STAGE_START = 27,
	STAGE_END = 28,
	STAGE_SPEAKER = 29,
	STAGE_TOPIC = 31,
	GUILD_APPLICATION_PREMIUM_SUBSCRIPTION = 32,
}