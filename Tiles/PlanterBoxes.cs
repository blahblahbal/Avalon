using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles
{
    public class PlanterBoxes : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(191, 142, 111));
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[] { TileID.PlanterBox };
            DustType = DustID.Dirt;
            Data.Sets.Tile.SuitableForPlantingHerbs[Type] = true;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tile = Main.tile[i, j];
            int tType = tile.TileType;
            Rectangle rectangle = new Rectangle(-1, -1, 0, 0);
            Tile tile23 = Main.tile[i - 1, j];
            if (tile23 == null)
            {
                return false;
            }
            Tile tile30 = Main.tile[i + 1, j];
            if (!(tile30 == null) && !(Main.tile[i - 1, j + 1] == null) && !(Main.tile[i + 1, j + 1] == null) && !(Main.tile[i - 1, j - 1] == null) && Main.tile[i + 1, j - 1] != null)
            {
                int num12 = -1;
                int num23 = -1;
                if (tile23 != null && tile23.HasTile)
                {
                    num23 = Main.tileStone[tile23.TileType] ? 1 : tile23.TileType;
                }
                if (tile30 != null && tile30.HasTile)
                {
                    num12 = Main.tileStone[tile30.TileType] ? 1 : tile30.TileType;
                }
                if (num12 >= 0 && !Main.tileSolid[num12])
                {
                    num12 = -1;
                }
                if (num23 >= 0 && !Main.tileSolid[num23])
                {
                    num23 = -1;
                }
                if (num23 == tType && num12 == tType)
                {
                    rectangle.X = 18;
                }
                else if (num23 == tType && num12 != tType)
                {
                    rectangle.X = 36;
                }
                else if (num23 != tType && num12 == tType)
                {
                    rectangle.X = 0;
                }
                else
                {
                    rectangle.X = 54;
                }
                tile.TileFrameX = (short)rectangle.X;
            }
            return false;
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            int toDrop = 0;
            switch (Main.tile[i, j].TileFrameY / 18)
            {
                case 0:
                    toDrop = ModContent.ItemType<Items.Placeable.Tile.BarfbushPlanterBox>();
                    break;
                case 1:
                    toDrop = ModContent.ItemType<Items.Placeable.Tile.HolybirdPlanterBox>();
                    break;
                case 2:
                    toDrop = ModContent.ItemType<Items.Placeable.Tile.TwilightPlumePlanterBox>();
                    break;
                case 3:
                    toDrop = ModContent.ItemType<Items.Placeable.Tile.SweetstemPlanterBox>();
                    break;
            }
            yield return new Item(toDrop);
        }
        public override void PostSetDefaults()
        {
            Main.tileNoSunLight[Type] = false;
        }
    }
}
