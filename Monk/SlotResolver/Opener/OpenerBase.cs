using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Helper;
using Xise.Monk.SlotResolver.Ability;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Opener;

public class OpenerBase : IOpener
{
    public int StartCheck()
    {
        // 核心爆发就绪检查：红莲极意、义结金兰
        if (!Spells.红莲极意.GetSpell().IsReadyWithCanCast()) return -2;
        if (!Spells.义结金兰.GetSpell().IsReadyWithCanCast()) return -3;

        if (Core.Me.Level >= 60 && !Spells.必杀技.GetSpell().IsReadyWithCanCast()) return -4;

        return 0;
    }

    public int StopCheck(int index)
    {
        // 不强制中断
        return -1;
    }

    public void InitCountDown(CountDownHandler handler)
    {
        // 预拉时间（ms）
        const int startTime = 15000;

        // 开场10s 真言 
        if (MonkHelper.队里有贤者or学者) handler.AddAction(startTime - 5000, Data.Spells.真言);

        handler.AddAction(startTime - 10000, Spells.演武);
        handler.AddAction(startTime - 12500, Spells.斗气adaptive);
    }
    
    public List<Action<Slot>> Sequence { get; } =
    [
    ];
}