using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class Rock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToUseable(false, 25, 25, false, 16, 16);
		Item.UseSound = SoundID.Item1;
		Item.noUseGraphic = true;
		Item.shootSpeed = 7f;
		Item.rare = ItemRarityID.Blue;
		Item.shoot = ModContent.ProjectileType<Projectiles.ThrowingRock>();
	}
}
