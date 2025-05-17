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
		Item.useAnimation = 1;
		Item.useTime = 1;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.shootSpeed = 16f;
		Item.autoReuse = true;
		Item.channel = true;
		Item.shoot = ModContent.ProjectileType<Projectiles.PointingLaser>();
		Item.rare = ItemRarityID.Pink;

		Item.GetGlobalItem<ItemGlowmask>().CustomPostDrawInWorld = true;
	}
	public override bool CanShoot(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
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
	public override void PostUpdate()
	{
		Lighting.AddLight(Item.Center, TeamColor(Main.LocalPlayer).ToVector3() * 0.2f);
	}
	public override bool? UseItem(Player player)
	{
		return base.UseItem(player);
	}
	public static Color TeamColor(Player player)
	{
		if (Main.netMode == NetmodeID.SinglePlayer) return new Color(218, 59, 59);

		return player.team switch
		{
			(int)Terraria.Enums.Team.Red => new Color(218, 59, 59),
			(int)Terraria.Enums.Team.Green => new Color(59, 218, 85),
			(int)Terraria.Enums.Team.Blue => new Color(68, 129, 255),
			(int)Terraria.Enums.Team.Yellow => new Color(218, 183, 59),
			(int)Terraria.Enums.Team.Pink => new Color(224, 100, 242),
			_ => Color.White
		};
	}
}
