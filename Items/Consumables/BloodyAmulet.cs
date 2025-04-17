using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class BloodyAmulet : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 3;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpawner(useAnim: 15, useTime: 30);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item4;
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.BloodyAmulet>();
	}
}
