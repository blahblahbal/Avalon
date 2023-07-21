using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

class TomeForge : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Tome Forge");
        //Tooltip.SetDefault("Used to craft Mystical Tomes");
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.TomeForge>();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 1);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
}
