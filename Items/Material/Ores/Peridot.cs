using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

class Peridot : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 15;
        ItemID.Sets.GeodeDrops = new Dictionary<int, (int, int)>
        {
            {
                177,
                (3, 7)
            },
            {
                178,
                (3, 7)
            },
            {
                179,
                (3, 7)
            },
            {
                180,
                (3, 7)
            },
            {
                181,
                (3, 7)
            },
            {
                182,
                (3, 7)
            },
            {
                999,
                (3, 7)
            },
            {
                ModContent.ItemType<Tourmaline>(),
                (3, 7)
            },
            {
                Type,
                (3, 7)
            },
            {
                ModContent.ItemType<Zircon>(),
                (3, 7)
            }
        };
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Gems;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.createTile = ModContent.TileType<Tiles.PlacedGems>();
        Item.placeStyle = 4 + 6;
        Item.consumable = true;
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.value = 4000;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override bool? UseItem(Player player)
    {
        int i = Player.tileTargetX;
        int j = Player.tileTargetY;
        if ((WorldGen.SolidTile(i - 1, j, noDoors: true) || WorldGen.SolidTile(i + 1, j, noDoors: true) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1)))
        {
            Item.createTile = ModContent.TileType<Tiles.PlacedGems>();
            Item.consumable = true;
        }
        else
        {
            Item.createTile = -1;
            Item.consumable = false;
        }
        return null;
    }
}
