using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.TroxiniumSpear;

public class TroxiniumSpear : ModItem
{
	private static Asset<Texture2D>? glow;
	public override void Load()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");
	}
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<TroxiniumSpearProj>(), 50, 5.6f, 23, 5f);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 2);
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 12)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Vector2 vector = glow.Size() / 2f;
		Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - glow.Height());
		Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
		float num = Item.velocity.X * 0.2f;
		spriteBatch.Draw(glow.Value, vector2, new Rectangle(0, 0, glow.Width(), glow.Height()), new Color(255, 255, 255, 0), num, vector, scale, SpriteEffects.None, 0f);
	}
}
public class TroxiniumSpearProj : SpearTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<TroxiniumSpear>().DisplayName;

	private static Asset<Texture2D> glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");
	}
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = 19;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.scale = 1.3f;
		Projectile.hide = true;
		Projectile.ownerHitCheck = true;
		Projectile.DamageType = DamageClass.Melee;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return lightColor * 4f;
	}
	protected override float HoldoutRangeMax => 180;
	protected override float HoldoutRangeMin => 40;
	public override void PostDraw(Color lightColor)
	{
		SpriteEffects dir = SpriteEffects.None;
		float rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
		Player player = Main.player[Projectile.owner];
		Vector2 origin = Vector2.Zero;
		if (player.direction == 1)
		{
			dir = SpriteEffects.FlipHorizontally;
			origin.X = glow.Value.Width;
			rotation -= MathHelper.PiOver2;
		}
		if (player.gravDir == -1f)
		{
			if (Projectile.direction == 1)
			{
				dir = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
				origin = new Vector2(glow.Value.Width, glow.Value.Height);
				rotation -= MathHelper.PiOver2;
			}
			else if (Projectile.direction == -1)
			{
				dir = SpriteEffects.FlipVertically;
				origin = new Vector2(0f, glow.Value.Height);
				rotation += MathHelper.PiOver2;
			}
		}
		Vector2 basePosition = Projectile.Center + new Vector2(0f, Projectile.gfxOffY);
		Main.EntitySpriteDraw(glow.Value, basePosition - Main.screenPosition, default, new Color(255, 255, 255, 0), rotation, origin, Projectile.scale, dir);
	}
}
