using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

class CurseofOblivion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<DarkRedRarity>();
        Item.createTile = ModContent.TileType<Tiles.Paintings>();
        Item.placeStyle = 10;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.value = Item.sellPrice(0, 0, 10, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
