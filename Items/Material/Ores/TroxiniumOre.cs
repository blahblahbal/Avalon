using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class TroxiniumOre : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;

		ItemGlowmask.AddGlow(this, 0);
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.TroxiniumOre>());
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 0, 15);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
}
