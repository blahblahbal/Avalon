using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture
{
    public class LockedChests : ChestTemplate
    {

        protected override bool CanBeLocked => base.CanBeLocked;
        public override int Dust => DustID.Stone;
        protected override int ChestKeyItemId => ModContent.ItemType<Items.Tools.SonicScrewdriverMkIII>();
        public override bool CanBeUnlockedNormally => false;
        public override void SetStaticDefaults()
        {
            Main.tileSpelunker[Type] = true;
            Main.tileContainer[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileOreFinderPriority[Type] = 500;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.BasicChest[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.AvoidedByNPCs[Type] = true;
            TileID.Sets.InteractibleByNPCs[Type] = true;
            TileID.Sets.IsAContainer[Type] = true;
            TileID.Sets.FriendlyFairyCanLureTo[Type] = true;

            DustType = Dust;
            AdjTiles = new int[] { TileID.Containers };

            // Other tiles with just one map entry use CreateMapEntryName() to use the default translationkey, "MapEntry"
            // Since ExampleChest needs multiple, we register our own MapEntry keys
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
            TileObjectData.newTile.AnchorInvalidTiles = new int[] {
                TileID.MagicalIceBlock,
                TileID.Boulder,
                TileID.BouncyBoulder,
                TileID.LifeCrystalBoulder,
                TileID.RollingCactus
            };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int style = TileObjectData.GetTileStyle(tile);
            int type = 0;
            switch (style)
            {
                case 0:
                    type = ItemID.IceChest;
                    break;
                case 1:
                    type = ItemID.Chest;
                    break;
                case 2:
                    type = ItemID.LivingWoodChest;
                    break;
                case 3:
                    type = ItemID.EbonwoodChest;
                    break;
                case 4:
                    type = ItemID.RichMahoganyChest;
                    break;
                case 5:
                    type = ItemID.PearlwoodChest;
                    break;
                case 6:
                    type = ItemID.IvyChest;
                    break;
                case 7:
                    type = ItemID.SkywareChest;
                    break;
                case 8:
                    type = ItemID.ShadewoodChest;
                    break;
                case 9:
                    type = ItemID.WebCoveredChest;
                    break;
            }
            if (type > 0)
            {
                yield return new Item(type);
            }
        }

        //public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
        //{
        //    if (Main.tile[i, j].TileFrameX == 0)
        //    {
        //        frameXAdjustment = -(11 * 36);
        //        Main.tile[i, j].TileType = TileID.Containers;
        //        return true;
        //    }
        //    else if (Main.tile[i, j].TileFrameX == 36)
        //    {
        //        frameXAdjustment = -36;
        //        Main.tile[i, j].TileType = TileID.Containers;
        //        return true;
        //    }
        //    else if (Main.tile[i, j].TileFrameX == 72)
        //    {
        //        frameXAdjustment = 10 * 36;
        //        Main.tile[i, j].TileType = TileID.Containers;
        //        return true;
        //    }
        //    return false;
        //}

        public static bool Unlock(int X, int Y)
        {
            if (Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null)
            {
                return false;
            }
            short num = 0;
            int type = 0;
            Tile tileSafely = Framing.GetTileSafely(X, Y);
            if (tileSafely.TileType != ModContent.TileType<LockedChests>()) return false;
            int type2 = tileSafely.TileType;
            int num2 = tileSafely.TileFrameX / 36;

            SoundEngine.PlaySound(SoundID.Unlock, new(X * 16, Y * 16));

            if (tileSafely.TileFrameX == 0)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        Main.tile[i, j].TileType = TileID.Containers;
                        tileSafely2.TileFrameX += 11 * 36;
                        for (int k = 0; k < 4; k++)
                        {
                            Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, type);
                        }
                    }
                }
            }
            else if (tileSafely.TileFrameX == 36)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        Main.tile[i, j].TileType = TileID.Containers;
                        Main.tile[i, j].TileFrameX -= 36;
                        for (int k = 0; k < 4; k++)
                        {
                            Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, type);
                        }
                    }
                }
            }
            else if (tileSafely.TileFrameX == 72)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        tileSafely2.TileType = TileID.Containers;
                        tileSafely2.TileFrameX += 10 * 36;
                        for (int k = 0; k < 4; k++)
                        {
                            Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, type);
                        }
                    }
                }
            }
            else if (tileSafely.TileFrameX >= 108 && tileSafely.TileFrameX <= 216)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        tileSafely2.TileType = TileID.Containers;
                        tileSafely2.TileFrameX += 4 * 36;
                        for (int k = 0; k < 4; k++)
                        {
                            Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, type);
                        }
                    }
                }
            }
            else if (tileSafely.TileFrameX >= 252 && tileSafely.TileFrameX <= 324)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        tileSafely2.TileType = TileID.Containers;
                        tileSafely2.TileFrameX += 6 * 36;
                        for (int k = 0; k < 4; k++)
                        {
                            Terraria.Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, type);
                        }
                    }
                }
            }
            return true;
        }

        public static bool Lock(int X, int Y)
        {
            if (Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null)
            {
                return false;
            }
            short num = 0;
            Tile tileSafely = Framing.GetTileSafely(X, Y);
            if (tileSafely.TileType != TileID.Containers) return false;
            int type = tileSafely.TileType;
            int num2 = tileSafely.TileFrameX / 36;


            SoundEngine.PlaySound(SoundID.Unlock, new(X * 16, Y * 16));
            if (tileSafely.TileFrameX == 0)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
                        tileSafely2.TileFrameX += 36;
                    }
                }
            }
            else if (tileSafely.TileFrameX == 396)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
                        tileSafely2.TileFrameX -= 11 * 36;
                    }
                }
            }
            else if (tileSafely.TileFrameX == 432)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
                        tileSafely2.TileFrameX -= 10 * 36;
                    }
                }
            }
            else if (tileSafely.TileFrameX >= 252 && tileSafely.TileFrameX <= 360)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
                        tileSafely2.TileFrameX -= 4 * 36;
                    }
                }
            }
            else if (tileSafely.TileFrameX >= 468 && tileSafely.TileFrameX <= 540)
            {
                for (int i = X; i <= X + 1; i++)
                {
                    for (int j = Y; j <= Y + 1; j++)
                    {
                        Tile tileSafely2 = Framing.GetTileSafely(i, j);
                        tileSafely2.TileType = (ushort)ModContent.TileType<LockedChests>();
                        tileSafely2.TileFrameX -= 6 * 36;
                    }
                }
            }
            return true;
        }
    }
}
