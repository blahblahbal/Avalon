using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

public class AccelerationDrill : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsDrill[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToDrill(ModContent.ProjectileType<Projectiles.Tools.AccelerationDrill>(), 400, 25, 7, 6, 9, knockback: 1f, width: 46, height: 16);
		Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
		Item.value = Item.sellPrice(0, 20, 32);
	}
	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
			{

				SoundEngine.PlaySound(SoundID.Unlock, player.position);
				int pfix = Item.prefix;
				Item.ChangeItemType(ModContent.ItemType<AccelerationDrillSpeed>());
				Item.Prefix(pfix);
			}
			if (player.controlUseItem)
			{
				if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
				{
					Tile t = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
					if (TileID.Sets.Ore[t.TileType])
					{
						ClassExtensions.VeinMine(new Point(Player.tileTargetX, Player.tileTargetY), t.TileType);
					}
				}
			}
		}

	}
}

public class AccelerationDrillSpeed : ModItem
{
	public override string Texture => ModContent.GetInstance<AccelerationDrill>().Texture;
	public override void SetDefaults()
	{
		Item.DefaultToDrill(ModContent.ProjectileType<Projectiles.Tools.AccelerationDrill>(), 0, 25, 7, 6, 9, knockback: 1f, width: 46, height: 16);
		Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
		Item.value = Item.sellPrice(0, 20, 32);
	}
	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			if (!Main.GamepadDisableCursorItemIcon && player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetX && (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f >= Player.tileTargetX && player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetY && (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f >= Player.tileTargetY)
			{
				player.cursorItemIconEnabled = true;
				Main.ItemIconCacheUpdate(Type);
			}
			if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
			{
				SoundEngine.PlaySound(SoundID.Unlock, player.position);
				int pfix = Item.prefix;
				Item.ChangeItemType(ModContent.ItemType<AccelerationDrill>());
				Item.Prefix(pfix);
			}
			if (player.controlUseItem)
			{
				if (player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetX && (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f >= Player.tileTargetX && player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetY && (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f >= Player.tileTargetY)
				{
					Point p = Main.MouseWorld.ToTileCoordinates();
					for (int x = p.X - 1; x <= p.X + 1; x++)
					{
						for (int y = p.Y - 1; y <= p.Y + 1; y++)
						{
							if (Main.tile[x, y].HasTile && !Main.tileHammer[Main.tile[x, y].TileType] && !Main.tileAxe[Main.tile[x, y].TileType])
							{
								player.PickTile(x, y, 400);
							}
						}
					}
				}
			}
		}
	}
}
