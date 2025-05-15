using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

public class BlahsPicksawTierII : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this, new Color(250, 250, 250, 250));
	}
	public override void SetDefaults()
	{
		Item.DefaultToPickaxeAxe(700, 300, 55, 5.5f, 6, 6, 16, 1.15f);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(gold: 50);
	}
}
