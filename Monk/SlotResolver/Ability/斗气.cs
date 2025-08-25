using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Ability;

public class 斗气 : ISlotResolver
{
    public int Check()
    {
        return Core.Me.Level < 40 ? -1 : 0;
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
    }
}