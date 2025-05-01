using Avalon.Common.Extensions;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class CraniumCrusher : ModItem
{
	public float scaleMult = 1.35f; // set this to same as in the projectile file
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<Projectiles.Melee.CraniumCrusher>(), 128, 9.5f, scaleMult, 30);
		Item.ArmorPenetration = 15;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 0, 40);
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
	public override void AddRecipes()
	{
		Recipe.Create(Type).AddTile(TileID.MythrilAnvil)
			.AddIngredient(ModContent.ItemType<MarrowMasher>())
			.AddIngredient(ItemID.Spike, 35)
			.AddIngredient(ItemID.Ectoplasm, 8);
	}
}
