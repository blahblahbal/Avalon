using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.WorldBuilding;
using System;
using System.Reflection;
using System.Linq;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Dungeon;
using Avalon.Tiles.Furniture;
using Avalon.Tiles.Hellcastle;
using Avalon.Tiles.Blastplains;
using Avalon.Tiles.Furniture.Functional;

namespace Avalon.Hooks
{
    internal class MinecartTrackDetour : ModHook
    {
        private static readonly int[] InvalidWalls = new int[14]
        {
            ModContent.WallType<Walls.OrangeBrickUnsafe>(), ModContent.WallType<Walls.OrangeSlabUnsafe>(), ModContent.WallType<Walls.OrangeTiledUnsafe>(),
            ModContent.WallType<Walls.PurpleBrickUnsafe>(), ModContent.WallType<Walls.PurpleSlabWallUnsafe>(), ModContent.WallType<Walls.PurpleTiledWallUnsafe>(),
            ModContent.WallType<Walls.YellowBrickUnsafe>(), ModContent.WallType<Walls.YellowSlabWallUnsafe>(), ModContent.WallType<Walls.YellowTiledWallUnsafe>(),
            WallID.IceBrick, WallID.ObsidianBackEcho, ModContent.WallType<Walls.ImperviousBrickWallUnsafe>(), ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>(),
            ModContent.WallType<Walls.NestWall>()
        };
        private static readonly int[] InvalidTiles = new int[30]
        {
            ModContent.TileType<OrangeBrick>(), ModContent.TileType<PurpleBrick>(), ModContent.TileType<YellowBrick>(),
            ModContent.TileType<CrackedOrangeBrick>(), ModContent.TileType<CrackedPurpleBrick>(), ModContent.TileType<CrackedYellowBrick>(),
            ModContent.TileType<BlastedStone>(), TileID.IceBrick, ModContent.TileType<ImperviousBrick>(), TileID.LavaMossBlock, TileID.Containers, TileID.Containers2,
            ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodChest>(), ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyChest>(),
            ModContent.TileType<HellfireChest>(), ModContent.TileType<IckyAltar>(), ModContent.TileType<Tiles.GemTrees.TourmalineTree>(),
            ModContent.TileType<Tiles.GemTrees.PeridotTree>(), ModContent.TileType<Tiles.GemTrees.ZirconTree>(), ModContent.TileType<Statues>(),
            ModContent.TileType<Tiles.Savanna.PlatformLeaf>(), ModContent.TileType<Tiles.GemStashes>(), ModContent.TileType<Tiles.ChunkstoneColumn>(),
            ModContent.TileType<Tiles.CrimstoneColumn>(), ModContent.TileType<Tiles.EbonstoneColumn>(), ModContent.TileType<Tiles.Savanna.TuhrtlBrick>(),
            ModContent.TileType<Tiles.Paintings>(), ModContent.TileType<PlatinumChest>(), ModContent.TileType<Tiles.Savanna.Loamstone>(), TileID.PlatinumBrick
        };

        protected override void Apply()
        {
            On_TrackGenerator.IsLocationInvalid += On_TrackGenerator_IsLocationInvalid;
        }
        private bool On_TrackGenerator_IsLocationInvalid(On_TrackGenerator.orig_IsLocationInvalid orig, int x, int y)
        {
            ushort wall = Main.tile[x, y].WallType;
            for (int i = 0; i < InvalidWalls.Length; i++)
            {
                if (wall == InvalidWalls[i] && (!WorldGen.notTheBees || wall != 108))
                {
                    return true;
                }
            }
            ushort type = Main.tile[x, y].TileType;
            for (int j = 0; j < InvalidTiles.Length; j++)
            {
                if (type == InvalidTiles[j])
                {
                    return true;
                }
            }

            return orig.Invoke(x, y);
        }
    }
}
