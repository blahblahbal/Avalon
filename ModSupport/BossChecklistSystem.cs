using Avalon.Items.Consumables;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Placeable.Trophy.Relics;
using Avalon.Items.Vanity;
using Avalon.NPCs.Bosses.Hardmode;
using Avalon.NPCs.Bosses.PreHardmode;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.ModSupport;

public class BossChecklistSystem : ModSystem
{
	public override void PostSetupContent()
	{
		if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
		{
			return;
		}

		// bacterium prime
		var customPortraitBP = (SpriteBatch sb, Rectangle rect, Color color) =>
		{
			Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/BossChecklist/BacteriumPrimeBossChecklist").Value;
			Vector2 centered = new Vector2(rect.X + rect.Width / 2 - texture.Width / 2, rect.Y + rect.Height / 2 - texture.Height / 2);
			sb.Draw(texture, centered, color);
		};

		bossChecklist.Call(
			"LogBoss",
			Mod,
			nameof(BacteriumPrime),
			3f,
			() => ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime,
			ModContent.NPCType<BacteriumPrime>(),
			new Dictionary<string, object>()
			{
				["spawnItems"] = ModContent.ItemType<InfestedCarcass>(),
				["spawnInfo"] = Language.GetText("Mods.Avalon.BossChecklist.SpawnInfo.BacteriumPrime"),
				["collectibles"] = new List<int> { ModContent.ItemType<BacteriumPrimeMask>(), ModContent.ItemType<BacteriumPrimeTrophy>(), ModContent.ItemType<BacteriumPrimeRelic>() }
				// Other optional arguments as needed...
			}
		);

		// desert beak
		var customPortraitDB = (SpriteBatch sb, Rectangle rect, Color color) =>
		{
			Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/BossChecklist/DesertBeakBossChecklist").Value;
			Vector2 centered = new Vector2(rect.X + rect.Width / 2 - texture.Width / 2, rect.Y + rect.Height / 2 - texture.Height / 2);
			sb.Draw(texture, centered, color);
		};

		bossChecklist.Call(
			"LogBoss",
			Mod,
			nameof(DesertBeak),
			6f,
			() => ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak,
			ModContent.NPCType<DesertBeak>(),
			new Dictionary<string, object>()
			{
				["spawnItems"] = ModContent.ItemType<DesertHorn>(),
				["spawnInfo"] = Language.GetText("Mods.Avalon.BossChecklist.SpawnInfo.DesertBeak"),
				["collectibles"] = new List<int> { ModContent.ItemType<DesertBeakMask>(), ModContent.ItemType<DesertBeakTrophy>(), ModContent.ItemType<DesertBeakRelic>() },
				// Other optional arguments as needed...
			}
		);

		// mechasting
		/* var customPortraitMechasting = (SpriteBatch sb, Rectangle rect, Color color) => {
			 Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/BossChecklist/MechastingBossChecklist").Value;
			 Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
			 sb.Draw(texture, centered, color);
		 };

		 bossChecklist.Call(
			 "LogBoss",
			 Mod,
			 nameof(Mechasting),
			 10.5f,
			 () => ModContent.GetInstance<DownedBossSystem>().DownedMechasting,
			 ModContent.NPCType<Mechasting>(),
			 new Dictionary<string, object>()
			 {
				 ["spawnItems"] = ModContent.ItemType<MechanicalWasp>(),
				 ["spawnInfo"] = Language.GetText("Mods.Avalon.BossChecklist.SpawnInfo.Mechasting"),
				 ["collectibles"] = new List<int> { ModContent.ItemType<MechastingMask>(), ModContent.ItemType<MechastingTrophy>(), ModContent.ItemType<MechastingRelic>() }
				 // Other optional arguments as needed...
			 }
		 );


		 // phantasm
		 var customPortraitPhantasm = (SpriteBatch sb, Rectangle rect, Color color) => {
			 Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/BossChecklist/Phantasm").Value;
			 Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
			 sb.Draw(texture, centered, color);
		 };

		 bossChecklist.Call(
			 "LogBoss",
			 Mod,
			 nameof(Phantasm),
			 19f,
			 () => ModContent.GetInstance<DownedBossSystem>().DownedPhantasm,
			 ModContent.NPCType<Phantasm>(),
			 new Dictionary<string, object>()
			 {
				 ["spawnItems"] = ModContent.ItemType<EctoplasmicBeacon>(),
				 ["spawnInfo"] = Language.GetText("Mods.Avalon.BossChecklist.SpawnInfo.Phantasm"),
				 ["collectibles"] = new List<int> { /*ModContent.ItemType<PhantasmMask>(), ModContent.ItemType<PhantasmTrophy>(), ModContent.ItemType<PhantasmRelic>() }
				 // Other optional arguments as needed...
			 }
		 );*/
	}
}
