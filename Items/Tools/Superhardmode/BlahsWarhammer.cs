using Avalon.Common.Extensions;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

public class BlahsWarhammer : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemGlowmask.AddGlow(this, new Color(250, 250, 250, 250));
	}
	public override void SetDefaults()
	{
		Item.DefaultToHammer(250, 120, 20f, 9, 9, 6, 1.15f, width: 44, height: 48);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
		Item.value = Item.sellPrice(gold: 50);
	}
	public override void HoldItem(Player player)
	{
		if (player.inventory[player.selectedItem].type == Item.type)
		{
			player.wallSpeed += 0.5f;
		}
	}
}
