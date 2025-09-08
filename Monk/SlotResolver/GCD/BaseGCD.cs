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
    private readonly JobApi_Monk _monkApi;

    private static int nearbyEnemies;
    private static bool isAOEEnabled;
    private bool HasLunar;
    private bool HasSolar;

    public BaseGCD()
    {
        // ✅ 使用依赖注入获取API实例
        _monkApi = Core.Resolve<JobApi_Monk>();
    }

    private bool 猛豹身形 => Core.Me?.HasAura(Buffs.猛豹身形) == true;
    private bool 盗龙身形 => Core.Me?.HasAura(Buffs.盗龙身形) == true;
    private bool 无相身形 => Core.Me?.HasAura(Buffs.无相身形) == true;
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

    // 检查必杀斗气
    public void CheckNadi()
    {
        HasLunar = (_monkApi.Nadi & Nadi.Lunar) != 0;
        HasSolar = (_monkApi.Nadi & Nadi.Solar) != 0;
    }

    public static void CheckAOE()
    {
        // 获取AOE设置状态
        isAOEEnabled = Qt.Instance?.GetQt("AOE") ?? true;
        // 检测附近敌人数量（5码范围内）
        nearbyEnemies = TargetHelper.GetNearbyEnemyCount((IBattleChara)Core.Me, 5, 5);
    }

    public void Build(Slot slot)
    {
        // 更新AOE检测
        CheckAOE();

        // 检查最终爆发模式
        if (Qt.Instance?.GetQt("最终爆发") == true)
        {
            // 最终爆发模式：优先使用斗气技能
            if (_monkApi?.CoeurlFury > 0 && 猛豹身形)
            {
                slot.Add(Spells.崩拳adaptive.GetSpell());
                return;
            }

            if (_monkApi?.RaptorFury > 0 && 盗龙身形)
            {
                slot.Add(Spells.正拳adaptive.GetSpell());
                return;
            }

            if (_monkApi?.OpoOpoFury > 0)
            {
                slot.Add(Spells.连击adaptive.GetSpell());
                return;
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
                    slot.Add(Spells.破坏神脚.GetSpell());
                }
                else if (Core.Me?.Level >= 52)
                {
                    slot.Add(Spells.地烈劲.GetSpell());
                }

                return;
            }
            else if (猛豹身形 && Core.Me?.Level >= 30)
            {
                // 猛豹身形AOE
                slot.Add(Spells.地烈劲.GetSpell());
                return;
            }
            else if (盗龙身形 && Core.Me?.Level >= 18)
            {
                // 盗龙身形AOE
                slot.Add(Spells.四面脚.GetSpell());
                return;
            }
            else if (Core.Me?.Level >= 26)
            {
                // 默认身形AOE
                slot.Add(Spells.破坏神冲.GetSpell());
                return;
            }
        }

        // 检查攒功力模式
        if (Qt.Instance?.GetQt("攒功力") == true)
        {
            // 攒功力模式：优先使用基础技能积累斗气
            if (猛豹身形)
            {
                slot.Add(Spells.破碎拳.GetSpell());
                return;
            }

            if (盗龙身形)
            {
                slot.Add(Spells.双掌打.GetSpell());
                return;
            }

            slot.Add(Spells.双龙脚.GetSpell());
            return;
        }

        if (Core.Me?.HasAura(Buffs.震脚) == true)
        {
            // 更新阴阳斗气
            CheckNadi();
            if (Qt.Instance.GetQt("下一个打阴") || !HasLunar)
            {
                slot.Add(_monkApi?.CoeurlFury > 0 ? Spells.崩拳adaptive.GetSpell() : Spells.破碎拳.GetSpell());
                return;
            }

            if (!_monkApi.BeastChakra.Contains(BeastChakra.Coeurl))
            {
                slot.Add(_monkApi?.CoeurlFury > 0 ? Spells.崩拳adaptive.GetSpell() : Spells.破碎拳.GetSpell());
                return;
            }

            if (!_monkApi.BeastChakra.Contains(BeastChakra.Raptor))
            {
                slot.Add(_monkApi?.RaptorFury > 0 ? Spells.正拳adaptive.GetSpell() : Spells.双掌打.GetSpell());
                return;
            }

            if (!_monkApi.BeastChakra.Contains(BeastChakra.OpoOpo))
            {
                slot.Add(_monkApi?.OpoOpoFury > 0 ? Spells.连击adaptive.GetSpell() : Spells.双龙脚.GetSpell());
                return;
            }
        }

        // 根据身形状态选择技能
        if (猛豹身形)
        {
            // 猛豹身形：根据斗气量选择技能
            slot.Add(_monkApi?.CoeurlFury > 0 ? Spells.崩拳adaptive.GetSpell() : Spells.破碎拳.GetSpell());
        }
        else if (盗龙身形)
        {
            // 盗龙身形：根据斗气量选择技能
            slot.Add(_monkApi?.RaptorFury > 0 ? Spells.正拳adaptive.GetSpell() : Spells.双掌打.GetSpell());
        }
        else if (无相身形)
        {
            // 无相身形：根据斗气量选择技能
            slot.Add(_monkApi?.OpoOpoFury > 0 ? Spells.连击adaptive.GetSpell() : Spells.双龙脚.GetSpell());
        }
        else
        {
            // 默认身形：根据斗气量选择技能
            slot.Add(_monkApi?.OpoOpoFury > 0 ? Spells.连击adaptive.GetSpell() : Spells.双龙脚.GetSpell());
        }
    }
}