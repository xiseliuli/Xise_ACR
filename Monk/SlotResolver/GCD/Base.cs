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
    private readonly JobApi_Monk _monkApi;
    
    public Base()
    {
        // ✅ 使用依赖注入获取API实例
        _monkApi = Core.Resolve<JobApi_Monk>();
    }
    
    // ✅ 性能优化：缓存状态检查结果
    private bool 猛豹身形 => Core.Me?.HasAura(Buffs.猛豹身形) == true;
    private bool 盗龙身形 => Core.Me?.HasAura(Buffs.盗龙身形) == true;
    private bool 无相身形 => Core.Me?.HasAura(Buffs.无相身形) == true;
    private bool Is战斗 => Helper.MemApi?.IsInCombat() == true;

    public int Check()
    {
        return 0;
    }

    public void Build(Slot slot)
    {
        // 检查必杀技状态
        uint 必杀技 = Helper.MemApiSpell?.CheckActionChange(Spells.必杀技) ?? Spells.必杀技;

        // 如果必杀技可用且不在团辅状态
        if (必杀技 != Spells.必杀技)
        {
            if (Helper.In团辅())
            {
                // 团辅状态下使用必杀技
                slot.Add(必杀技.GetSpell());
                return;
            }

            // 检查红莲极意冷却时间
            int 红莲剩余cd = Helper.MemApiSpell?.GetCooldown(Spells.红莲极意).Seconds ?? 0;
            if (红莲剩余cd > 8)
            {
                // 红莲极意冷却时间较长时使用必杀技
                slot.Add(必杀技.GetSpell());
                return;
            }
        }

        // 根据身形状态选择技能
        if (猛豹身形)
        {
            // 猛豹身形：根据斗气量选择技能
            slot.Add(_monkApi?.CoeurlFury > 0 ? 
                Spells.崩拳adaptive.GetSpell() : 
                Spells.破碎拳.GetSpell());
        }
        else if (盗龙身形)
        {
            // 盗龙身形：根据斗气量选择技能
            slot.Add(_monkApi?.RaptorFury > 0 ? 
                Spells.正拳adaptive.GetSpell() : 
                Spells.双掌打.GetSpell());
        }
        else
        {
            // 默认身形：根据斗气量选择技能
            slot.Add(_monkApi?.OpoOpoFury > 0 ? 
                Spells.连击adaptive.GetSpell() : 
                Spells.双龙脚.GetSpell());
        }
    }
}