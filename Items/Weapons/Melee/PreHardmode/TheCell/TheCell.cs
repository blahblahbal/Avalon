using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.TheCell;

public class TheCell : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}

	public override void SetDefaults()
	{
		Item.DefaultToFlail(ModContent.ProjectileType<Cell>(), 18, 6.5f, 45, 12f, scale: 1f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 54);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 10).AddIngredient(ModContent.ItemType<Material.Booger>(), 2).AddTile(TileID.Anvils).Register();
	}
}
public class Cell : FlailTemplate
{
	public override int LaunchTimeLimit => 16;
	public override float LaunchSpeed => 14f;
	public override float MaxLaunchLength => 700f;
	public override float RetractAcceleration => 3f;
	public override float MaxRetractSpeed => 13f;
	public override float ForcedRetractAcceleration => 6f;
	public override float MaxForcedRetractSpeed => 16f;
	public override int DefaultHitCooldown => 10;
	public override int SpinHitCooldown => 20;
	public override int MovingHitCooldown => 10;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		base.SetStaticDefaults();
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 20;
		Projectile.height = 20;
	}
	public override bool EmitDust(int dustType, Vector2? posMod, Vector2? velMod, float velMaxRadians, float velMult, int antecedent, int consequent, float fadeIn, bool noGravity, float scale, byte alpha)
	{
		dustType = ModContent.DustType<ContagionWeapons>();
		scale = 1.5f;
		alpha = 128;
		if (CurrentAIState == AIState.Spinning) // The base method does not specify conditions for spawning the dust, so you are able to specify anything here
		{
			consequent = 2;
		}
		else if (Projectile.velocity.Length() <= 3)
		{
			velMaxRadians = MathHelper.TwoPi;
			velMult = 1.5f;
			consequent = 5;
		}
		return base.EmitDust(dustType, posMod, velMod, velMaxRadians, velMult, antecedent, consequent, fadeIn, noGravity, scale, alpha);
	}
}
