using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class TroxiniumChainsaw : ModItem
{
	private static Asset<Texture2D> glow;
	public override void SetStaticDefaults()
	{
		glow = ModContent.Request<Texture2D>(Texture + "_Glow");
	}
	public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 32;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.axe = 20;
        Item.shootSpeed = 40f;
        Item.rare = ItemRarityID.Pink;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 6;
        Item.knockBack = 4.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.TroxiniumChainsaw>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 114000;
        Item.useAnimation = 25;
        Item.height = dims.Height;
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
        float num = Item.velocity.X * 0.2f;
        spriteBatch.Draw(glow.Value, vector2, dims, new Color(255, 255, 255, 0), num, vector, scale, SpriteEffects.None, 0f);
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
