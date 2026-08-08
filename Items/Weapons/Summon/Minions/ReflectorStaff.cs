using Avalon.Buffs.Summons;
using Avalon.Common.Extensions;
using Avalon.Projectiles.Summon.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Minions;

public class ReflectorStaff : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMinionWeapon(ModContent.ProjectileType<Reflector>(), ModContent.BuffType<ReflectorBuff>(), 0, 8.5f, 30);
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 30);
		Item.UseSound = SoundID.Item44;
	}
	public override bool CanUseItem(Player player)
	{
		if (player.maxMinions > 6)
			return player.ownedProjectileCounts[Item.shoot] < 6;
		return player.ownedProjectileCounts[Item.shoot] < player.maxMinions;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		player.AddBuff(Item.buffType, 2);
		player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);
		return false;
	}
}
