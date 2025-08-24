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
        if (!File.Exists(path))
        {
            Instance = new MonkSettings();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<MonkSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new();
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }
    
    public JobViewSave JobViewSave = new()
    {
        QtUnVisibleList = ["双阴起手"]
    }; // QT设置存档
}