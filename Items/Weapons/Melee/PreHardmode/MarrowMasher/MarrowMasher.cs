using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Data.Sets;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.MarrowMasher;

public class MarrowMasher : ModItem
{
	public const float ScaleMult = 1.25f;
	public override void SetStaticDefaults()
	{
		ItemSets.Maces[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<MarrowMasherProj>(), 58, 6.9f, ScaleMult, 30);
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
}
public class MarrowMasherProj : MaceTemplate
{
	public override string Texture => ModContent.GetInstance<MarrowMasher>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<MarrowMasher>().DisplayName;
	public override float MaxRotation => MathF.PI + MathHelper.PiOver4;
	public override float? StartRotationLimit => MathHelper.PiOver2;
	public override float SwingRadius => 58f;
	public override float ScaleMult => MarrowMasher.ScaleMult;
	public override float EndScaleTime => 0.167f;
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 2f);
	public override int TrailLength => 4;
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (hit.Crit)
		{
			hit.Knockback *= 1.5f;
		}
	}
}
