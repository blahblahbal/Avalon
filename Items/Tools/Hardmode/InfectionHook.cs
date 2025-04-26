using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class InfectionHook : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGrapplingHook(ModContent.ProjectileType<Projectiles.Tools.InfectionHook>(), 15f);
		Item.rare = ItemRarityID.LightPurple;
		Item.value = Item.sellPrice(0, 6);
	}
}
