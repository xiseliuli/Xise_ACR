using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Xise.Monk.Hotkey;
using Xise.Monk.SlotResolver.Data;
using Xise.Monk.SlotResolver.GCD;

namespace Xise.Monk.QtUI;

public class Qt
{
    public static JobViewWindow Instance { get; set; }

    /// <summary>
    /// 除了爆发药以外都复原
    /// </summary>
    public static void Reset()
    {
        Instance.SetQt("AOE", true);
        Instance.SetQt("双阴起手", true);
        Instance.SetQt("最终爆发", false);
        Instance.SetQt("攒功力", false);
        Instance.SetQt("下一个打阴", false);
        Instance.SetQt("自动真北", false);
        MonkHelper.ResetNadiCounter();
    }

    public static void Build()
    {
        Instance = new JobViewWindow(MonkSettings.Instance.JobViewSave, MonkSettings.Instance.Save, "XiseMonk");

        Instance.AddQt("爆发药", true);
        Instance.SetQt("AOE", true);
        Instance.AddQt("双阴起手", true);
        Instance.AddQt("最终爆发", false);
        Instance.AddQt("攒功力", false);
        Instance.AddQt("下一个打阴", true);
        Instance.AddQt("自动真北", true);


        Instance.AddHotkey("LB", new HotKeyResolver_LB());
        Instance.AddHotkey("疾跑", new HotKeyResolver_疾跑());
        Instance.AddHotkey("爆发药", new HotKeyResolver_Potion());

        Instance.AddHotkey("六合星导脚", new 六合星导脚());

        Instance.AddHotkey("极限技", new HotKeyResolver_LB());
        Instance.AddHotkey("爆发药", new HotKeyResolver_Potion());
        Instance.AddHotkey("震脚", new HotKeyResolver_NormalSpell(Spells.震脚, (SpellTargetType)1, false));
        Instance.AddHotkey("牵制", new HotKeyResolver_NormalSpell(Spells.牵制, 0, false));
        Instance.AddHotkey("亲疏自行", new HotKeyResolver_NormalSpell(Spells.亲疏自行, (SpellTargetType)1, false));
        Instance.AddHotkey("真北", new HotKeyResolver_NormalSpell(Spells.真北, (SpellTargetType)1, false));
        Instance.AddHotkey("冲刺", new HotKeyResolver_疾跑());

        Instance.AddHotkey("真言", new HotKeyResolver_NormalSpell(Spells.真言, (SpellTargetType)1, false));
        Instance.AddHotkey("浴血", new HotKeyResolver_NormalSpell(Spells.浴血, (SpellTargetType)1, false));
        Instance.AddHotkey("内丹", new HotKeyResolver_NormalSpell(Spells.内丹, (SpellTargetType)1, false));
    }
}