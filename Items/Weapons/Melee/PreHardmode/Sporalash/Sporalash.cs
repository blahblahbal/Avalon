using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.Sporalash;

public class Sporalash : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}
	public override void SetDefaults()
	{
		Item.DefaultToFlail(ModContent.ProjectileType<SporalashProj>(), 21, 6.75f, 46, 10f);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 54);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.JungleSpores, 15)
			.AddIngredient(ItemID.Stinger, 10)
			.AddIngredient(ItemID.Vine, 2)
			.AddIngredient(ModContent.ItemType<Material.Shards.ToxinShard>(), 2)
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.ThornWhip)
			.Register();
	}
}
public class SporalashProj : FlailTemplate
{
	public override int LaunchTimeLimit => 16;
	public override float LaunchSpeed => 18f;
	public override float MaxLaunchLength => 600f;
	public override float RetractAcceleration => 5f;
	public override float MaxRetractSpeed => 16f;
	public override float ForcedRetractAcceleration => 6f;
	public override float MaxForcedRetractSpeed => 16f;
	public override int DefaultHitCooldown => 10;
	public override int SpinHitCooldown => 20;
	public override int MovingHitCooldown => 10;
	public override int ChainVariants => 16;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 24;
		Projectile.height = 24;
	}

	public override bool EmitDust(int dustType, Vector2? posMod, Vector2? velMod, float velMaxRadians, float velMult, int antecedent, int consequent, float fadeIn, bool noGravity, float scale, byte alpha)
	{
		dustType = DustID.JunglePlants;
		fadeIn = Main.rand.NextFloat(0.9f, 1.3f);
		antecedent = 2;
		consequent = 3;
		if (CurrentAIState == AIState.Spinning) // The base method does not specify conditions for spawning the dust, so you are able to specify anything here
		{
			fadeIn = Main.rand.NextFloat(0.7f, 1.2f);
		}
		else if (Projectile.velocity.Length() <= 3)
		{
			fadeIn = Main.rand.NextFloat(0.7f, 1.2f);
			velMult = 0.5f;
			consequent = 14;
		}
		if (CurrentAIState != AIState.Spinning)
		{
			posMod = Main.rand.NextVector2CircularEdge(12, 12) + Main.rand.NextVector2Circular(6, 6);
		}
		return base.EmitDust(dustType, posMod, velMod, velMaxRadians, velMult, antecedent, consequent, fadeIn, noGravity, scale, alpha);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(BuffID.Poisoned, TimeUtils.SecondsToTicks(3));
		}
		base.ModifyHitNPC(target, ref modifiers);
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(BuffID.Poisoned, TimeUtils.SecondsToTicks(3));
		}
		base.ModifyHitPlayer(target, ref modifiers);
	}
}
