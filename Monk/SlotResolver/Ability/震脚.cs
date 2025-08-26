using AEAssist;
using AEAssist.MemoryApi;
using Xise.Monk.SlotResolver.Data;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Xise.Common;


namespace Xise.Monk.SlotResolver.Ability;

public class 震脚 : ISlotResolver
{
    public int Check()
    {
        if (Core.Me.Level < 40) return -1;
        if (Core.Me.HasAura(Buffs.震脚) || Core.Me.HasAura(Buffs.无相身形)) return -1;
        if (Helper.MemApiSpell.CheckActionChange(Spells.必杀技) != Spells.必杀技) return -1;

        // bool 震脚可用 = Spells.震脚.GetSpell().IsReadyWithCanCast();
        // int 震脚剩余cd = Helper.MemApiSpell.GetCooldown(Spells.震脚).Seconds;
        int 红莲剩余cd = Helper.MemApiSpell.GetCooldown(Spells.红莲极意).Seconds;
        int 桃园剩余cd = Helper.MemApiSpell.GetCooldown(Spells.义结金兰).Seconds;


        // 在爆发前开
        if (红莲剩余cd < 3 ||
            桃园剩余cd < 3)
        {
            return 0;
        }

        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.震脚.GetSpell());
    }
}