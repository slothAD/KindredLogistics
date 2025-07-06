using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using VampireCommandFramework;

namespace KindredLogistics;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("gg.deca.VampireCommandFramework")]
public class Plugin : BasePlugin
{
    static Plugin plugin;

    Harmony _harmony;
    public static Harmony Harmony => plugin._harmony;
    public static ManualLogSource LogInstance => plugin.Log;

    public override void Load()
    {
        plugin = this;

        // Plugin startup logic
        Log.LogInfo($"插件 {MyPluginInfo.PLUGIN_GUID} 版本 {MyPluginInfo.PLUGIN_VERSION} 已載入！");

        // Harmony patching
        _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

        // Register all commands in the assembly with VCF
        CommandRegistry.RegisterAll();
    }

    public override bool Unload()
    {
        CommandRegistry.UnregisterAssembly();
        _harmony?.UnpatchSelf();
        return true;
    }
}
