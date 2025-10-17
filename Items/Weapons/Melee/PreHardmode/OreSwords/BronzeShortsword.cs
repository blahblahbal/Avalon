using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.OreSwords;

public class BronzeShortsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<BronzeShortswordProj>(), 7, 4f, 12, 2.1f, scale: 0.95f, width: 50, height: 18);
		Item.value = Item.sellPrice(silver: 3);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 5)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
public class BronzeShortswordProj : BismuthShortswordProj
{
	public override LocalizedText DisplayName => ModContent.GetInstance<BronzeShortsword>().DisplayName;
	public override string Texture => ModContent.GetInstance<BronzeShortsword>().Texture;
}
