using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Ability;

public class 红莲极意 : ISlotResolver
{
    public int Check()
    {
        if (GCDHelper.GetGCDCooldown() < 600)
            return -1;

        if (!Spells.红莲极意.GetSpell().IsReadyWithCanCast() ||
            !Qt.Instance.GetQt("红莲极意")) return -1;
        if (Spells.义结金兰.RecentlyUsed())
            return 1;
        if (Qt.Instance.GetQt("义结金兰") &&
            Core.Resolve<MemApiSpell>().GetCooldown(Spells.义结金兰).TotalMilliseconds < 20000) return -1;

        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.红莲极意.GetSpell());
    }
}