using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Wands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Wands;

public class FlowerofTheJungle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<JungleFire>(), 22, 5f, 16, 5f, 42);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 0, 60);
		Item.UseSound = SoundID.Item1;
	}
}