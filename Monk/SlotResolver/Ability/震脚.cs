using AEAssist.MemoryApi;
using Xise.Monk.SlotResolver.Data;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;


namespace Xise.Monk.SlotResolver.Ability;

public class 震脚
{
    public int Check()
    {
        MemApiSpell api = new MemApiSpell();

        // 在爆发前开
        if (api.GetCooldown(Spells.红莲极意).Seconds < 3 || api.GetCooldown(Spells.义结金兰).Seconds < 3)
        {
            return 0;
        }

        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Data.Spells.震脚.GetSpell());
    }
}