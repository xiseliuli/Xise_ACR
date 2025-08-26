using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Xise.Common;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class Base : ISlotResolver
{
    private static bool 猛豹身形 => Core.Me.HasAura(Buffs.猛豹身形);
    private static bool 盗龙身形 => Core.Me.HasAura(Buffs.盗龙身形);
    private static bool 无相身形 => Core.Me.HasAura(Buffs.无相身形);
    private static bool Is战斗 => Helper.MemApi.IsInCombat();


    public int Check()
    {
        return 0;
    }

    public void Build(Slot slot)
    {
        uint 必杀技 = Helper.MemApiSpell.CheckActionChange(Spells.必杀技);

        if (必杀技 != Spells.必杀技)
        {
            if (Helper.In团辅())
            {
                slot.Add(必杀技.GetSpell());
                return;
            }

            int 红莲剩余cd = Helper.MemApiSpell.GetCooldown(Spells.红莲极意).Seconds;
            if (红莲剩余cd > 8)
            {
                slot.Add(必杀技.GetSpell());
                return;
            }
        }


        if (猛豹身形)
        {
            slot.Add(MonkHelper.MonkApi.CoeurlFury > 0 ? Spells.崩拳adaptive.GetSpell() : Spells.破碎拳.GetSpell());
        }
        else if (盗龙身形)
        {
            slot.Add(MonkHelper.MonkApi.RaptorFury > 0 ? Spells.正拳adaptive.GetSpell() : Spells.双掌打.GetSpell());
        }
        else
        {
            slot.Add(MonkHelper.MonkApi.OpoOpoFury > 0 ? Spells.连击adaptive.GetSpell() : Spells.双龙脚.GetSpell());
        }
    }
}