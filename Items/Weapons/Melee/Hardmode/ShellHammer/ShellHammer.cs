using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.ShellHammer;

public class ShellHammer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<Shell>(), 98, 12f, 7f, 50, 35, width: 56, height: 62);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 6, 20);
	}
	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		player.itemLocation = Vector2.Lerp(player.itemLocation, player.MountedCenter, 0.5f);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity.Y -= 3f;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.ChlorophyteBar, 18)
			.AddIngredient(ItemID.TurtleShell)
			.AddIngredient(ModContent.ItemType<VenomShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class Shell : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[Type] = 6;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.width = 32;
		Projectile.height = 24;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = -1;
		Projectile.tileCollide = true;
		Projectile.timeLeft = 900;
		Projectile.damage = 87;
		Projectile.alpha = 255;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 5f)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			Projectile.velocity.Y = -oldVelocity.Y * 0.2f;
		}
		if (Projectile.velocity.X != oldVelocity.X)
		{
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			Projectile.velocity.X = -oldVelocity.X * 0.85f;
		}
		return false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Asset<Texture2D> texture = TextureAssets.Projectile[Type];
		int frameHeight = texture.Value.Height / Main.projFrames[Type];
		Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Value.Width, frameHeight);
		Vector2 drawPos;
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
		{
			drawPos = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2;
			Main.EntitySpriteDraw(texture.Value, drawPos, frame, lightColor * Projectile.Opacity * ((ProjectileID.Sets.TrailCacheLength[Type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Type]) * 0.5f, Projectile.rotation, new Vector2(texture.Value.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
		}
		drawPos = Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(texture.Value, drawPos, frame, lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(texture.Value.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 30; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.WoodFurniture, 0, -2);
			d.noGravity = Main.rand.NextBool();
			d.fadeIn = 1;
			d.velocity += Projectile.velocity * 0.5f;
		}
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
	}
	public override void AI()
	{
		Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha - 20, 0, 255);

		Projectile.velocity.Y += 0.25f;
		if (Projectile.velocity.Y >= 0f)
		{
			int direction = 0;
			if (Projectile.velocity.X < 0f)
			{
				direction = -1;
			}
			if (Projectile.velocity.X > 0f)
			{
				direction = 1;
			}
			Vector2 posMod = Projectile.position;
			posMod.X += Projectile.velocity.X;
			int xPos = (int)((posMod.X + (float)(Projectile.width / 2) + (float)((Projectile.width / 2 + 1) * direction)) / 16f);
			int yPos = (int)((posMod.Y + (float)Projectile.height - 1f) / 16f);
			if (xPos * 16 < posMod.X + Projectile.width &&
			xPos * 16 + 16 > posMod.X &&
			((Main.tile[xPos, yPos].HasUnactuatedTile &&
			!Main.tile[xPos, yPos].TopSlope &&
			!Main.tile[xPos, yPos - 1].TopSlope &&
			Main.tileSolid[Main.tile[xPos, yPos].TileType] &&
			!Main.tileSolidTop[Main.tile[xPos, yPos].TileType]) ||
			(Main.tile[xPos, yPos - 1].IsHalfBlock &&
			Main.tile[xPos, yPos - 1].HasUnactuatedTile)) &&
			(!Main.tile[xPos, yPos - 1].HasUnactuatedTile ||
			!Main.tileSolid[Main.tile[xPos, yPos - 1].TileType] ||
				Main.tileSolidTop[Main.tile[xPos, yPos - 1].TileType] || (Main.tile[xPos, yPos - 1].IsHalfBlock && (!Main.tile[xPos, yPos - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[xPos, yPos - 4].TileType] || Main.tileSolidTop[Main.tile[xPos, yPos - 4].TileType]))) && (!Main.tile[xPos, yPos - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[xPos, yPos - 2].TileType] || Main.tileSolidTop[Main.tile[xPos, yPos - 2].TileType]) && (!Main.tile[xPos, yPos - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xPos, yPos - 3].TileType] || Main.tileSolidTop[Main.tile[xPos, yPos - 3].TileType]) && (!Main.tile[xPos - direction, yPos - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xPos - direction, yPos - 3].TileType]))
			{
				float yPosPixel = yPos * 16;
				if (Main.tile[xPos, yPos].IsHalfBlock)
				{
					yPosPixel += 8f;
				}
				if (Main.tile[xPos, yPos - 1].IsHalfBlock)
				{
					yPosPixel -= 8f;
				}
				if (yPosPixel < posMod.Y + Projectile.height)
				{
					float num101 = posMod.Y + Projectile.height - yPosPixel;
					float num102 = 16.1f;
					if (num101 <= num102)
					{
						Projectile.gfxOffY += Projectile.position.Y + Projectile.height - yPosPixel;
						Projectile.position.Y = yPosPixel - Projectile.height;
						if (num101 < 9f)
						{
							Projectile.stepSpeed = 1f;
						}
						else
						{
							Projectile.stepSpeed = 2f;
						}
					}
				}
			}
		}
		Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
		if (Projectile.velocity.Y <= 6f)
		{
			if (Projectile.velocity.X > 0f && Projectile.velocity.X < 7f)
			{
				Projectile.velocity.X = Projectile.velocity.X + 0.05f;
			}
			if (Projectile.velocity.X < 0f && Projectile.velocity.X > -7f)
			{
				Projectile.velocity.X = Projectile.velocity.X - 0.05f;
			}
		}
		Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
		if (Projectile.frameCounter > 13)
		{
			Projectile.frame += Projectile.direction;
			Projectile.frameCounter = 0;
		}
		if (Projectile.frame > 3)
		{
			Projectile.frame = 0;
		}
		else if (Projectile.frame < 0)
		{
			Projectile.frame = 3;
		}
	}
}
