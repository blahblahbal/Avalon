using Avalon.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Superhardmode;

public class MagicCleaver : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponSwing(ModContent.ProjectileType<Projectiles.Magic.MagicCleaver>(), 85, 5f, 16, 20f, 18, true, noUseGraphic: true, width: 20, height: 36);
		Item.rare = ModContent.RarityType<BlueRarity>();
		Item.value = Item.sellPrice(silver: 72);
		Item.UseSound = SoundID.Item1;
	}
}
