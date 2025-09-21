using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.View;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Xise.Monk;
using Xise.Monk.SlotResolver.GCD;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;
using Dalamud.Game.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Dalamud.Game.ClientState.JobGauge.Enums;
using ECommons;
using Xise.Common;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk;

public class MonkRotationEventHandler : IRotationEventHandler
{
    // 停手
    public static bool IsStopped = false;

    // 无敌
    public static bool IsInvul = false;

    // 必杀计数
    public static int FinishingMoveCount = 1;

    // 无目标
    public static bool NoTarget = false;

    public static double AttackRange;

    // 战前准备
    public async Task OnPreCombat()
    {
        if (Core.Resolve<MemApiDuty>().IsOver || !Core.Resolve<MemApiDuty>().InMission)
            return;
        if (Core.Resolve<MemApiDuty>().DutyMembersNumber() == 8 && MonkSettings.Instance.NoBurst)
        {
            MonkSettings.Instance.NoBurst = false;
        }

        var slot = new Slot(2500);

        if (Core.Resolve<JobApi_Monk>().BlitzTimeRemaining <= 5000 &&
            Core.Resolve<JobApi_Monk>().BlitzTimeRemaining > 0 &&
            (
                Spells.真空波adaptive.GetSpell().IsReadyWithCanCast() ||
                Spells.凤凰舞adaptive.GetSpell().IsReadyWithCanCast()
            )
           )
        {
            slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(Spells.必杀技).GetSpell());
            await PVE_RunSlotHelper.Run(slot, AI.Instance.BattleData, false);
        }

        if (Qt.Instance.GetQt("搓豆子") &&
            Spells.斗气adaptive.GetSpell().IsReadyWithCanCast())
        {
            slot.Add(Spells.斗气adaptive.GetSpell());
            await PVE_RunSlotHelper.Run(slot, AI.Instance.BattleData, false);
        }

        if (
            Core.Me.HasAura(Buffs.震脚) ||
            Core.Me.HasAura(Buffs.无相身形) ||
            !Spells.演武.IsUnlock() ||
            !Qt.Instance.GetQt("自动演武")
        )
        {
            return;
        }


        slot.Add(Spells.演武.GetSpell());
        await PVE_RunSlotHelper.Run(slot, AI.Instance.BattleData, false);
    }

    // 战斗重置
    public void OnResetBattle()
    {
        IsInvul = false;
        if (Core.Resolve<JobApi_Monk>().Nadi == Nadi.None)
            FinishingMoveCount = 1;
    }

    // 无目标
    public async Task OnNoTarget()
    {
        if (IsStopped) return;
        NoTarget = true;

        var slot = new Slot(2500);

        if (Core.Resolve<JobApi_Monk>().BlitzTimeRemaining <= 5000 &&
            Core.Resolve<JobApi_Monk>().BlitzTimeRemaining > 0 &&
            (
                Spells.真空波adaptive.GetSpell().IsReadyWithCanCast() ||
                Spells.凤凰舞adaptive.GetSpell().IsReadyWithCanCast()
            )
           )
        {
            slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(Spells.必杀技).GetSpell());
            await PVE_RunSlotHelper.Run(slot, AI.Instance.BattleData, false);
        }

        if (Qt.Instance.GetQt("搓豆子") &&
            Spells.斗气adaptive.GetSpell().IsReadyWithCanCast())
        {
            slot.Add(Spells.斗气adaptive.GetSpell());
            await PVE_RunSlotHelper.Run(slot, AI.Instance.BattleData, false);
        }

        if (
            Core.Me.HasAura(Buffs.震脚) ||
            Core.Me.HasAura(Buffs.无相身形) ||
            !Spells.演武.IsUnlock() ||
            !Qt.Instance.GetQt("自动演武")
        )
        {
            return;
        }


        slot.Add(Spells.演武.GetSpell());
        await PVE_RunSlotHelper.Run(slot, AI.Instance.BattleData, false);
    }

    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {
    }

    // 技能之后
    public void AfterSpell(Slot slot, Spell spell)
    {
        LogHelper.Print($"{spell.Id} {Spells.必杀.IndexOf(spell.Id) != -1}");
        if (Spells.必杀.IndexOf(spell.Id) != -1)
            ++FinishingMoveCount;
        if (spell == SpellHelper.GetSpell(7395U))
            AI.Instance.BattleData.CurrGcdAbilityCount = 0;
        if ((spell == SpellHelper.GetSpell(25768U) || spell == SpellHelper.GetSpell(25882U)) &&
            Qt.Instance.GetQt("下一个打阳"))
            Qt.Instance.SetQt("下一个打阳", false);
        if (spell != SpellHelper.GetSpell(3545U) && spell != SpellHelper.GetSpell(36948U) ||
            !Qt.Instance.GetQt("下一个打阴"))
            return;
        Qt.Instance.SetQt("下一个打阴", false);
    }


    // 战斗更新
    public void OnBattleUpdate(int currTimeInMs)
    {
        // 判断无敌
        if (Core.Me.HasAnyAura(Helper.Stop.StopSpell) && !IsInvul)
        {
            IsInvul = true;
        }

        // 判断停手
        if (!Core.Me.HasAnyAura(Helper.Stop.TargetCancel) && IsStopped)
        {
            IsInvul = true;
        }
    }

    // 进入ACR
    public void OnEnterRotation()
    {
    }

    // 退出ACR
    public void OnExitRotation()
    {
    }

    // 区域变更
    public void OnTerritoryChanged()
    {
    }
}