using System;

namespace Genesis.Shared.Constant
{
    [Flags]
    public enum UnkFlags : uint
    {
        AvatarStatusRendered = 0x000001,
        PossibleMissionItem = 0x000004,
        IsCorpse = 0x000008,
        UnkClean = 0x000010,
        IsInvincible = 0x000400,
        UnkEnabled = 0x002000,
        UnkSound = 0x010000, // sound initialized?
        Dirty = 0x020000,
        IsKit = 0x080000,
        IsBound = 0x100000,
    }
}
