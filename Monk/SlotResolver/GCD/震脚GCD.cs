using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class 震脚GCD : ISlotResolver
{
    private static readonly JobApi_Monk _monkApi = Core.Resolve<JobApi_Monk>();


    public int Check()
    {
        if (MonkRotationEventHandler.IsStopped || MonkRotationEventHandler.IsInvul) return -1;
        if (!Core.Me.HasAura(Buffs.震脚)) return -2;
        if (Qt.Instance.GetQt("AOE攒震脚") &&
            GameObjectExtension.Distance(Core.Me, GameObjectExtension.GetCurrTarget(Core.Me)) >
            MonkSettings.Instance.AttackRange
           ) return -2;
        return 0;
    }


    public static uint GetSpellId()
    {
        if (Qt.Instance.GetQt("下一个打阳"))
        {
            if (Qt.Instance.GetQt("AOE攒震脚") && BaseGCD.isAOEEnabled)
            {
                if (!_monkApi.BeastChakra.Contains(BeastChakra.Coeurl))
                {
                    return Spells.地烈劲;
                }

                if (!_monkApi.BeastChakra.Contains(BeastChakra.Raptor))
                {
                    return Spells.四面脚;
                }

                if (!_monkApi.BeastChakra.Contains(BeastChakra.OpoOpo))
                {
                    return Spells.破坏神脚adaptive;
                }
            }

            if (!_monkApi.BeastChakra.Contains(BeastChakra.Coeurl))
            {
                return _monkApi?.CoeurlFury > 0 ? Spells.崩拳adaptive : Spells.破碎拳;
            }

            if (!_monkApi.BeastChakra.Contains(BeastChakra.Raptor))
            {
                return _monkApi?.RaptorFury > 0 ? Spells.正拳adaptive : Spells.双掌打;
            }

            if (!_monkApi.BeastChakra.Contains(BeastChakra.OpoOpo))
            {
                return _monkApi?.OpoOpoFury > 0 ? Spells.连击adaptive : Spells.双龙脚;
            }
        }

        if (Qt.Instance.GetQt("AOE攒震脚") && BaseGCD.isAOEEnabled)
        {
            return Spells.破坏神脚adaptive;
        }

        return _monkApi?.CoeurlFury > 0 ? Spells.崩拳adaptive : Spells.破碎拳;
    }

    public void Build(Slot slot)
    {
        BaseGCD.CheckAOE();
        slot.Add(GetSpellId().GetSpell());
    }
}