using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Xise.Common;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Ability;

public class 团辅 : ISlotResolver
{
    public int Check()
    {
        
        if (Core.Me.Level < 68) return -1;
        if (!Spells.红莲极意.GetSpell().IsReadyWithCanCast() && !Spells.义结金兰.GetSpell().IsReadyWithCanCast()) return -1;
        
        
        if (Core.Me.HasAura(Buffs.震脚)) return 0;
        if (Helper.In团辅()) return 0;
        
        // int 震脚cd = Helper.MemApiSpell.GetCooldown(Spells.震脚).Seconds;
        // if (震脚cd > 8) return 0;
        
        return 0;
    }

    public void Build(Slot slot)
    {

        if (Spells.红莲极意.GetSpell().IsReadyWithCanCast()) slot.Add(Spells.红莲极意.GetSpell());
        if (Spells.义结金兰.GetSpell().IsReadyWithCanCast()) slot.Add(Spells.义结金兰.GetSpell());
    }
}