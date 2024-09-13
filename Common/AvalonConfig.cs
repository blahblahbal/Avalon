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
    [DefaultValue(true)] // This sets the configs default value.
    [ReloadRequired] // Marking it with [ReloadRequired] makes tModLoader force a mod reload if the option is changed. It should be used for things like item toggles, which only take effect during mod loading
    public bool RevertDungeonGen;

    [DefaultValue(true)]
    [ReloadRequired]
    public bool VanillaTextureReplacement;

    [DefaultValue(false)]
    [ReloadRequired]
    public bool ReducedRespawnTimer;

    [DefaultValue(true)]
    [ReloadRequired]
    public bool VanillaRenames;

    public struct WorldDataValues
    {
        public bool contagion;
        public bool tropics;
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
    [DefaultValue(true)]
    public bool BiomeParticlesEnabled;

    [DefaultValue(true)]
    public bool AdditionalScreenshakes;

	[DefaultValue(false)]
	[ReloadRequired]
	public bool BetaTropicsGen;

	[DefaultValue(false)]
	[ReloadRequired]
	public bool UnimplementedStructureGen;

	[DefaultValue(false)]
	[ReloadRequired]
	public bool SuperhardmodeStuff;
}
