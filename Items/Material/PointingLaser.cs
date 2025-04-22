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
	private static Asset<Texture2D>? glow;
	public override void Load()
	{
		glow = Mod.Assets.Request<Texture2D>("Items/Material/PointingLaser_Glow");
	}
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
		Item.staff[Item.type] = true;
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
		if (!Main.dedServ)
		{
			Item.GetGlobalItem<ItemGlowmask>().glowTexture = glow;
		}
	}
	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
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
		spriteBatch.Draw(glow.Value, position, frame, c, 0f, origin, scale, SpriteEffects.None, 0f);
	}
}
