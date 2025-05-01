using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class MagicGrenade : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<Projectiles.Magic.MagicGrenade>(), 85, 8f, 40, 8f, 22, true, noUseGraphic: true, height: 16, width: 20);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = SoundID.Item1;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.MagicDagger)
			.AddIngredient(ItemID.Grenade, 10)
			.AddIngredient(ItemID.SoulofFright, 20)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
