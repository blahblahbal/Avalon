using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Terraria.ModLoader.Config;

namespace Avalon.Common;
public class AvalonConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;
    [Header("$Mods.Avalon.Config.ItemHeader")] // Headers are like titles in a config. You only need to declare a header on the item it should appear over, not every item in the category.
    [Label("$Mods.Avalon.Config.DungeonRevert.Label")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
    [Tooltip("$Mods.Avalon.Config.DungeonRevert.Tooltip")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
    [DefaultValue(true)] // This sets the configs default value.
    [ReloadRequired] // Marking it with [ReloadRequired] makes tModLoader force a mod reload if the option is changed. It should be used for things like item toggles, which only take effect during mod loading
    public bool RevertDungeonGen;

    [Label("$Mods.Avalon.Config.VanillaTextureReplacement.Label")]
    [Tooltip("$Mods.Avalon.Config.VanillaTextureReplacement.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool VanillaTextureReplacement;

    [Label("$Mods.Avalon.Config.Renames.Label")]
    [Tooltip("$Mods.Avalon.Config.Renames.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool VanillaRenames;

    public struct WorldDataValues
    {
        public bool contagion;
    }

    // Key value is each twld path
    [DefaultListValue(false)]
    [JsonProperty]
    private Dictionary<string, WorldDataValues> worldData = new Dictionary<string, WorldDataValues>();

    // Methods to avoid public variable getting picked up by serialiser
    public Dictionary<string, WorldDataValues> GetWorldData() { return worldData; }
    public void SetWorldData(Dictionary<string, WorldDataValues> newDict) { worldData = newDict; }
    public static void Save(ModConfig config)
    {
        Directory.CreateDirectory(ConfigManager.ModConfigPath);
        string filename = config.Mod.Name + "_" + config.Name + ".json";
        string path = Path.Combine(ConfigManager.ModConfigPath, filename);
        string json = JsonConvert.SerializeObject((object)config, ConfigManager.serializerSettings);
        File.WriteAllText(path, json);
    }
}
public class AvalonClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    [Header("$Mods.Avalon.Config.ItemHeader")]
    [Label("$Mods.Avalon.Config.BiomeParticles.Label")]
    [Tooltip("$Mods.Avalon.Config.BiomeParticles.Tooltip")]
    [DefaultValue(true)]
    public bool BiomeParticlesEnabled;
}
