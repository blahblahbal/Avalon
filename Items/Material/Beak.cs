using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Material;

class Beak : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.autoReuse = true;
        Item.consumable = true;
        //Item.createTile = ModContent.TileType<Tiles.Beak>();
        Item.useTime = 10;
        Item.useTurn = true;
        Item.maxStack = 999;
        Item.value = 50;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.Size = new Vector2(16);
    }
}
