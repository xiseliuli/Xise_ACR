using System.Runtime.InteropServices;
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
        // 检测目标团辅 - 当目标有团辅Buff时使用团辅技能
        List<uint> 目标团辅 = [背刺, 连环计];
        if (目标团辅.Any(buff => Core.Me?.GetCurrTarget()?.HasAura(buff) == true)) return true;

        // 检测自身团辅 - 当自身有团辅Buff时使用团辅技能
        List<uint> 自身团辅 = [灼热之光, 星空, 占卜, 义结金兰, 战斗连祷, 大舞, 战斗之声, 鼓励, 神秘环];
        return 自身团辅.Any(buff => Core.Me?.HasAura(buff) == true);
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

    public static class Stop
    {
        private const uint 加速度炸弹4144 = 4144;
        private const uint 加速度炸弹3802 = 3802;
        private const uint 加速度炸弹3793 = 3793;
        private const uint 加速度炸弹1384 = 1384;
        private const uint 加速度炸弹2657 = 2657;
        private const uint 加速度炸弹1072 = 1072;
        private const uint 热病960 = 960;
        private const uint 热病639 = 639;
        private const uint 热病3522 = 3522;
        private const uint 热病1599 = 1599;
        private const uint 热病1133 = 1133;
        private const uint 热病1049 = 1049;
        private const uint 无敌325 = 325;
        private const uint 无敌529 = 529;
        private const uint 无敌656 = 656;
        private const uint 无敌671 = 671;
        private const uint 无敌775 = 775;
        private const uint 无敌776 = 776;
        private const uint 无敌969 = 969;
        private const uint 无敌981 = 981;
        private const uint 无敌1570 = 1570;
        private const uint 无敌1697 = 1697;
        private const uint 无敌1829 = 1829;
        private const uint 土神的心石 = 328;
        private const uint 纯正神圣领域 = 2287;
        private const uint 风神障壁 = 3012;

        public static readonly List<uint> TargetCancel = new()
        {
            加速度炸弹4144, 加速度炸弹3802, 加速度炸弹3793, 加速度炸弹1384, 加速度炸弹2657, 加速度炸弹1072,
            热病960, 热病639, 热病3522, 热病1599, 热病1133, 热病1049
        };

        public static readonly List<uint> StopSpell = new()
        {
            无敌325, 无敌529, 无敌656, 无敌671, 无敌775, 无敌776, 无敌969, 无敌981,
            无敌1570, 无敌1697, 无敌1829, 土神的心石, 纯正神圣领域, 风神障壁
        };
    }
}