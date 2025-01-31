namespace Chaos.Scripting.Abstractions;

/// <summary>
///     Represents a <see cref="Chaos.Scripting.Abstractions.IScript" /> intended to be used for a specific object
/// </summary>
/// <typeparam name="T">The <see cref="Chaos.Scripting.Abstractions.IScripted" /> object this script is attached to</typeparam>
public abstract class SubjectiveScriptBase<T> : ScriptBase where T: IScripted
{
    /// <summary>
    ///     The object this script is attached to
    /// </summary>
    public T Subject { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SubjectiveScriptBase{T}" /> class.
    /// </summary>
    /// <param name="subject">The subject of this script. The object the script is attached to</param>
    protected SubjectiveScriptBase(T subject) => Subject = subject;
}