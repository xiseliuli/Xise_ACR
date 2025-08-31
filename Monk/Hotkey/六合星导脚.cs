using System.Numerics;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using Xise.Monk.SlotResolver.Data;

namespace Xise.Monk.Hotkey;

public class 六合星导脚 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
    }

    public int Check()
    {
        return 0;
    }

    public void Run()
    {
        AI.Instance.BattleData.NextSlot.Add((Spells.六合星导脚.GetSpell()));
    }
}