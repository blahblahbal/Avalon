using Avalon.Common.Extensions;
using Avalon.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class ColdCatcher : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToFishingPole(ModContent.ProjectileType<ColdCatcherBobber>(), 25, 14f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 2, 80);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
	public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
	{
		lineOriginOffset = new Vector2(46, -33);
		lineColor = new Color(144, 160, 38);
	}
}
