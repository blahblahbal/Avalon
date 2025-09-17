using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class HallowedThorn : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeapon(26, 28, ModContent.ProjectileType<Projectiles.Magic.HallowedThorn>(), 28, 2f, 20, 32f, 28, 28, true);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(silver: 40);
		Item.UseSound = SoundID.Item8;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.HallowedBar, 8)
			.AddIngredient(ItemID.SoulofFright, 15)
			.AddIngredient(ItemID.LightShard, 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
