using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;
namespace Xise.Monk;

public class MonkSettings
{
    public static MonkSettings Instance;
    
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