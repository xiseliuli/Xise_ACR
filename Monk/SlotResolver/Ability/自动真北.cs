using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Xise.Common;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.Data;
using Xise.Monk.SlotResolver.GCD;

namespace Xise.Monk.SlotResolver.Ability;

public class 自动真北 : ISlotResolver
{
    public int Check()
    {
        if (MonkRotationEventHandler.IsStopped || MonkRotationEventHandler.IsInvul)
            return -99;
        if (GCDHelper.GetGCDCooldown() < 600)
            return -1;
        if (!Qt.Instance.GetQt("自动真北"))
            return -2;
        if (!Spells.真北.GetSpell().IsReadyWithCanCast())
            return -3;
        if (Core.Me.HasAura(Buffs.真北))
            return -4;
        uint spellId = Core.Me.HasAura(Buffs.震脚) ? 震脚GCD.GetSpellId() : BaseGCD.GetSpellId();
        if (spellId != Spells.崩拳adaptive && spellId != Spells.破碎拳) return -4;
        if ((spellId == Spells.崩拳adaptive && Helper.IsFlanking) ||
            (spellId == Spells.破碎拳 && Helper.IsBehind)) return -5;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.真北.GetSpell());
    }
}