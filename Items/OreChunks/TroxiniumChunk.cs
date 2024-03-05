using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Avalon.PlayerDrawLayers;

namespace Avalon.Items.OreChunks;

class TroxiniumChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 100;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.LightRed;
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        }
        Item.GetGlobalItem<ItemGlowmask>().glowAlpha = 0;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor * 4f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ModContent.ItemType<Material.Bars.TroxiniumBar>())
            .AddIngredient(Type, 5)
            .AddTile(TileID.AdamantiteForge)
            .Register();
    }
}
