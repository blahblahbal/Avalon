using Avalon.Common.Extensions;
using Avalon.Projectiles.Magic.Wands;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Wands;

public class Boomlash : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeaponChanneled(ModContent.ProjectileType<BoomlashProj>(), 80, 12f, 40, 4f, 30);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 15);
		Item.UseSound = SoundID.Item20;
	}
}
