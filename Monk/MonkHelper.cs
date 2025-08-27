using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace Xise.Monk;

public class MonkHelper
{
    // ✅ 使用依赖注入获取API实例
    public static JobApi_Monk MonkApi => Core.Resolve<JobApi_Monk>();
}