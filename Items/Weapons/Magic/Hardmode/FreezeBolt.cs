using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class FreezeBolt : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Projectiles.Magic.FreezeBolt>(), 43, 5f, 11, 7f, 17, 17);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 1);
		Item.UseSound = SoundID.Item21;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.WaterBolt)
			.AddIngredient(ModContent.ItemType<Material.SoulofIce>(), 20)
			.AddIngredient(ItemID.FrostCore, 2)
			.AddTile(TileID.Bookcases)
			.Register();
	}
}
