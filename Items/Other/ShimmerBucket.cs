using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class ShimmerBucket : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToUseable(false, 15, 10);
		Item.rare = ItemRarityID.Orange;
	}
	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			Point tilePos = Main.MouseWorld.ToTileCoordinates();
			Tile tile = Main.tile[tilePos];

			if (player.IsInTileInteractionRange(tilePos.X, tilePos.Y, TileReachCheckSettings.Simple) && !tile.HasTile &&
				(tile.LiquidAmount == 0 || tile.LiquidType == LiquidID.Shimmer))
			{
				player.cursorItemIconEnabled = true;
				player.cursorItemIconID = Type;
				if (player.itemTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
				{
					SoundEngine.PlaySound(SoundID.SplashWeak, player.position);
					tile.LiquidType = LiquidID.Shimmer;
					tile.LiquidAmount = 255;

					Item.stack--;
					player.PutItemInInventoryFromItemUsage(ItemID.EmptyBucket, player.selectedItem);
					player.ApplyItemTime(Item);
					WorldGen.SquareTileFrame(tilePos.X, tilePos.Y);
					if (Main.netMode == NetmodeID.MultiplayerClient)
					{
						NetMessage.sendWater(tilePos.X, tilePos.Y);
					}
				}
			}
		}
	}
}
