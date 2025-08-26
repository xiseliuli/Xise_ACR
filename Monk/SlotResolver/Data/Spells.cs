using AEAssist;

namespace Xise.Monk.SlotResolver.Data;

public class Spells
{
    public static uint
        // 123
        连击 = 53,
        正拳 = 54,
        崩拳 = 56,
        双龙脚 = 74,
        双掌打 = 61,
        破碎拳 = 66,
        猿舞连击 = 36945,
        龙颚正拳 = 36946,
        豹袭崩拳 = 36947,
        // aoe
        破坏神冲 = 62,
        四面脚 = 16473,
        地烈劲 = 70,
        破坏神脚 = 25767,
        // 斗气
        铁山斗气 = 36940,
        空鸣斗气 = 36941,
        万象斗气 = 36943,
        阴阳斗气 = 36942,
        铁山靠 = 25761,
        空鸣拳 = 67,
        阴阳斗气斩 = 3547,
        万象斗气圈 = 16474,
        // 必杀
        翻天脚 = 25765,
        苍气炮 = 3545,
        梦幻斗舞 = 25769,
        凤凰舞 = 25768,
        真空波 = 36948,
        斗魂旋风脚 = 3543,
        爆裂脚 = 25882,
        必杀技 = 25764,
        // 爆发
        红莲极意 = 7395,
        疾风极意 = 25766,
        义结金兰 = 7396,
        乾坤斗气弹 = 36950,
        绝空拳 = 36949,
        // 减伤
        牵制 = 7549,
        真言 = 65,
        金刚极意 = 7394,
        金刚周天 = 36944,
        // buff
        震脚 = 69,
        演武 = 4262,

        // 特殊
        浴血 = 7542,
        内丹 = 7541,
        真北 = 7546,
        //其他
        冲刺 = 3,
        轻身步法 = 25762,
        六合星导脚 = 16476,
        亲疏自行 = 7548,
        扫腿 = 7863;

    public static uint 连击adaptive =>
        Core.Me.Level switch
        {
            >= 92 => 猿舞连击,
            _ => 连击
        };

    public static uint 正拳adaptive =>
        Core.Me.Level switch
        {
            >= 92 => 龙颚正拳,
            _ => 正拳
        };

    public static uint 崩拳adaptive =>
        Core.Me.Level switch
        {
            >= 92 => 豹袭崩拳,
            _ => 崩拳
        };

    public static uint 破坏神脚adaptive =>
        Core.Me.Level switch
        {
            >= 82 => 破坏神脚,
            _ => 破坏神冲
        };

    public static uint 斗气adaptive =>
        Core.Me.Level switch
        {
            >= 74 => 万象斗气,
            >= 54 => 阴阳斗气,
            >= 40 => 空鸣斗气,
            _ => 铁山斗气
        };

    public static uint 阴阳斗气斩adaptive =>
        Core.Me.Level switch
        {
            >= 54 => 阴阳斗气斩,
            _ => 铁山靠
        };

    public static uint 万象斗气圈adaptive =>
        Core.Me.Level switch
        {
            >= 74 => 万象斗气圈,
            _ => 空鸣拳
        };

    public static uint 凤凰舞adaptive =>
        Core.Me.Level switch
        {
            >= 86 => 凤凰舞,
            _ => 爆裂脚
        };

    public static uint 真空波adaptive =>
        Core.Me.Level switch
        {
            >= 92 => 真空波,
            _ => 苍气炮
        };

    public static uint 梦幻斗舞adaptive =>
        Core.Me.Level switch
        {
            >= 90 => 梦幻斗舞,
            _ => 斗魂旋风脚
        };
}