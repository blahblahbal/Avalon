using Avalon.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class SilenceCandle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.SilenceCandle>());
		Item.width = 8;
		Item.height = 18;
		Item.value = Item.sellPrice(silver: 3);
		Item.noWet = true;
		Item.flame = true;
		Item.holdStyle = ItemHoldStyleID.HoldFront;
	}

	//public override void AddRecipes()
	//{
	//    CreateRecipe()
	//        .AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>())
	//        .AddIngredient(ItemID.Torch)
	//        .AddTile(TileID.WorkBenches).Register();
	//}

	public override void HoldItem(Player player)
	{
		if (!player.wet && !player.pulley)
		{
			int maxValue2 = 20;
			if (player.itemAnimation > 0)
			{
				maxValue2 = 7;
			}
			if (player.direction == -1)
			{
				if (Main.rand.NextBool(maxValue2))
				{
					int num52 = Dust.NewDust(new Vector2(player.itemLocation.X - 12f, player.itemLocation.Y - 20f * player.gravDir), 4, 4, DustID.Torch, 0f, 0f, 100);
					if (!Main.rand.NextBool(3))
					{
						Main.dust[num52].noGravity = true;
					}
					Main.dust[num52].velocity *= 0.3f;
					Main.dust[num52].velocity.Y -= 1.5f;
					Main.dust[num52].position = player.RotatedRelativePoint(Main.dust[num52].position);
				}
				Lighting.AddLight(player.RotatedRelativePoint(new Vector2(player.itemLocation.X - 16f + player.velocity.X, player.itemLocation.Y - 14f)), 1f, 0.95f, 0.8f);
			}
			else
			{
				if (Main.rand.NextBool(maxValue2))
				{
					int num53 = Dust.NewDust(new Vector2(player.itemLocation.X + 4f, player.itemLocation.Y - 20f * player.gravDir), 4, 4, DustID.Torch, 0f, 0f, 100);
					if (!Main.rand.NextBool(3))
					{
						Main.dust[num53].noGravity = true;
					}
					Main.dust[num53].velocity *= 0.3f;
					Main.dust[num53].velocity.Y -= 1.5f;
					Main.dust[num53].position = player.RotatedRelativePoint(Main.dust[num53].position);
				}
				Lighting.AddLight(player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 6f + player.velocity.X, player.itemLocation.Y - 14f)), 1f, 0.95f, 0.8f);
			}
			player.AddBuff(ModContent.BuffType<SilenceCandleBuff>(), 2);
		}
	}
}
