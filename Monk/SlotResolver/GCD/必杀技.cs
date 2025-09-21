using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Xise.Common;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class 必杀技 : ISlotResolver
{
    private readonly JobApi_Monk MonkApi;
    private uint Get必杀技;

    public 必杀技()
    {
        // ✅ 使用依赖注入获取API实例
        MonkApi = Core.Resolve<JobApi_Monk>();
    }

    public void Check必杀技()
    {
        Get必杀技 = Helper.MemApiSpell.CheckActionChange(Spells.必杀技);
    }

    public int Check()
    {
        // 检查等级要求
        if (Core.Me.Level < 60) return -1;

        Check必杀技();
        // 检查必杀技是否可用
        if (Get必杀技 == Spells.必杀技) return -1;

        // 检查震脚状态
        if (Core.Me.HasAura(Buffs.震脚) || Core.Me.HasAura(Buffs.无相身形)) return -1;

        if (!Core.Me.HasAura(Buffs.红莲极意) && (MonkApi.BlitzTimeRemaining / 1000) >
            ((Helper.MemApiSpell?.GetCooldown(Spells.红莲极意).Seconds ?? 0) + 3)) return -1;

        return 0;
    }

    public void Build(Slot slot)
    {
        // 添加必杀技
        slot.Add(Get必杀技.GetSpell());
    }
}