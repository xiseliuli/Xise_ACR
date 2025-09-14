using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Ability;

public class 义结金兰 : ISlotResolver
{
    public int Check()
    {
        if (!Spells.义结金兰.GetSpell().IsReadyWithCanCast()) return -1;
        long currBattleTimeInMs = AI.Instance.BattleData.CurrBattleTimeInMs;
        if (currBattleTimeInMs < 3 * 1000) return -1;

        if (Qt.Instance?.GetQt("义结金兰") != true) return -1;
        if (GCDHelper.GetGCDCooldown() < 1300 ||
            Core.Resolve<MemApiSpell>().GetCooldown(Spells.红莲极意).TotalMilliseconds > 700)
            return -1;
        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.义结金兰.GetSpell());
    }
}