using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Items.Tools.PreHardmode;

public class WaypointSystem : ModSystem
{
	public static Vector2 savedLocation;
	public override void OnWorldLoad()
	{
		savedLocation = Vector2.Zero;
	}
	public override void SaveWorldData(TagCompound tag)
	{
		tag["Avalon:PPSavedLocation"] = savedLocation;
	}
	public override void LoadWorldData(TagCompound tag)
	{
		savedLocation = tag.Get<Vector2>("Avalon:PPSavedLocation");
	}
}
public class PortablePylon : ModItem
{
	public static List<Vector2> SavedLocations = new List<Vector2>();
	public override void SaveData(TagCompound tag)
	{
		tag["Avalon:PortablePylonSavedLocations"] = SavedLocations;
	}
	public override void LoadData(TagCompound tag)
	{
		if (tag.ContainsKey("Avalon:PortablePylonSavedLocations"))
		{
			SavedLocations = tag.Get<List<Vector2>>("Avalon:PortablePylonSavedLocations");
		}
	}

	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 25, 25, true);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 2);
		Item.UseSound = SoundID.Item6;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.PotionOfReturn, 5)
			.AddIngredient(ItemID.MagicMirror)
			.AddIngredient(ItemID.HellstoneBar, 8)
			.AddTile(TileID.Anvils)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.PotionOfReturn, 5)
			.AddIngredient(ItemID.IceMirror)
			.AddIngredient(ItemID.HellstoneBar, 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.altFunctionUse == 2 && player.itemTime == Item.useTime / 2)
		{
			if (SavedLocations.Contains(new Vector2(Main.worldID, 0)))
			{
				SavedLocations.RemoveAt(SavedLocations.FindIndex(i => i.X == Main.worldID) + 1);
				SavedLocations.Remove(new Vector2(Main.worldID, 0));
			}
			SavedLocations.Add(new Vector2(Main.worldID, 0));
			SavedLocations.Add(player.Center + new Vector2(0, -15));
			//WaypointSystem.savedLocation = player.Center + new Vector2(0, -15);
			Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypoint"));
		}
		else
		{
			if (player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
			}
			else if (player.itemTime == Item.useTime / 2)
			{
				if (SavedLocations.Count > 0 && SavedLocations[SavedLocations.FindIndex(i => i.X == Main.worldID) + 1] != Vector2.Zero)
				{
					for (int num345 = 0; num345 < 70; num345++)
					{
						Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default, 1.5f);
					}
					player.grappling[0] = -1;
					player.grapCount = 0;
					for (int num346 = 0; num346 < 1000; num346++)
					{
						if (Main.projectile[num346].active && Main.projectile[num346].owner == player.whoAmI && Main.projectile[num346].aiStyle == 7)
						{
							Main.projectile[num346].Kill();
						}
					}
					player.Teleport(SavedLocations[SavedLocations.FindIndex(i => i.X == Main.worldID) + 1]);
					NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, SavedLocations[SavedLocations.FindIndex(i => i.X == Main.worldID) + 1].X, SavedLocations[SavedLocations.FindIndex(i => i.X == Main.worldID) + 1].Y, 0);
					for (int num347 = 0; num347 < 70; num347++)
					{
						Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
					}
				}
				else
				{
					Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.NoWaypointFound"), 250, 0, 0);
				}
			}
		}
	}
}
