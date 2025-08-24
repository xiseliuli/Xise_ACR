using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class Base : ISlotResolver
{
    private static bool 猛豹身形 => Core.Me.HasAura(Buffs.猛豹身形);
    private static bool 盗龙身形 => Core.Me.HasAura(Buffs.盗龙身形);
    private static bool 战斗 => new MemApiCondition().IsInCombat();


    public int Check()
    {
        return 0;
    }

    public void Build(Slot slot)
    {
        var mnk = Core.Resolve<JobApi_Monk>();
        if (mnk.Chakra >= 5)
        {
            var enemyCount = TargetHelper.GetNearbyEnemyCount(10);
            if (enemyCount >= 3)
            {
                // 使用AOE技能
                slot.Add(Spells.万象斗气圈adaptive.GetSpell());
            }

            slot.Add(Spells.阴阳斗气斩adaptive.GetSpell());
            // 使用斗气技能
        }

        if (猛豹身形)
        {
            slot.Add(mnk.CoeurlFury > 0 ? Spells.崩拳adaptive.GetSpell() : Spells.破碎拳.GetSpell());
        }
        else if (盗龙身形)
        {
            slot.Add(mnk.RaptorFury > 0 ? Spells.正拳adaptive.GetSpell() : Spells.双掌打.GetSpell());
        }
        else
        {
            slot.Add(mnk.OpoOpoFury > 0 ? Spells.连击adaptive.GetSpell() : Spells.双龙脚.GetSpell());
        }
    }
}