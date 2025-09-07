using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Xise.Monk.SlotResolver.Data;
using Xise.Monk.QtUI;

namespace Xise.Monk.SlotResolver.Ability;

public class 疾风极意 : ISlotResolver
{
    public int Check()
    {
        if (Core.Me.Level < 72 || !Spells.疾风极意.GetSpell().IsReadyWithCanCast()) return -1;
        
        // 检查QT设置
        if (Qt.Instance?.GetQt("疾风极意") != true) return -1;
        
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Spells.疾风极意.GetSpell());
    }
}