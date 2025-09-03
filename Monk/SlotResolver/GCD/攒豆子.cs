using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class 攒豆子 : ISlotResolver
{
    public int Check()
    {
        if (Core.Me.Level < 15) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.斗气adaptive.GetSpell());
    }
}