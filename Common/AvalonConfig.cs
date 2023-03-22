using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Avalon.Common;
public class AvalonConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;
    [Header("$Mods.ExxoAvalonOrigins.Config.ItemHeader")] // Headers are like titles in a config. You only need to declare a header on the item it should appear over, not every item in the category.
    [Label("$Mods.ExxoAvalonOrigins.Config.DungeonRevert.Label")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
    [Tooltip("$Mods.ExxoAvalonOrigins.Config.DungeonRevert.Tooltip")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
    [DefaultValue(true)] // This sets the configs default value.
    [ReloadRequired] // Marking it with [ReloadRequired] makes tModLoader force a mod reload if the option is changed. It should be used for things like item toggles, which only take effect during mod loading
    public bool RevertDungeonGen;

    [Label("$Mods.ExxoAvalonOrigins.Config.VanillaTextureReplacement.Label")]
    [Tooltip("$Mods.ExxoAvalonOrigins.Config.VanillaTextureReplacement.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool VanillaTextureReplacement;

    [Label("$Mods.ExxoAvalonOrigins.Config.Renames.Label")]
    [Tooltip("$Mods.ExxoAvalonOrigins.Config.Renames.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool VanillaRenames;

}
public class AvalonClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    [Header("$Mods.ExxoAvalonOrigins.Config.ItemHeader")]
    [Label("$Mods.ExxoAvalonOrigins.Config.Screenshake.Label")]
    [Tooltip("$Mods.ExxoAvalonOrigins.Config.Screenshake.Tooltip")]
    [DefaultValue(true)]
    public bool ScreenshakeEnabled;

    [Label("$Mods.ExxoAvalonOrigins.Config.BiomeParticles.Label")]
    [Tooltip("$Mods.ExxoAvalonOrigins.Config.BiomeParticles.Tooltip")]
    [DefaultValue(true)]
    public bool BiomeParticlesEnabled;
}
