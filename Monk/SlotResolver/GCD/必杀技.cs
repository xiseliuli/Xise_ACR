using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.API.Helper;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Xise.Common;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Ability;

public class 必杀技 : ISlotResolver
{
    private readonly JobApi_Monk _monkApi;
    
    public 必杀技()
    {
        // ✅ 使用依赖注入获取API实例
        _monkApi = Core.Resolve<JobApi_Monk>();
    }
    
    public int Check()
    {
        // 检查必杀技是否可用
        if (Helper.MemApiSpell.CheckActionChange(Spells.必杀技) != Spells.必杀技) return -1;
        
        // 检查等级要求
        if (Core.Me?.Level < 60) return -1;
        
        // 检查震脚状态
        if (Core.Me?.HasAura(Buffs.震脚) != true) return -1;
        
        // 检查兽形斗气状态
        if (_monkApi?.BeastChakra[2] == BeastChakra.None) return -1;

        return 0;
    }

    public void Build(Slot slot)
    {
        // 添加必杀技
        slot.Add(Helper.MemApiSpell.CheckActionChange(Spells.必杀技).GetSpell());
    }
}