namespace Pamx.Neo;

/// <summary>
/// The type of sound the background object should react to.
/// </summary>
public enum BackgroundObjectReactivityType
{
    /// <summary>
    /// No reaction.
    /// </summary>
    None,

    /// <summary>
    /// React to low frequencies.
    /// </summary>
    Bass,

    /// <summary>
    /// React to mid frequencies.
    /// </summary>
    Mid,

    /// <summary>
    /// React to high frequencies.
    /// </summary>
    Treble
}