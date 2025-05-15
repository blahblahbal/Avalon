using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class TroxiniumPickaxe : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this, 0);
	}
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(185, 30, 5f, 8, 25);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 2, 28);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 18)
			.AddTile(TileID.MythrilAnvil).Register();
	}
}
