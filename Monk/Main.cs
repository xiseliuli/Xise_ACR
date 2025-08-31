using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using Xise.Common;
using Xise.Monk.QtUI;
using Xise.Monk.SlotResolver.GCD;
using Xise.Monk.SlotResolver.Ability;
using Xise.Monk.SlotResolver.Opener;

namespace Xise.Monk;

public class MonkRotationEntry : IRotationEntry, IDisposable
{
    public string AuthorName { get; set; } = Helper.AuthorName;

    private readonly Jobs _targetJob = Jobs.Monk;
    private readonly AcrType _acrType = AcrType.Both; //高难专用
    private readonly int _minLevel = 15;
    private readonly int _maxLevel = 100;


    private readonly List<SlotResolverData> _slotResolvers =
    [

        // 能力技
        new(new 震脚(), SlotMode.OffGcd),
        new(new 斗气(), SlotMode.OffGcd),
        new(new 团辅(), SlotMode.OffGcd),
        new(new 疾风极意(), SlotMode.OffGcd),
        
        // GCD
        new(new 必杀技(), SlotMode.Gcd),
        new(new 乾坤斗气弹(), SlotMode.Gcd),
        new(new 绝空拳(), SlotMode.Gcd),
        new(new Base(), SlotMode.Gcd),

    ];


    public Rotation? Build(string settingFolder)
    {
        MonkSettings.Build(settingFolder);
        Qt.Build();
        var rot = new Rotation(_slotResolvers)
        {
            TargetJob = _targetJob,
            // AcrType = _acrType,
            // MinLevel = _minLevel,
            // MaxLevel = _maxLevel,
        };
        rot.AddOpener(level => level < _minLevel ? null : new OpenerBase());
        return rot;
    }

    public IRotationUI GetRotationUI()
    {
        return Qt.Instance;
    }

    public void OnDrawSetting()
    {
    }

    public void Dispose()
    {
    }
}