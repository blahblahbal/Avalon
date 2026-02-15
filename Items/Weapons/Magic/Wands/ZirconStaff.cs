using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Wands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Wands;

public class ZirconStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<ZirconBolt>(), 25, 4.75f, 9, 9.75f, 25, 25, true);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 72);
		Item.UseSound = SoundID.Item43;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 10)
			.AddIngredient(ModContent.ItemType<Material.Ores.Zircon>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}