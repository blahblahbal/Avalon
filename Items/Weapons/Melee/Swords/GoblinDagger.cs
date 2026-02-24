using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Shortsword;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class GoblinDagger : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<GoblinDaggerProj>(), 30, 5f, 11, 2.1f, scale: 0.95f);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 54);
	}
}