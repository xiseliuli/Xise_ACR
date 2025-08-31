using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class 乾坤斗气弹 : ISlotResolver
{
    public int Check()
    {
        if (
            !Core.Me.HasAura(Buffs.乾坤斗气弹预备) ||
            Core.Me.HasAura(Buffs.震脚) ||
            Core.Me.HasAura(Buffs.无相身形)
        ) return -1;


        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.乾坤斗气弹.GetSpell());
    }
}