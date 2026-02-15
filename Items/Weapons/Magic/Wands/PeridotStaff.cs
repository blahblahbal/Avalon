using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Wands;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Wands;

public class PeridotStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToStaff(ModContent.ProjectileType<PeridotBolt>(), 21, 4.75f, 7, 7.75f, 31, 31, true);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 38);
		Item.UseSound = SoundID.Item43;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 10)
			.AddIngredient(ModContent.ItemType<Material.Ores.Peridot>(), 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
