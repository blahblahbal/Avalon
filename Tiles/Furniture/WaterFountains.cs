using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using System;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;
using Avalon.Waters;

namespace Avalon.Tiles.Furniture
{
    public class WaterFountains : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true; // Any multitile requires this
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.InteractibleByNPCs[Type] = true; // Town NPCs will palm their hand at this tile

            DustType = 1;
            AdjTiles = new int[] { TileID.WaterFountain };

            TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.WaterFountain, 0));
            TileObjectData.newTile.StyleWrapLimit = 24;
            TileObjectData.newTile.LavaDeath = false; // Does not break when lava touches it
            TileObjectData.newTile.DrawYOffset = 2; // So the tile sinks into the ground

            TileObjectData.newTile.StyleLineSkip = 7; // This needs to be added to work for modded tiles.

            // Register the tile data itself
            TileObjectData.addTile(Type);

            // Register map name and color
            AddMapEntry(new Color(144, 148, 144), Language.GetText("MapObject.WaterFountain"));
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameY > 72)
            {
                Main.SceneMetrics.ActiveFountainColor = Mod.Find<ModWaterStyle>("ContagionWaterStyle").Slot;
            }
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;

            int style = TileObjectData.GetTileStyle(Main.tile[i, j]);
            player.cursorItemIconID = TileLoader.GetItemDropFromTypeAndStyle(Type, style);
        }
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }
        public override bool RightClick(int i, int j)
        {
            SoundEngine.PlaySound(SoundID.Mech, new Vector2(i * 16, j * 16));
            ToggleTile(i, j);
            return true;
        }
        public override void HitWire(int i, int j)
        {
            ToggleTile(i, j);
        }

        // ToggleTile is a method that contains code shared by HitWire and RightClick, since they both toggle the state of the tile.
        // Note that TileFrameY doesn't necessarily match up with the image that is drawn, AnimateTile and AnimateIndividualTile contribute to the drawing decisions.
        public void ToggleTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int topX = i - tile.TileFrameX % 36 / 18;
            int topY = j - tile.TileFrameY % 72 / 18;

            short frameAdjustment = (short)(tile.TileFrameY >= 72 ? -72 : 72);

            for (int x = topX; x < topX + 2; x++)
            {
                for (int y = topY; y < topY + 4; y++)
                {
                    Main.tile[x, y].TileFrameY += frameAdjustment;

                    if (Wiring.running)
                    {
                        Wiring.SkipWire(x, y);
                    }
                }
            }

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendTileSquare(-1, topX, topY, 2, 4);
            }
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.WaterFountain];
            frameCounter = Main.tileFrameCounter[TileID.WaterFountain];
        }
        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            var tile = Main.tile[i, j];
            if (tile.TileFrameY > 72)
            {
                frameYOffset = Main.tileFrame[type] * 72;
            }
            else
            {
                frameYOffset = 0;
            }
        }
    }
}
