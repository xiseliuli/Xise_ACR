using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Xise.Monk.SlotResolver.Data;
using Xise.Monk.QtUI;

namespace Xise.Monk.SlotResolver.Ability;

public class 斗气 : ISlotResolver
{
    public int Check()
    {
        // 检查等级和斗气量要求
        if (Core.Me?.Level < 40 || MonkHelper.MonkApi?.Chakra < 5) return -1;
        
        // 检查目标是否可攻击
        var target = Core.Me?.GetCurrTarget();
        if (target?.CanAttack() != true) return -1;
        
        // 检查QT设置
        if (Qt.Instance?.GetQt("攒功力") != true) return -1;
        
        return 0;
    }

    public void Build(Slot slot)
    {
        // 获取附近敌人数量
        var enemyCount = TargetHelper.GetNearbyEnemyCount(10);
        
        // 根据敌人数量选择技能
        if (enemyCount >= 3)
        {
            // 使用AOE技能 - 万象斗气圈
            slot.Add(Spells.万象斗气圈adaptive.GetSpell());
        }
        else
        {
            // 使用单体技能 - 阴阳斗气斩
            slot.Add(Spells.阴阳斗气斩adaptive.GetSpell());
        }
    }
}