using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

public class TeamMirror : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 32, 32, useTurn: true, width: 24, height: 28);
		Item.maxStack = 1;
		Item.rare = ItemRarityID.LightPurple;
		Item.value = Item.sellPrice(0, 2);
		Item.UseSound = SoundID.Item6;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.MagicMirror)
			.AddIngredient(ItemID.WormholePotion, 5)
			.AddIngredient(ModContent.ItemType<Material.BloodshotLens>(), 4)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override bool CanUseItem(Player player)
	{
		return true;
	}
	public override void UseStyle(Player player, Rectangle r)
	{
		if (Main.rand.NextBool(2))
		{
			Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default(Color), 1.1f);
		}
		if (player.itemTime == 0)
		{
			player.itemTime = Item.useTime;
		}
		else if (player.itemTime == Item.useTime / 2)
		{
			for (int num345 = 0; num345 < 70; num345++)
			{
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default(Color), 1.5f);
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
			player.Spawn(PlayerSpawnContext.RecallFromItem);
			for (int num347 = 0; num347 < 70; num347++)
			{
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default(Color), 1.5f);
			}
		}
	}
}
