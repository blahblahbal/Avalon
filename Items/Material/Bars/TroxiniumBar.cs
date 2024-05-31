using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

class TroxiniumBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.PlacedBars>();
        Item.placeStyle = 11;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.value = Item.sellPrice(0, 0, 75);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
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
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Ores.TroxiniumOre>(), 4)
            .AddTile(TileID.AdamantiteForge)
            .Register();
    }
}
