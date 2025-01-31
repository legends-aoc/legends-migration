using Chaos.Common.Abstractions;
using Chaos.Common.Utilities;

namespace Chaos.Services.Storage.Options;

public sealed record UserSaveManagerOptions : IDirectoryBound
{
    public required string BackupDirectory { get; set; }
    public int BackupIntervalMins { get; set; }
    public int BackupRetentionDays { get; set; }

    public required string Directory { get; set; }

    /// <inheritdoc />
    public void UseBaseDirectory(string baseDirectory)
    {
        Directory = Path.Combine(baseDirectory, Directory);
        BackupDirectory = Path.Combine(baseDirectory, BackupDirectory);

        if (PathEx.IsSubPathOf(BackupDirectory, Directory))
            throw new InvalidOperationException($"{nameof(BackupDirectory)} cannot be a subdirectory of {nameof(Directory)}");
    }
}