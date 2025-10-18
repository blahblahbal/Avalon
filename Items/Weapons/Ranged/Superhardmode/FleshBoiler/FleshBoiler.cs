using Avalon.Common.Extensions;
using Avalon.Items.Ammo;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Superhardmode.FleshBoiler;

public class FleshBoiler : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToFlamethrower(ModContent.ProjectileType<CanisterFire>(), 55, 0.6f, 10f, 4, 20, false);
		Item.useAmmo = ModContent.ItemType<Canister>();
		Item.rare = ModContent.RarityType<BlueRarity>();
		Item.value = Item.sellPrice(0, 20);
	}
	//public override bool CanConsumeAmmo(Item ammo, Player player) => player.itemAnimation >= player.itemAnimationMax - 4;
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		Vector2 muzzleOffset = Vector2.Normalize(velocity) * 54f;
		if (Collision.CanHit(position, 6, 6, position + muzzleOffset, 6, 6))
		{
			position += muzzleOffset;
		}
	}
	public override Vector2? HoldoutOffset() => new Vector2(-7, -3);
}