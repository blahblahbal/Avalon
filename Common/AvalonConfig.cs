using Avalon.ModSupport;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using ReLogic.Content;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
using Microsoft.Xna.Framework;

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
	[CustomModConfigItem(typeof(NeedsReloadIfNoAltLibBooleanElement))]
	public bool BetaTropicsGen;

	[DefaultValue(false)]
	[ReloadRequired]
	public bool UnimplementedStructureGen;

	[DefaultValue(false)]
	[ReloadRequired]
	public bool SuperhardmodeStuff;

	[DefaultValue(true)]
	[ReloadRequired]
	public bool BloodyAmulet;

	public override bool NeedsReload(ModConfig pendingConfig)
	{
		AvalonClientConfig pendingAvalonConfig = (AvalonClientConfig)pendingConfig;
		if (!AltLibrarySupport.Enabled && pendingAvalonConfig.BetaTropicsGen != BetaTropicsGen) return true;
		return base.NeedsReload(pendingConfig);
	}

	internal class NeedsReloadIfNoAltLibBooleanElement : ConfigElement<bool>
	{
		private Asset<Texture2D>? _toggleTexture;

		public override void OnBind()
		{
			base.OnBind();
			_toggleTexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Toggle");

			OnLeftClick += (ev, v) => Value = !Value;
			ShowReloadRequiredTooltip = !AltLibrarySupport.Enabled;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = GetDimensions();
			// "Yes" and "No" since no "True" and "False" translation available
			Terraria.UI.Chat.ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, Value ? Lang.menu[126].Value : Lang.menu[124].Value, new Vector2(dimensions.X + dimensions.Width - 60, dimensions.Y + 8f), Color.White, 0f, Vector2.Zero, new Vector2(0.8f));
			Rectangle sourceRectangle = new Rectangle(Value ? ((_toggleTexture.Width() - 2) / 2 + 2) : 0, 0, (_toggleTexture.Width() - 2) / 2, _toggleTexture.Height());
			Vector2 drawPosition = new Vector2(dimensions.X + dimensions.Width - sourceRectangle.Width - 10f, dimensions.Y + 8f);
			spriteBatch.Draw(_toggleTexture!.Value, drawPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
		}
	}
}
