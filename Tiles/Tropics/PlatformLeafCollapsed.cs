using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Tropics;

public class PlatformLeafCollapsed : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolidTop[Type] = false;
        Main.tileTable[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.DoesntGetReplacedWithTileReplacement[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
        TileObjectData.newTile.CoordinateWidth = 24;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
        TileObjectData.newTile.AnchorAlternateTiles = new int[] { TileID.WoodenSpikes };
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.AnchorTop = AnchorData.Empty;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<Dusts.TropicalDust>();
        AddMapEntry(new Color(82, 123, 35));
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Tile.PlatformLeaf>());
    }
    //public override IEnumerable<Item> GetItemDrops(int i, int j)
    //{
    //    yield return new Item(ModContent.ItemType<Items.Placeable.Tile.PlatformLeaf>());
    //}
    public override bool IsTileDangerous(int i, int j, Player player)
    {
        return true;
    }
    public override void RandomUpdate(int i, int j)
    {
        int xpos;
        int ypos;
        for (xpos = Main.tile[i, j].TileFrameX / 18; xpos > 2; xpos -= 3) { }
        for (ypos = Main.tile[i, j].TileFrameY / 18; ypos > 3; ypos -= 4) { }
        xpos = i - xpos;
        ypos = j - ypos;


        for (int x = xpos; x < xpos + 3; x++)
        {
            for (int y = ypos; y < ypos + 4; y++)
            {
                Main.tile[x, y].TileType = (ushort)ModContent.TileType<PlatformLeaf>();
            }
        }
        SoundStyle s = new SoundStyle("Terraria/Sounds/Grass") { Pitch = 0.8f };
        SoundEngine.PlaySound(s, new Vector2((i + 1) * 16, j * 16));
        WorldGen.TreeGrowFX(xpos + 1, ypos + 3, 2, ModContent.GoreType<PlatformLeafLeaf>(), true);
    }
}
