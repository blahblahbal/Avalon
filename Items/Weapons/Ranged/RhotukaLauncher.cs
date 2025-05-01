using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

public class RhotukaLauncher : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetStaticDefaults()
	{
		Item.staff[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToRangedWeapon(26, 32, ModContent.ProjectileType<Projectiles.Ranged.RhotukaSpinnerScrambler>(), ModContent.ItemType<Ammo.RhotukaSpinner>(), 30, 0f, 14f, 22, 22);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 2);
		Item.UseSound = SoundID.Item39;
	}
	public override Vector2? HoldoutOffset() => new Vector2(-10, 0);
}
