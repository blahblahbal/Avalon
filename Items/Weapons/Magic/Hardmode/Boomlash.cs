using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class Boomlash : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponChanneled(ModContent.ProjectileType<Projectiles.Magic.Boomlash>(), 80, 12f, 40, 4f, 30);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 15);
		Item.UseSound = SoundID.Item20;
	}
}
