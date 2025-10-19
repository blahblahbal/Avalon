using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Avalon.Items.Weapons.Ranged.PreHardmode.Icicle;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode.FrozenLyre;

public class FrozenLyre : ModItem
{
	public SoundStyle note = new("Terraria/Sounds/Item_26")
	{
		Volume = 1f,
		Pitch = 0f,
		PitchVariance = 0.5f,
		MaxInstances = 10,
	};
	public override void SetDefaults()
	{
		Item.DefaultToMagicWeapon(30, 24, ModContent.ProjectileType<IceNote>(), 16, 1f, 4, 6f, 20, 20, true);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 40, 0);
		Item.holdStyle = ItemHoldStyleID.HoldHeavy;
		Item.UseSound = note;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6, 0);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddRecipeGroup("IronBar", 4)
			.AddIngredient(ModContent.ItemType<Icicle>(), 50)
			.AddIngredient(ItemID.FallenStar, 8)
			.AddIngredient(ModContent.ItemType<FrostShard>(), 4)
			.AddTile(TileID.IceMachine)
			.Register();
	}
}
public class IceNote : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.penetrate = 3;
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.timeLeft = 180;
		Projectile.scale = 1f;
		Projectile.coldDamage = true;
		DrawOriginOffsetY -= 2;
	}
	public SoundStyle note = new SoundStyle("Terraria/Sounds/Item_26")
	{
		Volume = 1f,
		Pitch = 0f,
		PitchVariance = 0.5f,
		MaxInstances = 10,
	};
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 10);
	}
	public int Timer;
	public override void AI()
	{
		Projectile.spriteDirection = -Projectile.direction;
		if (Main.rand.NextBool(2))
		{
			int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92, 0f, 0f, default, default, 1.2f);
			Dust dust2 = Main.dust[dust1];
			dust2.velocity *= 0f;
			dust2.noGravity = true;
		}
		Timer++;
		if (Timer <= 10)
		{
			Projectile.scale *= 1.03f;
		}
		if (Timer >= 10)
		{
			Projectile.scale *= 0.97f;
		}
		if (Timer == 20)
		{
			Timer = 0;
			Projectile.scale = 1f;
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.penetrate--;
		if (Projectile.penetrate <= 0)
		{
			Projectile.Kill();
		}
		if (Projectile.velocity.X != oldVelocity.X)
		{
			Projectile.velocity.X = -oldVelocity.X;
		}
		if (Projectile.velocity.Y != oldVelocity.Y)
		{
			Projectile.velocity.Y = -oldVelocity.Y;
		}
		return false;
	}
	public SoundStyle Icenote = new SoundStyle("Terraria/Sounds/Item_27")
	{
		Volume = 0.8f,
		Pitch = -0.25f,
		PitchVariance = 0f,
		MaxInstances = 10,
	};
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(Icenote, Projectile.Center);

		for (int num237 = 0; num237 < 5; num237++)
		{
			int num239 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 92, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f, default, default, 1.2f);
			Dust dust30 = Main.dust[num239];
			dust30.noGravity = true;
		}
	}
}