using AEAssist;
using AEAssist.MemoryApi;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

namespace Xise.Common;

public static class Helper
{
    // 作者
    public const string AuthorName = "Xise";

    /// <summary>
    /// 获取自身buff的剩余时间
    /// </summary>
    /// <param name="buffId"></param>
    /// <returns></returns>
    public static int GetAuraTimeLeft(uint buffId) => Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, buffId, true);

    /// <summary>显示一个文本提示，用于在游戏中显示简短的消息。</summary>
    /// <param name="msg">要显示的消息文本。</param>
    /// <param name="s">文本提示的样式。支持蓝色提示（1）和红色提示（2）两种</param>
    /// <param name="time">文本提示显示的时间（单位毫秒）。如显示3秒，填写3000即可</param>
    public static void SendTips(string msg, int s = 1, int time = 3000) => Core.Resolve<MemApiChatMessage>()
        .Toast2(msg, s, time);

    /// <summary>
    /// 全局设置
    /// </summary>
    public static GeneralSettings GlobalSettings => SettingMgr.GetSetting<GeneralSettings>();

    /// <summary>
    /// 当前地图id
    /// </summary>
    public static uint GetTerritoyId => Core.Resolve<MemApiMap>().GetCurrTerrId();

    /// <summary>
    /// 返回可变技能的当前id
    /// </summary>
    public static uint GetActionChange(uint spellId) => Core.Resolve<MemApiSpell>().CheckActionChange(spellId);

    /// <summary>
    /// 返回连击剩余时间
    /// </summary>
    public static double GetComboTimeLeft => Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds;

    /// <summary>
    /// 在攻击范围
    /// </summary>
    public static bool WithinAttackRange =>
        Core.Me.Distance(Core.Me.GetCurrTarget()!) <= SettingMgr.GetSetting<GeneralSettings>().AttackRange;

    /// <summary>
    /// 在背身
    /// </summary>
    public static bool IsBehind => Core.Resolve<MemApiTarget>().IsBehind;

    /// <summary>
    /// 在侧身
    /// </summary>
    public static bool IsFlanking => Core.Resolve<MemApiTarget>().IsFlanking;

    /// <summary>
    /// 检测buff时间是否小于
    /// </summary>
    public static bool BuffTimeLessThan(uint buffId, int timeLeft)
    {
        if (!Core.Me.HasAura(buffId)) return false;
        return GetAuraTimeLeft(buffId) <= timeLeft;
    }

    public static IBattleChara? 最优aoe目标(this uint spellId, int count)
    {
        return TargetHelper.GetMostCanTargetObjects(spellId, count);
    }

    public static bool In团辅()
    {
        //检测目标团辅
        List<uint> 目标团辅 = [背刺, 连环计];
        if (目标团辅.Any(buff => BuffTimeLessThan(buff, 15000))) return true;

        //检测自身团辅
        List<uint> 自身团辅 = [灼热之光, 星空, 占卜, 义结金兰, 战斗连祷, 大舞, 战斗之声, 鼓励, 神秘环];
        return 自身团辅.Any(buff => BuffTimeLessThan(buff, 15000));
    }
    
    public static MemApiCondition MemApi = new MemApiCondition();
    
    public static MemApiSpell MemApiSpell = new MemApiSpell();
    

    private static uint
        背刺 = 3849,
        强化药 = 49,
        灼热之光 = 2703,
        星空 = 3685,
        占卜 = 1878,
        义结金兰 = 1185,
        战斗连祷 = 786,
        大舞 = 1822,
        战斗之声 = 141,
        鼓励 = 1239,
        神秘环 = 2599,
        连环计 = 2617;
}