using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace Xise.Monk;

public class MonkHelper
{
    // ✅ 使用依赖注入获取API实例
    public static JobApi_Monk MonkApi => Core.Resolve<JobApi_Monk>();
    
    
    public static bool 队里有贤者or学者 => 
        PartyHelper.Party.FirstOrDefault(p => p.CurrentJob() == Jobs.Scholar) != null ||
        PartyHelper.Party.FirstOrDefault(p => p.CurrentJob() == Jobs.Sage) != null;
}