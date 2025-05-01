using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class MarrowMasher : ModItem
{
	public float scaleMult = 1.25f; // set this to same as in the projectile file
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<Projectiles.Melee.MarrowMasher>(), 58, 6.9f, scaleMult, 30);
		Item.ArmorPenetration = 15;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 40);
	}
	public override bool MeleePrefix()
	{
		return true;
	}

	public int swing;
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		Rectangle dims = this.GetDims();
		float posMult = 1 + (dims.Height * scaleMult - 26) / 26 * 0.1f;
		velocity = Vector2.Zero;
		int height = dims.Height;
		if (player.gravDir == -1)
		{
			height = -dims.Height;
		}
		if (swing == 1)
		{
			swing--;
			position = player.Center + new Vector2(0, height * Item.scale * posMult);
		}
		else
		{
			swing++;
			position = player.Center + new Vector2(0, -height * Item.scale * posMult);
		}
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
