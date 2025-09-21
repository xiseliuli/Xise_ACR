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
    private static readonly JobApi_Monk MonkApi = Core.Resolve<JobApi_Monk>();


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
        if (Qt.Instance.GetQt("下一个打阴"))
            return SolarSpellId();
        if (Qt.Instance.GetQt("下一个打阳"))
            return LunarSpellId();

        LogHelper.Print($"{MonkRotationEventHandler.FinishingMoveCount}");
        // 双阴起手
        if (Qt.Instance.GetQt("双阴起手") && MonkRotationEventHandler.FinishingMoveCount < 2)
            return SolarSpellId();
        LogHelper.Print($"Solar {MonkApi.Nadi.HasFlag(Nadi.Solar)}");
        LogHelper.Print($"Lunar {MonkApi.Nadi.HasFlag(Nadi.Lunar)}");
        if (!MonkApi.Nadi.HasFlag(Nadi.Solar))
            return SolarSpellId();
        if (!MonkApi.Nadi.HasFlag(Nadi.Lunar))
            return LunarSpellId();

        return SolarSpellId();
    }


    public static uint SolarSpellId()
    {
        if (Qt.Instance.GetQt("AOE攒震脚") && BaseGCD.isAOEEnabled)
        {
            return Spells.破坏神脚adaptive;
        }

        return MonkApi?.CoeurlFury > 0 ? Spells.崩拳adaptive : Spells.破碎拳;
    }

    public static uint LunarSpellId()
    {
        if (Qt.Instance.GetQt("AOE攒震脚") && BaseGCD.isAOEEnabled)
        {
            if (!MonkApi.BeastChakra.Contains(BeastChakra.Coeurl))
                return Spells.地烈劲;


            if (!MonkApi.BeastChakra.Contains(BeastChakra.Raptor))
                return Spells.四面脚;

            // if (!MonkApi.BeastChakra.Contains(BeastChakra.OpoOpo))
            return Spells.破坏神脚adaptive;
        }

        if (!MonkApi.BeastChakra.Contains(BeastChakra.Coeurl))
        {
            return MonkApi?.CoeurlFury > 0 ? Spells.崩拳adaptive : Spells.破碎拳;
        }

        if (!MonkApi.BeastChakra.Contains(BeastChakra.Raptor))
        {
            return MonkApi?.RaptorFury > 0 ? Spells.正拳adaptive : Spells.双掌打;
        }

        if (!MonkApi.BeastChakra.Contains(BeastChakra.OpoOpo))
        {
            return MonkApi?.OpoOpoFury > 0 ? Spells.连击adaptive : Spells.双龙脚;
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        BaseGCD.CheckAOE();
        slot.Add(GetSpellId().GetSpell());
    }
}