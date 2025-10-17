using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Data.Sets;
using Avalon.Items.Weapons.Melee.PreHardmode.MarrowMasher;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.CraniumCrusher;

public class CraniumCrusher : ModItem
{
	public const float ScaleMult = 1.35f;
	public override void SetStaticDefaults()
	{
		ItemSets.Maces[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<CraniumCrusherProj>(), 128, 9.5f, ScaleMult, 30);
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
		velocity = Vector2.Zero;
		if (swing == 1)
		{
			swing = -1;
		}
		else
		{
			swing = 1;
		}
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, swing, Main.LocalPlayer.MountedCenter.AngleTo(Main.MouseWorld));
		return false;
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
public class CraniumCrusherProj : MaceTemplate
{
	public override string Texture => ModContent.GetInstance<CraniumCrusher>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<CraniumCrusher>().DisplayName;
	public override float MaxRotation => MathF.PI + MathHelper.PiOver2;
	public override float? StartRotationLimit => MathHelper.PiOver2;
	public override float SwingRadius => 83f;
	public override Vector2 VisualOffset => new(3, -3);
	public override float ScaleMult => CraniumCrusher.ScaleMult;
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 2.5f);
	public override int TrailLength => 6;
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (hit.Crit)
		{
			target.AddBuff(BuffID.BrokenArmor, TimeUtils.SecondsToTicks(12));
			hit.Knockback *= 3f;
		}
	}
}
