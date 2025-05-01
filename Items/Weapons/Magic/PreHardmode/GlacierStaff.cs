using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class GlacierStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<Projectiles.Magic.GlacierBall>(), 27, 5f, 10, 10f, 50, 50, width: 32, height: 32);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.buyPrice(0, 3, 50);
		Item.UseSound = SoundID.Item43;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 200);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.IceBlock, 50)
			.AddIngredient(ModContent.ItemType<Icicle>(), 50)
			.AddIngredient(ItemID.FallenStar, 8)
			.AddIngredient(ModContent.ItemType<FrostShard>(), 4)
			.AddTile(TileID.IceMachine)
			.Register();
	}
}
