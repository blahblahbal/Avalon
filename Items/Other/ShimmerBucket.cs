using Avalon.Common.Extensions;
using Avalon.Common.Players;
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
		Vector2 pos = player.GetModPlayer<AvalonPlayer>().MousePosition;
		Point tilePos = pos.ToTileCoordinates();
		if (player.IsInTileInteractionRange(tilePos.X, tilePos.Y, TileReachCheckSettings.Simple) && !Main.tile[tilePos.X, tilePos.Y].HasTile &&
			(Main.tile[tilePos.X, tilePos.Y].LiquidAmount == 0 || Main.tile[tilePos.X, tilePos.Y].LiquidType == LiquidID.Shimmer))
		{
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = Type;
			if (player.itemTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
			{
				SoundStyle s = new SoundStyle("Terraria/Sounds/Splash_1");
				SoundEngine.PlaySound(s, player.position);
				Tile t = Main.tile[tilePos.X, tilePos.Y];
				t.LiquidType = LiquidID.Shimmer;
				t.LiquidAmount = 255;

				Item.stack--;
				player.PutItemInInventoryFromItemUsage(ItemID.EmptyBucket, player.selectedItem);
				player.ApplyItemTime(Item);
				WorldGen.SquareTileFrame(tilePos.X, tilePos.Y);
				if (Main.netMode == NetmodeID.MultiplayerClient)
					NetMessage.sendWater(tilePos.X, tilePos.Y);
			}
		}
	}
}
