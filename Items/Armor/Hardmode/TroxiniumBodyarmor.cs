using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
class TroxiniumBodyarmor : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 15;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 2, 60);
        Item.height = dims.Height;
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ArmorGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Body_Glow").Value;
        }
        Item.GetGlobalItem<ArmorGlowmask>().glowAlpha = 0;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor * 4f;
    }
    public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
    {
        if (!Main.gameMenu)
        {
            color = Color.White * (Lighting.Brightness((int)drawPlayer.position.X / 16, (int)drawPlayer.position.Y / 16) * 4f);
        }
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Rectangle dims = this.GetDims();
        Vector2 vector = dims.Size() / 2f;
        Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - dims.Height);
        Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
        float num = Item.velocity.X * 0.2f;
        spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>(Texture + "_Glow"), vector2, dims, new Color(255, 255, 255, 0), num, vector, scale, SpriteEffects.None, 0f);
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.08f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>(), 24)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
