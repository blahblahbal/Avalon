using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class SixHundredWattLightbulb : ModItem
{
	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 2);
	}
	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		Lighting.AddLight(Item.position, 1, 0.8f, 0.5f);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[BuffID.Blackout] = true;
	}
}
