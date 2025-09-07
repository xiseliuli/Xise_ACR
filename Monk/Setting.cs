using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;
namespace Xise.Monk;

public class MonkSettings
{
    public static MonkSettings Instance;
    
    // 基础设置
    public bool ShowToast = true; // 显示Toast通知
    public bool RestoreQtSet = true; // 恢复QT设置
    public int OpenerWindow = 1; // 开场窗口设置 (0=2层震脚, 1=1层震脚)
    
    // 智能战斗设置
    public bool NoBurst = false; // 小怪低血量不交爆发
    public bool PullingNoBurst = false; // 拉怪中不交爆发
    public float MinMobHpPercent = 30.0f; // 小怪血量百分比阈值
    public float MinTTK = 5000.0f; // 最小击杀时间阈值(毫秒)
    public float ConcentrationThreshold = 70.0f; // 敌人集中度阈值
    public float AttackRange = 3.0f; // 攻击范围
    
    private static string path;

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, $"{nameof(MonkSettings)}.json");
        
        // 如果文件不存在，创建默认设置
        if (!File.Exists(path))
        {
            Instance = new MonkSettings();
            Instance.Save();
            return;
        }

        try
        {
            // ✅ 改进异常处理
            var jsonContent = File.ReadAllText(path);
            if (string.IsNullOrEmpty(jsonContent))
            {
                LogHelper.Print("设置文件为空，使用默认设置");
                Instance = new MonkSettings();
                Instance.Save();
                return;
            }
            
            Instance = JsonHelper.FromJson<MonkSettings>(jsonContent);
            if (Instance == null)
            {
                LogHelper.Print("设置解析失败，使用默认设置");
                Instance = new MonkSettings();
                Instance.Save();
            }
        }
        catch (Exception e)
        {
            // ✅ 详细的错误日志记录
            LogHelper.Error($"加载武僧设置失败: {e.Message}");
            LogHelper.Error($"设置文件路径: {path}");
            LogHelper.Error($"异常详情: {e}");
            
            Instance = new();
            Instance.Save();
        }
    }

    public void Save()
    {
        try
        {
            // ✅ 确保目录存在
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            var jsonContent = JsonHelper.ToJson(this);
            File.WriteAllText(path, jsonContent);
        }
        catch (Exception e)
        {
            // ✅ 保存失败时的错误处理
            LogHelper.Error($"保存武僧设置失败: {e.Message}");
            LogHelper.Error($"设置文件路径: {path}");
        }
    }
    
    public JobViewSave JobViewSave = new()
    {
        QtUnVisibleList = []
    }; // QT设置存档

}