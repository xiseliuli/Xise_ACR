using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.GCD;

public class 震脚GCD : ISlotResolver
{
    public int Check()
    {
        if (MonkRotationEventHandler.IsStopped || MonkRotationEventHandler.IsInvul) return -1;
        if (Core.Me.HasAura(Buffs.震脚)) return -2;
        return 0;
    }

    public void Build(Slot slot)
    {
        throw new NotImplementedException();
    }
}