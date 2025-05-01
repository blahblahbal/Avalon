using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class EruptionHook : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGrapplingHook(ModContent.ProjectileType<Projectiles.Tools.EruptionHook>(), 14f);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 0, 54);
	}
}
