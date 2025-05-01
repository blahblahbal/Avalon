using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class DurataniumDrill : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsDrill[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToDrill(ModContent.ProjectileType<Projectiles.Tools.DurataniumDrill>(), 110, 10, 7);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 15)
			.AddTile(TileID.Anvils).Register();
	}
}
