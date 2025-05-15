using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class TroxiniumWaraxe : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this, 0);
	}
	public override void SetDefaults()
	{
		Item.DefaultToAxe(105, 45, 6.5f, 8, 35);
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
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 12)
			.AddTile(TileID.MythrilAnvil).Register();
	}
}
