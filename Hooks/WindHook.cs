using Avalon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalon.Tiles.Furniture;
using Avalon.Tiles.Furniture.BleachedEbony;
using Avalon.Tiles.Furniture.Coughwood;
using Avalon.Tiles.Furniture.Heartstone;
using Avalon.Tiles.Furniture.OrangeDungeon;
using Avalon.Tiles.Furniture.PurpleDungeon;
using Avalon.Tiles.Furniture.ResistantWood;
using Avalon.Tiles.Furniture.WildMushroom;
using Avalon.Tiles.Furniture.YellowDungeon;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Avalon.Compatability.Thorium.Tiles;
using System.Text.Encodings.Web;

namespace Avalon.Hooks
{
    internal class WindHook : ModHook //Should rename since its nolonger just hooks
    {
        protected override void Apply()
        {
            On_TileDrawing.DrawMultiTileVinesInWind += On_TileDrawing_DrawMultiTileVinesInWind;
            On_TileDrawing.PostDrawTiles += On_TileDrawing_PostDrawTiles;
            IL_TileDrawing.DrawMultiTileGrassInWind += IL_TileDrawing_DrawMultiTileGrassInWind;
        }

        private void IL_TileDrawing_DrawMultiTileGrassInWind(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new(il);
            c.EmitLdarg0(); //self
            c.EmitLdarg3(); //topLeftX
            c.EmitLdarg(4); //topLeftY
            c.EmitLdarga(1); //screenPosition
            c.EmitLdarga(2); //offset
            c.EmitLdarga(5); //sizeX
            c.EmitLdarga(6); //size Y
            c.EmitDelegate((TileDrawing self, int topLeftX, int topLeftY, ref Vector2 screenPosition, ref Vector2 offSet, ref int sizeX, ref int sizeY) =>
            {
                Texture2D dud = null;
                Color dud2 = Color.White;
                DrawMultiTileGrassVariableEditor(self, ref screenPosition, ref offSet, topLeftX, topLeftY, ref sizeX, ref sizeY, ref dud, ref dud2); //Adds the sizing of the tile
            });

            c.GotoNext(
                MoveType.After,
                i => i.MatchLdarg0(),
                i => i.MatchLdloc(10),
                i => i.MatchLdloc(8),
                i => i.MatchLdloc(9),
                i => i.MatchCall<TileDrawing>("GetTileDrawTexture"),
                i => i.MatchStloc(27)
                ); //Goes to before the Main.Spritebatch Drawing and injects, when I mean right before, I mean the next instruction is calling Main.spritebatch, ya know
            c.EmitLdarg0(); //self
            c.EmitLdarg3(); //topLeftX
            c.EmitLdarg(4); //topLeftY
            c.EmitLdloca(4); //val2
            c.EmitLdloca(5); //color
            c.EmitDelegate((TileDrawing self, int topLeftX, int topLeftY, ref Texture2D val2, ref Color color) => //Should note that the val2 is the extra texture thats only used by sunflower's """"flames""""
            {
                int dud = 0;
                Vector2 dud2 = Vector2.Zero;
                DrawMultiTileGrassVariableEditor(self, ref dud2, ref dud2, topLeftX, topLeftY, ref dud, ref dud, ref val2, ref color); //Adds extra content (glowmasks, color, ect)
            });
        }

        public static void DrawMultiTileGrassVariableEditor(TileDrawing self, ref Vector2 screenPosition, ref Vector2 offSet, int topLeftX, int topLeftY, ref int sizeX, ref int sizeY, ref Texture2D overlayTexture, ref Color overlayColor)
        {
            if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Herbs.LargeHerbsStage1>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Herbs.LargeHerbsStage2>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Herbs.LargeHerbsStage3>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Herbs.LargeHerbsStage4>())
            {
                sizeY = 3;
            }
            if (ExxoAvalonOrigins.Thorium != null)
            {
                if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Compatability.Thorium.Tiles.LargeMarineKelpStage1>() ||
                    Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Compatability.Thorium.Tiles.LargeMarineKelpStage2>() ||
                    Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Compatability.Thorium.Tiles.LargeMarineKelpStage3>() ||
                    Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Compatability.Thorium.Tiles.LargeMarineKelpStage4>())
                {
                    sizeY = 3;
                    overlayColor = Color.White;
                    overlayTexture = ModContent.Request<Texture2D>(TileLoader.GetTile(ModContent.TileType<LargeMarineKelpStage4>()).Texture + "_Glow").Value;
                }
            }
        }

        private void On_TileDrawing_DrawMultiTileVinesInWind(On_TileDrawing.orig_DrawMultiTileVinesInWind orig, TileDrawing self, Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY)
        {
            if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.MonsterBanner>())
            {
                sizeY = 3;
            }
            else if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodLantern>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodLantern>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyLantern>())
            {
                sizeY = 2;
            }
            else if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.HangingPots>())
            {
                sizeX = 2;
                sizeY = 3;
            }
            else if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.BismuthChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.BronzeChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.ZincChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.OrangeDungeon.OrangeDungeonChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.PurpleDungeon.PurpleDungeonChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.YellowDungeon.YellowDungeonChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomChandelier>() ||
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.Dirtalier>())
            {
                sizeX = 3;
                sizeY = 3;
            }
            orig.Invoke(self, screenPosition, offSet, topLeftX, topLeftY, sizeX, sizeY);
        }
        private void On_TileDrawing_PostDrawTiles(On_TileDrawing.orig_PostDrawTiles orig, TileDrawing self, bool solidLayer, bool forRenderTargets, bool intoRenderTargets)
        {
            orig.Invoke(self, solidLayer, forRenderTargets, intoRenderTargets);
            if (!solidLayer && !intoRenderTargets)
            {
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
                DrawChandeliers();
                DrawLanterns();
                Main.spriteBatch.End();
            }
        }
        private void DrawLanterns()
        {
            for (int i = 0; i < ModContent.GetInstance<BleachedEbonyLantern>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<BleachedEbonyLantern>().DrawMultiTileVines(ModContent.GetInstance<BleachedEbonyLantern>().Coordinates[i].X, ModContent.GetInstance<BleachedEbonyLantern>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<CoughwoodLantern>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<CoughwoodLantern>().DrawMultiTileVines(ModContent.GetInstance<CoughwoodLantern>().Coordinates[i].X, ModContent.GetInstance<CoughwoodLantern>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<HeartstoneLantern>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<HeartstoneLantern>().DrawMultiTileVines(ModContent.GetInstance<HeartstoneLantern>().Coordinates[i].X, ModContent.GetInstance<HeartstoneLantern>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<ResistantWoodLantern>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<ResistantWoodLantern>().DrawMultiTileVines(ModContent.GetInstance<ResistantWoodLantern>().Coordinates[i].X, ModContent.GetInstance<ResistantWoodLantern>().Coordinates[i].Y, Main.spriteBatch);
            }
        }
        private void DrawChandeliers()
        {
            for (int i = 0; i < ModContent.GetInstance<BismuthChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<BismuthChandelier>().DrawMultiTileVines(ModContent.GetInstance<BismuthChandelier>().Coordinates[i].X, ModContent.GetInstance<BismuthChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<BronzeChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<BronzeChandelier>().DrawMultiTileVines(ModContent.GetInstance<BronzeChandelier>().Coordinates[i].X, ModContent.GetInstance<BronzeChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<ZincChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<ZincChandelier>().DrawMultiTileVines(ModContent.GetInstance<ZincChandelier>().Coordinates[i].X, ModContent.GetInstance<ZincChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<BleachedEbonyChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<BleachedEbonyChandelier>().DrawMultiTileVines(ModContent.GetInstance<BleachedEbonyChandelier>().Coordinates[i].X, ModContent.GetInstance<BleachedEbonyChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<CoughwoodChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<CoughwoodChandelier>().DrawMultiTileVines(ModContent.GetInstance<CoughwoodChandelier>().Coordinates[i].X, ModContent.GetInstance<CoughwoodChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<Dirtalier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<Dirtalier>().DrawMultiTileVines(ModContent.GetInstance<Dirtalier>().Coordinates[i].X, ModContent.GetInstance<Dirtalier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<HeartstoneChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<HeartstoneChandelier>().DrawMultiTileVines(ModContent.GetInstance<HeartstoneChandelier>().Coordinates[i].X, ModContent.GetInstance<HeartstoneChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<OrangeDungeonChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<OrangeDungeonChandelier>().DrawMultiTileVines(ModContent.GetInstance<OrangeDungeonChandelier>().Coordinates[i].X, ModContent.GetInstance<OrangeDungeonChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<PurpleDungeonChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<PurpleDungeonChandelier>().DrawMultiTileVines(ModContent.GetInstance<PurpleDungeonChandelier>().Coordinates[i].X, ModContent.GetInstance<PurpleDungeonChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<ResistantWoodChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<ResistantWoodChandelier>().DrawMultiTileVines(ModContent.GetInstance<ResistantWoodChandelier>().Coordinates[i].X, ModContent.GetInstance<ResistantWoodChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<WildMushroomChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<WildMushroomChandelier>().DrawMultiTileVines(ModContent.GetInstance<WildMushroomChandelier>().Coordinates[i].X, ModContent.GetInstance<WildMushroomChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
            for (int i = 0; i < ModContent.GetInstance<YellowDungeonChandelier>().Coordinates.Count; i++)
            {
                ModContent.GetInstance<YellowDungeonChandelier>().DrawMultiTileVines(ModContent.GetInstance<YellowDungeonChandelier>().Coordinates[i].X, ModContent.GetInstance<YellowDungeonChandelier>().Coordinates[i].Y, Main.spriteBatch);
            }
        }
    }
}
