using AEAssist;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace Xise.Monk;

public class MonkHelper
{
    // ✅ 使用依赖注入获取API实例
    public static JobApi_Monk MonkApi => Core.Resolve<JobApi_Monk>();

    public class NadiCount
    {
        public int Lunar { get; set; }
        public int Solar { get; set; }

        public NadiCount(int lunar, int solar)
        {
            Lunar = lunar;
            Solar = solar;
        }
    }

    public static NadiCount NadiCounter { get; set; } = new NadiCount(0, 0);

    public static void ResetNadiCounter()
    {
        NadiCounter.Lunar = 0;
        NadiCounter.Solar = 0;
    }
}