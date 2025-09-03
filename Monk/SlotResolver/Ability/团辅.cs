using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Xise.Common;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.SlotResolver.Ability;

public class 团辅 : ISlotResolver
{
    public int Check()
    {
        long currBattleTimeInMs = AI.Instance.BattleData.CurrBattleTimeInMs;
        // 检查等级要求
        if (Core.Me?.Level < 68) return -1;

        if (currBattleTimeInMs < 4 * 1000) return -1;

        // 检查技能可用性
        if (!Spells.红莲极意.GetSpell().IsReadyWithCanCast() &&
            !Spells.义结金兰.GetSpell().IsReadyWithCanCast()) return -1;

        // 检查震脚状态
        if (Core.Me?.HasAura(Buffs.震脚) == true) return 0;

        // 检查是否在团辅状态
        if (Helper.In团辅()) return 0;

        // int 震脚cd = Helper.MemApiSpell.GetCooldown(Spells.震脚).Seconds;
        // if (震脚cd > 8) return 0;

        return 0;
    }

    public void Build(Slot slot)
    {
        // 添加义结金兰技能
        if (Spells.义结金兰.GetSpell().IsReadyWithCanCast())
            slot.Add(Spells.义结金兰.GetSpell());

        // 添加红莲极意技能
        if (Spells.红莲极意.GetSpell().IsReadyWithCanCast())
            slot.Add(Spells.红莲极意.GetSpell());
    }
}