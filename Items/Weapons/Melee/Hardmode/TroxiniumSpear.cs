using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

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
		Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.Melee.TroxiniumSpear>(), 50, 5.6f, 23, 5f);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 2);
		//if (!Main.dedServ)
		//{
		//    Item.GetGlobalItem<ItemGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
		//}
		//Item.GetGlobalItem<ItemGlowmask>().glowAlpha = 0;
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
