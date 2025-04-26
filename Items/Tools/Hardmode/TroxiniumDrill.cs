using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class TroxiniumDrill : ModItem
{
	private static Asset<Texture2D>? glow;
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsDrill[Type] = true;
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");
	}
	public override void SetDefaults()
	{
		Item.DefaultToDrill(ModContent.ProjectileType<Projectiles.Tools.TroxiniumDrill>(), 185, 24, 4);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 2, 28);
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
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Rectangle dims = this.GetDims();
		Vector2 vector = dims.Size() / 2f;
		Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - dims.Height);
		Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
		spriteBatch.Draw(glow.Value, vector2, dims, new Color(255, 255, 255, 0), rotation, vector, scale, SpriteEffects.None, 0f);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 18)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
