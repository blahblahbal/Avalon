using Avalon.Common.Extensions;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode.PurpleHaze;

public class PurpleHaze : ModItem
{
	public SoundStyle gas = new("Terraria/Sounds/Item_34")
	{
		Volume = 0.5f,
		Pitch = -0.5f,
		PitchVariance = 1.5f,
		MaxInstances = 10,
	};
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<PurpleHazeProj>(), 26, 1.5f, 5, 8f, 7, 21);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 4);
		Item.UseSound = gas;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(10, 0);
	}

	public override void AddRecipes()
	{
		Recipe.Create(Type).AddIngredient(ItemID.SpellTome).AddIngredient(ModContent.ItemType<Pathogen>(), 20).AddIngredient(ItemID.SoulofNight, 15).AddTile(TileID.Bookcases).Register();
	}
}
public class PurpleHazeProj : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<PurpleHaze>().DisplayName;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NoLiquidDistortion[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.width = 36;
		Projectile.height = 36;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.alpha = 128;
		Projectile.friendly = true;
		Projectile.timeLeft = 720;
		Projectile.ignoreWater = true;
		Projectile.hostile = false;
		Projectile.scale = 0.4f;
		Projectile.extraUpdates = 1;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		Projectile.ai[1]++;
		if (Projectile.ai[2] > 1)
		{
			Projectile.alpha += 1;
			if (Projectile.ai[1] % 10 == 0)
			{
				Projectile.damage--;
			}
		}
		else
			Projectile.alpha -= 3;

		if (Projectile.alpha <= 100)
			Projectile.ai[2]++;

		if (Projectile.alpha == 255) Projectile.Kill();

		Projectile.velocity = Projectile.velocity.RotatedByRandom(0.1f) * 0.985f;
		Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.03f, -0.3f, 0.3f);
		Projectile.scale += 0.03f;
		Projectile.Resize((int)(32 * Projectile.scale), (int)(32 * Projectile.scale));
		Lighting.AddLight(Projectile.Center, new Vector3(0.4f, 0.2f, 0.5f) * Projectile.scale * Projectile.Opacity * 0.3f);
	}
	public override bool? CanHitNPC(NPC target)
	{
		return (Projectile.alpha < 220 || Projectile.ai[2] < 1) && !target.friendly;
	}

	public override bool CanHitPvp(Player target)
	{
		return Projectile.alpha < 220;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Pathogen>(), 7 * 60);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Pathogen>(), 7 * 60);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		ClassExtensions.DrawGas(Texture, lightColor * 0.8f, Projectile, 4, 6);
		return false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.velocity = Projectile.oldVelocity * 0.7f;
		return false;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
}

