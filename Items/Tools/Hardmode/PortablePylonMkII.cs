using Avalon.Common.Extensions;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Items.Tools.Hardmode;

public class PortablePylonMkII : ModItem
{

	List<Vector2>[] SavedLocations =
	[
		[Vector2.Zero],
		[Vector2.Zero],
		[Vector2.Zero]
	];
	int mode = 0;
	public override void SaveData(TagCompound tag)
	{
		tag["Avalon:PortablePylon2SavedLocations"] = SavedLocations;
	}
	public override void LoadData(TagCompound tag)
	{
		if (tag.ContainsKey("Avalon:PortablePylon2SavedLocations"))
		{
			SavedLocations = tag.Get<List<Vector2>[]>("Avalon:PortablePylon2SavedLocations");
		}
	}

	//private static Asset<Texture2D>? Mode2Texture;
	//private static Asset<Texture2D>? Mode3Texture;
	//public override void SetStaticDefaults()
	//{
	//	Mode2Texture = ModContent.Request<Texture2D>(Texture + "Point2");
	//	Mode3Texture = ModContent.Request<Texture2D>(Texture + "Point3");
	//}
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 25, 25, true);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 3);
		Item.UseSound = SoundID.Item6;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<PortablePylon>())
			.AddIngredient(ItemID.SpectreBar, 16)
			.AddIngredient(ItemID.BeetleHusk, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
	public override void OnCreated(ItemCreationContext context)
	{
		if (PortablePylon.SavedLocations.Contains(new Vector2(Main.worldID, 0)))
		{
			SavedLocations[0].Add(new Vector2(Main.worldID, 0));
			SavedLocations[0].Add(PortablePylon.SavedLocations[PortablePylon.SavedLocations.FindIndex(i => i.X == Main.worldID) + 1]);
		}
	}
	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		var assignedKeys = KeybindSystem.ModeChangeHotkey.GetAssignedKeys();

		var assignedKeyInfo = new TooltipLine(Mod, "Controls:PromptKey", "Press " + (assignedKeys.Count > 0 ? string.Join(", ", assignedKeys) : "[c/565656:<Unbound>]") + " to change waypoint mode");
		tooltips.Add(assignedKeyInfo);
	}
	public override void HoldItem(Player player)
	{
		if (KeybindSystem.ModeChangeHotkey.JustPressed)
		{
			if (++mode > 2) mode = 0;
			Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.WaypointMode") + (mode + 1));
		}
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.altFunctionUse == 2 && player.itemTime == Item.useTime / 2)
		{
			if (SavedLocations[mode].Contains(new Vector2(Main.worldID, 0)))
			{
				SavedLocations[mode].RemoveAt(SavedLocations[mode].FindIndex(i => i.X == Main.worldID) + 1);
				SavedLocations[mode].Remove(new Vector2(Main.worldID, 0));
			}
			SavedLocations[mode].Add(new Vector2(Main.worldID, 0));
			SavedLocations[mode].Add(player.Center + new Vector2(0, -15));
			//WaypointSystem.savedLocation = player.Center + new Vector2(0, -15);
			Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypoint" + (mode + 1)));
		}
		else
		{
			if (player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
			}
			else if (player.itemTime == Item.useTime / 2)
			{
				//if (SavedLocations != null)
				{
					if (SavedLocations[mode].Count > 0 && SavedLocations[mode][SavedLocations[mode].FindIndex(i => i.X == Main.worldID) + 1] != Vector2.Zero)
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
						player.Teleport(SavedLocations[mode][SavedLocations[mode].FindIndex(i => i.X == Main.worldID) + 1]);
						NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, SavedLocations[mode][SavedLocations[mode].FindIndex(i => i.X == Main.worldID) + 1].X, SavedLocations[mode][SavedLocations[mode].FindIndex(i => i.X == Main.worldID) + 1].Y, 0);
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
				//else
				//{
				//	Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.NoWaypointFound"), 250, 0, 0);
				//}
			}
		}
	}

	// todo: make it use the alt textures when used, not just in inv/world
	// or alternatively if keeping the item's form between player loads is desired, make it into 3 separate items like is commented out lower in this file, idk what the deal with that is, blah did it
	//public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	//{
	//	Main.EntitySpriteDraw(mode == 0 ? TextureAssets.Item[Type].Value : mode == 1 ? Mode2Texture.Value : Mode3Texture.Value, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None);
	//	return false;
	//}
	//public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	//{
	//	Main.GetItemDrawFrame(Item.type, out var itemTexture, out var itemFrame);
	//	Vector2 drawOrigin = itemFrame.Size() / 2f;
	//	Vector2 drawPosition = Item.Bottom - Main.screenPosition - new Vector2(0, drawOrigin.Y);

	//	Main.EntitySpriteDraw(mode == 0 ? TextureAssets.Item[Type].Value : mode == 1 ? Mode2Texture.Value : Mode3Texture.Value, drawPosition, itemFrame, lightColor, rotation, drawOrigin, scale, SpriteEffects.None);
	//	return false;
	//}
}
//public class WaypointSystemMkII : ModSystem
//{
//	public static Vector2[] savedLocations = new Vector2[3];
//	public override void OnWorldLoad()
//	{
//		for (int i = 0; i < 3; i++)
//		{
//			savedLocations[i] = Vector2.Zero;
//		}
//	}
//	public override void SaveWorldData(TagCompound tag)
//	{
//		tag["Avalon:PPM2SavedLocations"] = savedLocations;
//	}
//	public override void LoadWorldData(TagCompound tag)
//	{
//		savedLocations = tag.Get<Vector2[]>("Avalon:PPM2SavedLocations");
//	}
//}

//public class PortablePylonMkIIPoint1 : ModItem
//{
//	public override bool IsLoadingEnabled(Mod mod)
//	{
//		return false;
//	}
//	public override void SetStaticDefaults()
//	{
//		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
//	}
//	public override void SetDefaults()
//	{
//		Rectangle dims = this.GetDims();
//		Item.rare = ItemRarityID.Pink;
//		Item.width = dims.Width;
//		Item.useTime = 25;
//		Item.useTurn = true;
//		Item.value = Item.sellPrice(0, 3);
//		Item.useStyle = ItemUseStyleID.HoldUp;
//		Item.useAnimation = 25;
//		Item.height = dims.Height;
//		Item.UseSound = SoundID.Item6;
//	}
//	public override void AddRecipes()
//	{
//		CreateRecipe()
//			.AddIngredient(ModContent.ItemType<PortablePylon>())
//			.AddIngredient(ItemID.SpectreBar, 10)
//			.AddIngredient(ItemID.BeetleHusk, 4)
//			.AddTile(TileID.MythrilAnvil)
//			.Register();
//	}
//	public override bool AltFunctionUse(Player player)
//	{
//		return false;
//	}
//	public override void OnCreated(ItemCreationContext context)
//	{
//		if (WaypointSystem.savedLocation != Vector2.Zero)
//		{
//			WaypointSystemMkII.savedLocations[0] = WaypointSystem.savedLocation;
//		}
//	}
//	public override void HoldItem(Player player)
//	{
//		if (KeybindSystem.ModeChangeHotkey.JustPressed)
//		{
//			WaypointSystemMkII.savedLocations[0] = player.Center + new Vector2(0, -15);
//			Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypointOne"));
//		}
//		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
//		{
//			SoundEngine.PlaySound(SoundID.Unlock, player.position);
//			Item.ChangeItemType(ModContent.ItemType<PortablePylonMkIIPoint2>());
//		}
//	}
//	public override void ModifyTooltips(List<TooltipLine> tooltips)
//	{
//		var assignedKeys = KeybindSystem.ModeChangeHotkey.GetAssignedKeys();

//		var assignedKeyInfo = new TooltipLine(Mod, "Controls:PromptKey", "Press " + (assignedKeys.Count > 0 ? string.Join(", ", assignedKeys) : "[c/565656:<Unbound>]") + " to assign a waypoint");
//		tooltips.Add(assignedKeyInfo);
//	}
//	public override void UseStyle(Player player, Rectangle heldItemFrame)
//	{
//		//if (player.altFunctionUse == 2 && player.itemTime == Item.useTime / 2)
//		//{
//		//    WaypointSystemMkII.savedLocations[0] = player.Center + new Vector2(0, -15);
//		//    Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypointOne"));
//		//}
//		//else
//		{
//			if (player.itemTime == 0)
//			{
//				player.itemTime = Item.useTime;
//			}
//			else if (player.itemTime == Item.useTime / 2)
//			{
//				if (WaypointSystemMkII.savedLocations[0] != Vector2.Zero)
//				{
//					for (int num345 = 0; num345 < 70; num345++)
//					{
//						Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default(Color), 1.5f);
//					}
//					player.grappling[0] = -1;
//					player.grapCount = 0;
//					for (int num346 = 0; num346 < 1000; num346++)
//					{
//						if (Main.projectile[num346].active && Main.projectile[num346].owner == player.whoAmI && Main.projectile[num346].aiStyle == 7)
//						{
//							Main.projectile[num346].Kill();
//						}
//					}
//					player.Teleport(WaypointSystemMkII.savedLocations[0]);
//					NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, WaypointSystemMkII.savedLocations[0].X, WaypointSystemMkII.savedLocations[0].Y, 0);
//					for (int num347 = 0; num347 < 70; num347++)
//					{
//						Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
//					}
//				}
//				else
//				{
//					Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.NoWaypointFound"), 250, 0, 0);
//				}
//			}
//		}
//	}
//}

//public class PortablePylonMkIIPoint2 : ModItem
//{
//	public override bool IsLoadingEnabled(Mod mod)
//	{
//		return false;
//	}
//	public override void SetStaticDefaults()
//	{
//		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
//	}
//	public override void SetDefaults()
//	{
//		Rectangle dims = this.GetDims();
//		Item.rare = ItemRarityID.Pink;
//		Item.width = dims.Width;
//		Item.useTime = 25;
//		Item.useTurn = true;
//		Item.value = Item.sellPrice(0, 3);
//		Item.useStyle = ItemUseStyleID.HoldUp;
//		Item.useAnimation = 25;
//		Item.height = dims.Height;
//		Item.UseSound = SoundID.Item6;
//	}
//	public override bool AltFunctionUse(Player player)
//	{
//		return false;
//	}
//	public override void HoldItem(Player player)
//	{
//		if (KeybindSystem.ModeChangeHotkey.JustPressed)
//		{
//			WaypointSystemMkII.savedLocations[0] = player.Center + new Vector2(0, -15);
//			Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypointTwo"));
//		}
//		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
//		{
//			SoundEngine.PlaySound(SoundID.Unlock, player.position);
//			Item.ChangeItemType(ModContent.ItemType<PortablePylonMkIIPoint3>());
//		}
//	}
//	public override void ModifyTooltips(List<TooltipLine> tooltips)
//	{
//		var assignedKeys = KeybindSystem.ModeChangeHotkey.GetAssignedKeys();

//		var assignedKeyInfo = new TooltipLine(Mod, "Controls:PromptKey", "Press " + (assignedKeys.Count > 0 ? string.Join(", ", assignedKeys) : "[c/565656:<Unbound>]") + " to assign a waypoint");
//		tooltips.Add(assignedKeyInfo);
//	}
//	public override void UseStyle(Player player, Rectangle heldItemFrame)
//	{
//		//if (player.altFunctionUse == 2 && player.itemTime == Item.useTime / 2)
//		//{
//		//    WaypointSystemMkII.savedLocations[1] = player.Center + new Vector2(0, -15);
//		//    Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypointTwo"));
//		//}
//		//else
//		{
//			if (player.itemTime == 0)
//			{
//				player.itemTime = Item.useTime;
//			}
//			else if (player.itemTime == Item.useTime / 2)
//			{
//				if (WaypointSystemMkII.savedLocations[1] != Vector2.Zero)
//				{
//					for (int num345 = 0; num345 < 70; num345++)
//					{
//						Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default(Color), 1.5f);
//					}
//					player.grappling[0] = -1;
//					player.grapCount = 0;
//					for (int num346 = 0; num346 < 1000; num346++)
//					{
//						if (Main.projectile[num346].active && Main.projectile[num346].owner == player.whoAmI && Main.projectile[num346].aiStyle == 7)
//						{
//							Main.projectile[num346].Kill();
//						}
//					}
//					player.Teleport(WaypointSystemMkII.savedLocations[1]);
//					NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, WaypointSystemMkII.savedLocations[1].X, WaypointSystemMkII.savedLocations[1].Y, 0);
//					for (int num347 = 0; num347 < 70; num347++)
//					{
//						Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
//					}
//				}
//				else
//				{
//					Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.NoWaypointFound"), 250, 0, 0);
//				}
//			}
//		}
//	}
//}

//public class PortablePylonMkIIPoint3 : ModItem
//{
//	public override bool IsLoadingEnabled(Mod mod)
//	{
//		return false;
//	}
//	public override void SetStaticDefaults()
//	{
//		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
//	}
//	public override void SetDefaults()
//	{
//		Rectangle dims = this.GetDims();
//		Item.rare = ItemRarityID.Pink;
//		Item.width = dims.Width;
//		Item.useTime = 25;
//		Item.useTurn = true;
//		Item.value = Item.sellPrice(0, 3);
//		Item.useStyle = ItemUseStyleID.HoldUp;
//		Item.useAnimation = 25;
//		Item.height = dims.Height;
//		Item.UseSound = SoundID.Item6;
//	}
//	public override bool AltFunctionUse(Player player)
//	{
//		return false;
//	}
//	public override void HoldItem(Player player)
//	{
//		if (KeybindSystem.ModeChangeHotkey.JustPressed)
//		{
//			WaypointSystemMkII.savedLocations[0] = player.Center + new Vector2(0, -15);
//			Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypointThree"));
//		}
//		if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
//		{
//			SoundEngine.PlaySound(SoundID.Unlock, player.position);
//			Item.ChangeItemType(ModContent.ItemType<PortablePylonMkIIPoint1>());
//		}
//	}
//	public override void ModifyTooltips(List<TooltipLine> tooltips)
//	{
//		var assignedKeys = KeybindSystem.ModeChangeHotkey.GetAssignedKeys();

//		var assignedKeyInfo = new TooltipLine(Mod, "Controls:PromptKey", "Press " + (assignedKeys.Count > 0 ? string.Join(", ", assignedKeys) : "[c/565656:<Unbound>]") + " to assign a waypoint");
//		tooltips.Add(assignedKeyInfo);
//	}
//	public override void UseStyle(Player player, Rectangle heldItemFrame)
//	{
//		//if (player.altFunctionUse == 2 && player.itemTime == Item.useTime / 2)
//		//{
//		//    WaypointSystemMkII.savedLocations[2] = player.Center + new Vector2(0, -15);
//		//    Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypointThree"));
//		//}
//		//else
//		{
//			if (player.itemTime == 0)
//			{
//				player.itemTime = Item.useTime;
//			}
//			else if (player.itemTime == Item.useTime / 2)
//			{
//				if (WaypointSystemMkII.savedLocations[2] != Vector2.Zero)
//				{
//					for (int num345 = 0; num345 < 70; num345++)
//					{
//						Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default, 1.5f);
//					}
//					player.grappling[0] = -1;
//					player.grapCount = 0;
//					for (int num346 = 0; num346 < 1000; num346++)
//					{
//						if (Main.projectile[num346].active && Main.projectile[num346].owner == player.whoAmI && Main.projectile[num346].aiStyle == 7)
//						{
//							Main.projectile[num346].Kill();
//						}
//					}
//					player.Teleport(WaypointSystemMkII.savedLocations[2]);
//					NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, WaypointSystemMkII.savedLocations[2].X, WaypointSystemMkII.savedLocations[2].Y, 0);
//					for (int num347 = 0; num347 < 70; num347++)
//					{
//						Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
//					}
//				}
//				else
//				{
//					Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.NoWaypointFound"), 250, 0, 0);
//				}
//			}
//		}
//	}
//}