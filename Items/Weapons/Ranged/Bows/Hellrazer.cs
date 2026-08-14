using Avalon.Common.Extensions;
using Avalon.Core;
using Avalon.Projectiles.Ranged.Bows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Bows;
public class Hellrazer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGun(70, 12f, 10.5f, 30, 30, true, crit: 10);
		Item.useAmmo = AmmoID.Stake;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 30, 0, 0);
		Item.UseSound = Sounds.Item.HellrazerShot.Asset with { pitchVariance = 0.2f, pitch = -0.4f, volume = 0.8f};
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-8, -4);
	}
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		var texture = AssetReferences.Items.Weapons.Ranged.Bows.Hellrazer_Glow.Asset;
		Vector2 vector = texture.Size() / 2f;
		Vector2 value = new((float)(Item.width / 2) - vector.X, Item.height - texture.Height());
		Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
		DrawData drawData = new(texture.Value, vector2, new Rectangle(0, 0, texture.Width(), texture.Height()), Color.White with { A = 128 }, rotation, vector, scale, SpriteEffects.None, 0f);
		float glow = (float)(Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.25f + 0.75f);
		Main.EntitySpriteDraw(drawData);
		for (int i = 0; i < 4; i++)
		{
			Main.EntitySpriteDraw(drawData with { position = drawData.position + new Vector2(0, 4 * glow).RotatedBy(i * MathHelper.PiOver2 + (float)Main.timeForVisualEffects * 0.03f), texture = AssetReferences.Items.Weapons.Ranged.Bows.Hellrazer_Glow.Asset.Value, color = Color.OrangeRed with { A = 64 } * 0.5f * glow });
		}
		Main.EntitySpriteDraw(drawData with { texture = AssetReferences.Items.Weapons.Ranged.Bows.Hellrazer_Glow.Asset.Value, color = Color.White with { A = 128 } });


		Lighting.AddLight(Item.Bottom, new Vector3(60 / 255f, 35 / 255f, 5 / 255f));
	}
	public override bool ModifyItemDraw(ref PlayerDrawSet drawInfo, ref DrawData drawData, ref DrawData? coloredDrawData, ref DrawData? glowMaskDrawData)
	{
		float glow = (float)(Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.25f + 0.75f);
		drawInfo.DrawDataCache.Add(drawData);
		for(int i = 0; i < 4; i++)
		{
			drawInfo.DrawDataCache.Add(drawData with {position = drawData.position + new Vector2(0,4 * glow).RotatedBy(i * MathHelper.PiOver2 + (float)Main.timeForVisualEffects * 0.03f), texture = AssetReferences.Items.Weapons.Ranged.Bows.Hellrazer_Glow.Asset.Value, color = Color.OrangeRed with { A = 64 } * 0.5f * glow});
		}
		drawInfo.DrawDataCache.Add(drawData with { texture = AssetReferences.Items.Weapons.Ranged.Bows.Hellrazer_Glow.Asset.Value, color = Color.White with { A = 128 } });
		return false;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		type = ModContent.ProjectileType<HellrazerStake>();
	}
}
