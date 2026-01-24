using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Bows;

public class TroxiniumRepeater : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this, 0);
	}
	public override void SetDefaults()
	{
		Item.DefaultToRepeater(44, 1.5f, 10.5f, 21, 21);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 2, 60);

		Item.GetGlobalItem<ItemGlowmask>().glowOffsetX = -5;
		Item.GetGlobalItem<ItemGlowmask>().glowOffsetY = 0;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 12)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}
}
