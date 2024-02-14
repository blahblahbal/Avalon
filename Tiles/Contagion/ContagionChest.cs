using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Contagion
{
    public class ContagionChest : ChestTemplate
    {
        public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.ContagionChest>();
        protected override bool CanBeLocked => true;
        protected override int ChestKeyItemId => ModContent.ItemType<Items.Other.ContagionKey>();
        public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
        {
            return NPC.downedPlantBoss;
        }
        public override ushort GetMapOption(int i, int j)
        {
            return (ushort)(Main.tile[i, j].TileFrameX / 36);
        }
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

        //public override IEnumerable<Item> GetItemDrops(int i, int j)
        //{
        //    Tile tile = Main.tile[i, j];
        //    int style = TileObjectData.GetTileStyle(tile);
        //    if (style == 0)
        //    {
        //        yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.ContagionChest>());
        //    }
        //    if (style == 1)
        //    {
        //        // Style 1 is ExampleChest when locked. We want that tile style to drop the ExampleChest item as well. Use the Chest Lock item to lock this chest.
        //        // No item places ExampleChest in the locked style, so the automatic item drop is unknown, this is why GetItemDrops is necessary in this situation.
        //        yield return new Item(ModContent.ItemType<Items.Placeable.Furniture.ContagionChest>());
        //    }
        //}
    }
}
