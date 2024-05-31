using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class TroxiniumWaraxe : ModItem
{
	private static Asset<Texture2D> glow;
	public override void SetStaticDefaults()
	{
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
        Item.width = 32;
        Item.height = 36;
        Item.UseSound = SoundID.Item1;
        Item.damage = 45;
        Item.autoReuse = true;
        Item.useTurn = true;
        //Item.scale = 1.2f;
        Item.axe = 22;
        Item.rare = ItemRarityID.Pink;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.knockBack = 6.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 2, 28, 0);
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
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 12)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
