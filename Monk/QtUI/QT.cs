using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;

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
    }

    public static void Build()
    {
        Instance = new JobViewWindow(MonkSettings.Instance.JobViewSave, MonkSettings.Instance.Save, "XiseMonk");
        
        Instance.AddQt("爆发药", true);
        Instance.SetQt("AOE", true);
        Instance.AddQt("双阴起手", true);
        
        Instance.AddHotkey("LB", new HotKeyResolver_LB());
        Instance.AddHotkey("防击退",
            new HotKeyResolver_NormalSpell(SpellsDefine.ArmsLength, SpellTargetType.Self, false));
        Instance.AddHotkey("内丹",
            new HotKeyResolver_NormalSpell(SpellsDefine.SecondWind, SpellTargetType.Self, false));

        Instance.AddHotkey("疾跑", new HotKeyResolver_疾跑());
        Instance.AddHotkey("爆发药", new HotKeyResolver_Potion());
    }

    
}