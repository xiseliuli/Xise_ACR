using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons;
using Xise.Common;
using Xise.Monk.SlotResolver.Data;
using Xise.Monk.QtUI;

namespace Xise.Monk.SlotResolver.GCD;

public class BaseGCD : ISlotResolver
{
    private static readonly JobApi_Monk _monkApi = Core.Resolve<JobApi_Monk>();
    
    public static bool isAOEEnabled;

    private static int nearbyEnemies;

    public static bool 猛豹身形 => Core.Me?.HasAura(Buffs.猛豹身形) == true;
    public static bool 盗龙身形 => Core.Me?.HasAura(Buffs.盗龙身形) == true;
    private static bool 无相身形 => Core.Me?.HasAura(Buffs.无相身形) == true;
    private bool 震脚状态 => Core.Me?.HasAura(Buffs.震脚) == true;
    private bool Is战斗 => Helper.MemApi?.IsInCombat() == true;


    public int Check()
    {
        // 检查目标距离
        var target = Core.Me?.GetCurrTarget();
        if (target == null) return -2;

        // 检查是否在攻击范围内
        if (!Helper.WithinAttackRange) return -3;

        return 0;
    }


    public static void CheckAOE()
    {
        // 获取AOE设置状态
        isAOEEnabled = Qt.Instance?.GetQt("AOE") ?? true;
        // 检测附近敌人数量（5码范围内）
        nearbyEnemies = TargetHelper.GetNearbyEnemyCount((IBattleChara)Core.Me, 5, 5);
    }

    public static uint GetSpellId()
    {
        // 检查最终爆发模式
        if (Qt.Instance?.GetQt("最终爆发") == true)
        {
            // 最终爆发模式：优先使用斗气技能
            if (_monkApi?.CoeurlFury > 0 && 猛豹身形)
            {
                return Spells.崩拳adaptive;
            }

            if (_monkApi?.RaptorFury > 0 && 盗龙身形)
            {
                return Spells.正拳adaptive;
            }

            if (_monkApi?.OpoOpoFury > 0)
            {
                return Spells.连击adaptive;
            }
        }


        // 检查AOE模式
        if (isAOEEnabled && nearbyEnemies > 3)
        {
            // AOE模式：根据身形选择AOE技能
            if (无相身形)
            {
                // 无相身形AOE
                if (Core.Me?.Level >= 82)
                {
                    return Spells.破坏神脚;
                }
                else if (Core.Me?.Level >= 52)
                {
                    return Spells.地烈劲;
                }
            }
            else if (猛豹身形 && Core.Me?.Level >= 30)
            {
                // 猛豹身形AOE
                return Spells.地烈劲;
            }
            else if (盗龙身形 && Core.Me?.Level >= 18)
            {
                // 盗龙身形AOE
                return Spells.四面脚;
            }
            else if (Core.Me?.Level >= 26)
            {
                // 默认身形AOE
                return Spells.破坏神冲;
            }
        }

        // 检查攒功力模式
        if (Qt.Instance?.GetQt("攒功力") == true)
        {
            // 攒功力模式：优先使用基础技能积累斗气
            if (猛豹身形)
            {
                return Spells.破碎拳;
            }

            if (盗龙身形)
            {
                return Spells.双掌打;
            }

            return Spells.双龙脚;
        }

        // 根据身形状态选择技能
        if (猛豹身形)
        {
            // 猛豹身形：根据斗气量选择技能
            return _monkApi?.CoeurlFury > 0 ? Spells.崩拳adaptive : Spells.破碎拳;
        }
        else if (盗龙身形)
        {
            // 盗龙身形：根据斗气量选择技能
            return _monkApi?.RaptorFury > 0 ? Spells.正拳adaptive : Spells.双掌打;
        }
        else if (无相身形)
        {
            // 无相身形：根据斗气量选择技能
            return _monkApi?.OpoOpoFury > 0 ? Spells.连击adaptive : Spells.双龙脚;
        }
        else
        {
            // 默认身形：根据斗气量选择技能
            return _monkApi?.OpoOpoFury > 0 ? Spells.连击adaptive : Spells.双龙脚;
        }
    }

    public void Build(Slot slot)
    {
        // 更新AOE检测
        CheckAOE();

        slot.Add(GetSpellId().GetSpell());
    }
}