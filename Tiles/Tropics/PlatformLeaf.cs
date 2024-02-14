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

public class PlatformLeaf : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolidTop[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;
        AnimationFrameHeight = 72;
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
    //public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    //{
    //    offsetY = 2;
    //}
    public override void FloorVisuals(Player player)
    {
    }
    //public override IEnumerable<Item> GetItemDrops(int i, int j)
    //{
    //    yield return new Item(ModContent.ItemType<Items.Placeable.Tile.PlatformLeaf>());
    //}
    public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
    {
        if (Main.tile[i, j].TileFrameY >= 74)
        {
            Main.tileTable[type] = false;
            Main.tileSolidTop[type] = false;
            //if (Main.tileFrameCounter[Type] > 50)
            //{
            //    Main.tileFrame[Type] = 0;
            //    Main.tileFrameCounter[Type] = 0;
            //}
        }
        else
        {
            Main.tileSolidTop[type] = true;
            Main.tileTable[type] = true;
        }
    }
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


        if (Main.tile[i, j].TileFrameY > 74)
        {
            for (int x = xpos; x < xpos + 3; x++)
            {
                for (int y = ypos; y < ypos + 4; y++)
                {
                    Main.tile[x, y].TileFrameY -= 74;
                }
            }
            SoundStyle s = new SoundStyle("Terraria/Sounds/Grass") { Pitch = 0.2f };
            SoundEngine.PlaySound(s, new Vector2((i + 1) * 16, j * 16));
            WorldGen.TreeGrowFX(xpos + 1, ypos, 4, ModContent.GoreType<TropicsTreeLeaf>(), true);
        }
    }
}
