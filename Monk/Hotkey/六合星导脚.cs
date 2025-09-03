using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.Hotkey;

public class 六合星导脚 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 vector2 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap idalamudTextureWrap;
        Core.Resolve<MemApiIcon>().GetActionTexture(Spells.六合星导脚, out idalamudTextureWrap);
        ImGui.Image(idalamudTextureWrap.ImGuiHandle, vector2);
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(new Spell(Spells.六合星导脚, GameObjectExtension.GetCurrTarget(Core.Me)), size, isActive);
    }

    public int Check()
    {
        return 0;
    }

    public void Run()
    {
        Spell spell = new Spell(Spells.六合星导脚, GameObjectExtension.GetCurrTarget(Core.Me))
        {
            DontUseGcdOpt = true
        };
        AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(new Slot().Add(spell));
    }
}