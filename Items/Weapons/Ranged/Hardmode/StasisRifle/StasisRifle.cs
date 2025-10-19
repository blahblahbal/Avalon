using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode.StasisRifle;

public class StasisRifle : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 32;
        Item.scale = 1f;
        Item.shootSpeed = 24f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.knockBack = 2.3f;
        Item.shoot = ModContent.ProjectileType<StasisRifleHeld>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 7, 0);

        Item.damage = 82;
        Item.useAnimation = 84;
        Item.useTime = 84;
        Item.reuseDelay = 40;
        Item.channel = true;
        Item.noUseGraphic = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.AdamantiteBar, 20)
            .AddIngredient(ItemID.FrostCore, 2)
            .AddIngredient(ItemID.IceBlock, 20)
            .AddIngredient(ItemID.Shotgun, 20)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
public class StasisRifleHeld : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 4;
	}
	public override bool? CanDamage()
	{
		return false;
	}
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(16);
		Projectile.scale = 0.85f;
		Projectile.aiStyle = -1;
		Projectile.timeLeft = 1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Ranged;
	}
	float frame = 0;
	bool Notify;
	public override void AI()
	{
		float Power = 0;
		Player player = Main.player[Projectile.owner];
		Projectile.spriteDirection = player.direction;
		player.heldProj = Projectile.whoAmI;
		if (player.channel)
		{
			Projectile.ai[1] += 0.02f;

			Power = MathHelper.Clamp(Projectile.ai[1], 0, 1);
			Projectile.timeLeft = 60;
			player.SetDummyItemTime(20);
			if (player.whoAmI == Main.myPlayer)
			{
				Projectile.velocity = player.Center.DirectionTo(Main.MouseWorld);
				player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
				Projectile.position.X -= player.direction * 6;
			}
		}
		if (Power == 1 && Main.myPlayer == player.whoAmI && !Notify)
		{
			Notify = true;
			SoundEngine.PlaySound(SoundID.MaxMana);
		}
		else
		{
			if (Projectile.timeLeft == 59)
			{
				SoundEngine.PlaySound(SoundID.Item75, player.position);
				for (int i = 0; i < 10; i++)
				{
					Dust d = Dust.NewDustDirect(Projectile.Center + new Vector2(24, 0).RotatedBy(Projectile.rotation) + new Vector2(0, -8), 0, 0, DustID.FrostStaff);
					d.velocity *= 1.3f;
					d.velocity += Projectile.velocity * 3;
					d.noGravity = true;
				}
				//Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center + Projectile.velocity * 40 + new Vector2(0,-4), Projectile.velocity * player.HeldItem.shootSpeed, ModContent.ProjectileType<StasisShot>(), (Projectile.damage * Power) + 1, Projectile.knockBack, Projectile.owner);
			}
		}
		Projectile.rotation = Projectile.velocity.ToRotation();

		Vector2 vector = Vector2.Normalize(Projectile.velocity) * 20;
		Projectile.Center = player.Center + new Vector2(vector.X, vector.Y * 1f);
		player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, new Vector2(player.Center.X + player.direction * -6, player.Center.Y).DirectionTo(Projectile.Center).ToRotation() - MathHelper.PiOver2);
		if (player.channel)
			frame += Power;
		else
			frame += Projectile.timeLeft / 60f;

		if (frame > 2)
		{
			Projectile.frame++;
			frame = 0;
		}
		if (Projectile.frame > 3)
			Projectile.frame = 0;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		SpriteEffects Flip = Projectile.direction == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
		int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Projectile.type];
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
		Vector2 drawPos = Projectile.Center - Main.screenPosition;
		drawPos.Y += Main.player[Projectile.owner].gfxOffY;
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos + new Vector2(0, -4), frame, lightColor, Projectile.rotation, new Vector2(TextureAssets.Projectile[Type].Value.Width, frameHeight) / 2, Projectile.scale, Flip, 0);
		return false;
	}
}
public class StasisShot : ModProjectile
{
	public override string Texture => ModContent.GetInstance<StasisRifleHeld>().Texture;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(6);
		Projectile.hide = true;
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
		Projectile.timeLeft = 60 * 2;
	}

	public override void AI()
	{
		if (Projectile.ai[1] == 0f)
		{
			Projectile.oldPosition = Projectile.position;
			Projectile.ai[1] = 1f;
		}
		Projectile.velocity.Y += 0.1f;
		for (float i = 0; i < 1; i += 0.2f)
		{
			var dust = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center, Projectile.oldPosition + (Projectile.Size / 2), i), DustID.IceTorch, Vector2.Zero);
			dust.noGravity = true;
			dust.scale = 1;
		}
	}
	public override void OnKill(int timeLeft)
	{
		base.OnKill(timeLeft);
	}
}
