using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class FlowerofTheJungle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<Projectiles.Magic.JungleFire>(), 22, 5f, 16, 5f, 42);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 0, 60);
		Item.UseSound = SoundID.Item1;
	}
}
