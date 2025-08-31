using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class 绝空拳 : ISlotResolver
{
    public int Check()
    {
        if (!Core.Me.HasAura(Buffs.绝空拳预备)) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.绝空拳.GetSpell());
    }
}