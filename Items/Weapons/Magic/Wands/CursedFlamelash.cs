using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Wands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Wands;

public class CursedFlamelash : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponChanneled(ModContent.ProjectileType<CursedFlamelashProj>(), 40, 4f, 17, 6f, 23);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
		Item.UseSound = SoundID.Item20;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Flamelash)
			.AddIngredient(ItemID.CursedFlame, 30)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}