using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class 震脚GCD : ISlotResolver
{
    public int Check()
    {
        if (MonkRotationEventHandler.IsStopped || MonkRotationEventHandler.IsInvul) return -1;
        if (Core.Me.HasAura(Buffs.震脚)) return -2;
        if (Qt.Instance.GetQt("AOE攒震脚") &&
            GameObjectExtension.Distance(Core.Me, GameObjectExtension.GetCurrTarget(Core.Me)) >
            MonkSettings.Instance.AttackRange
           ) return -2;
        return 0;
    }

    public void Build(Slot slot)
    {
        BaseGCD.CheckAOE();
    }
}