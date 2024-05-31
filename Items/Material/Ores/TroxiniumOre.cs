using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

class TroxiniumOre : ModItem
{
    private static Asset<Texture2D> glow;
	public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");
    }
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Vector2 vector = glow.Size() / 2f;
		Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - glow.Height());
		Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
		float num = Item.velocity.X * 0.2f;
		spriteBatch.Draw(glow.Value, vector2, new Rectangle(0, 0, glow.Width(), glow.Height()), new Color(255, 255, 255, 0), num, vector, scale, SpriteEffects.None, 0f);
	}

	public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Ores.TroxiniumOre>();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 15);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = glow.Value;
        }
        Item.GetGlobalItem<ItemGlowmask>().glowAlpha = 0;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor * 4f;
    }
    //public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    //{
    //    Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
    //    spriteBatch.Draw
    //    (
    //        texture,
    //        new Vector2
    //        (
    //            Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
    //            Item.position.Y - Main.screenPosition.Y + Item.height * 0.5f
    //        ),
    //        new Rectangle(0, 0, texture.Width, texture.Height),
    //        Color.White,
    //        rotation,
    //        texture.Size() * 0.5f,
    //        scale,
    //        SpriteEffects.None,
    //        0f
    //    );
    //}
}
