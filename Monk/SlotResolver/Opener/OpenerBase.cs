using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Helper;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Opener;

public class OpenerBase : IOpener
{
    public int StartCheck()
    {
        if (Spells.红莲极意.GetSpell().IsReadyWithCanCast()) return -1;
        if (Spells.义结金兰.GetSpell().IsReadyWithCanCast()) return -2;
        return 0;
    }

    public int StopCheck(int index)
    {
        return -1;
    }

    public List<Action<Slot>> Sequence { get; } =
    [
        Step1,
    ];

    public void InitCountDown(CountDownHandler handler)
    {
        Qt.Reset();

        const int startTime = 5000;
        handler.AddAction(startTime, Spells.演武);
        handler.AddAction(startTime - 2000, Spells.斗气adaptive);
    }

    public static void Step1(Slot slot)
    {
        // slot.Add(Spells.双龙脚.GetSpell());
    }

}