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
		Item.DefaultToMisc();
		Item.rare = ItemRarityID.Blue;
		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.noUseGraphic = true;
		Item.UseSound = SoundID.Item1;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.shootSpeed = 7f;
		Item.shoot = ModContent.ProjectileType<Projectiles.ThrowingRock>();
	}
}
