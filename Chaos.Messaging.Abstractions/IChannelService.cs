using Chaos.Common.Definitions;

namespace Chaos.Messaging.Abstractions;

/// <summary>
///     Defines the contract for an object that provides channel-based messaging services
/// </summary>
public interface IChannelService
{
    /// <summary>
    ///     Determines whether or not the given channel exists
    /// </summary>
    /// <param name="channelName">The channel name to check</param>
    /// <returns><c>true</c> if a channel with the given name exists, otherwise <c>false</c></returns>
    bool ContainsChannel(string channelName);

    /// <summary>
    ///     Gets all members of a channel
    /// </summary>
    /// <param name="channelName"></param>
    /// <returns></returns>
    IEnumerable<IChannelSubscriber> GetSubscribers(string channelName);

    /// <summary>
    ///     Determines whether or not the given string is a valid channel name
    /// </summary>
    /// <param name="str">The string to check</param>
    /// <returns><c>true</c> if the given string is in the correct format to be a channel name, otherwise <c>false</c></returns>
    bool IsChannel(string str);

    /// <summary>
    ///     Joins a channel, allowing you to send and receive messages to/from it
    /// </summary>
    /// <param name="obj">The object being added to the channel</param>
    /// <param name="channelName">The name of the channel</param>
    void JoinChannel(IChannelSubscriber obj, string channelName);

    /// <summary>
    ///     Leaves a channel, preventing you from sending and receiving messages to/from it
    /// </summary>
    /// <param name="obj">The object leaving the channel</param>
    /// <param name="channelName">The name of the channel</param>
    void LeaveChannel(IChannelSubscriber obj, string channelName);

    /// <summary>
    ///     Registers a channel, allowing you to send and receive messages to/from it
    /// </summary>
    /// <param name="obj">If a subscriber registered the channel, this will be that subscriber</param>
    /// <param name="channelName">The name of the channel</param>
    /// <param name="defaultMessageColor">The color to use for the given channel</param>
    /// <param name="bypassValidation">
    ///     Whether or not to bypass the phrase filter. Use this to add channels with names that you have added to the
    ///     phrase blacklist for reservation purposes
    /// </param>
    /// <param name="channelNameOverride">The name that will be displayed when sending and receiving from this channel (INTERNAL ONLY)</param>
    /// <param name="messageType">The message type to use for the registered channel</param>
    void RegisterChannel(
        IChannelSubscriber? obj,
        string channelName,
        MessageColor defaultMessageColor,
        bool bypassValidation = false,
        string? channelNameOverride = null,
        ServerMessageType messageType = ServerMessageType.ActiveMessage
    );

    /// <summary>
    ///     Sends a message to a channel
    /// </summary>
    /// <param name="obj">The obj sending the message</param>
    /// <param name="channelName">The name of the channel to send to</param>
    /// <param name="message">The message to send to the channel</param>
    void SendMessage(IChannelSubscriber obj, string channelName, string message);

    /// <summary>
    ///     Sets the text color of a channel
    /// </summary>
    /// <param name="obj">The subscriber to set the channel color for</param>
    /// <param name="channelName">The name of the channel</param>
    /// <param name="messageColor">The color to use for the given channel</param>
    void SetChannelColor(IChannelSubscriber obj, string channelName, MessageColor messageColor);

    /// <summary>
    ///     Unregisters a channel, preventing anyone from sending and receiving messages to/from it
    /// </summary>
    /// <param name="channelName">The name of the channel</param>
    bool UnregisterChannel(string channelName);
}