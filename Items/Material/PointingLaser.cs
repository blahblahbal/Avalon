using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class PointingLaser : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
		Item.staff[Item.type] = true;

		ItemGlowmask.AddGlow(this);
	}
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 30;
		Item.maxStack = Item.CommonMaxStack;
		Item.useAnimation = 2;
		Item.useTime = 2;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.shootSpeed = 6f;
		Item.autoReuse = true;
		Item.channel = true;
		Item.shoot = ModContent.ProjectileType<Projectiles.PointingLaser>();
		Item.rare = ItemRarityID.Pink;

		Item.GetGlobalItem<ItemGlowmask>().CustomPostDrawInWorld = true;
	}
	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		ItemGlowmask.GlowTextures.TryGetValue(Type, out Asset<Texture2D> glow);
		spriteBatch.Draw(glow.Value, position, frame, TeamColor(Main.LocalPlayer), 0f, origin, scale, SpriteEffects.None, 0f);
	}
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		ItemGlowmask.GlowTextures.TryGetValue(Type, out var texture);
		Vector2 vector = texture.Size() / 2f;
		Vector2 value = new((float)(Item.width / 2) - vector.X, Item.height - texture.Height());
		Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
		spriteBatch.Draw(texture.Value, vector2, new Rectangle(0, 0, texture.Width(), texture.Height()), TeamColor(Main.LocalPlayer), rotation, vector, scale, SpriteEffects.None, 0f);
	}
	public static Color TeamColor(Player player)
	{
		Color c = Color.White;
		if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Red || Main.netMode == NetmodeID.SinglePlayer)
		{
			c = new Color(218, 59, 59);
		}
		if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Yellow)
		{
			c = new Color(218, 183, 59);
		}
		if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Green)
		{
			c = new Color(59, 218, 85);
		}
		if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Blue)
		{
			c = new Color(59, 149, 218);
		}
		if (Main.LocalPlayer.team == (int)Terraria.Enums.Team.Pink)
		{
			c = new Color(171, 59, 218);
		}
		return c;
	}
}
