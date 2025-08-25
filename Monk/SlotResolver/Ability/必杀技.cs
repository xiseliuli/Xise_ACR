using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Ability;

public class 必杀技
{
    public int Check()
    {
        return Core.Me.Level < 60 ? -1 : 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.必杀技.GetSpell());
    }
}