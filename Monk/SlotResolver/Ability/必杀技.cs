using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.API.Helper;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Xise.Common;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Ability;

public class 必杀技 : ISlotResolver
{
    public int Check()
    {
        if (Helper.MemApiSpell.CheckActionChange(Spells.必杀技) != Spells.必杀技) return -1;
        if (Core.Me.Level < 60 ||
            !Core.Me.HasAura(Buffs.震脚) || Core.Resolve<JobApi_Monk>().BeastChakra[2] == BeastChakra.None)
        {
            return -1;
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Helper.MemApiSpell.CheckActionChange(Spells.必杀技).GetSpell());
    }
}