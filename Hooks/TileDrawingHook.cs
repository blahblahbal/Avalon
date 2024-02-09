using Avalon.Common;
using Avalon.Tiles.Furniture;
using Avalon.Tiles.Furniture.BleachedEbony;
using Avalon.Tiles.Furniture.Coughwood;
using Avalon.Tiles.Furniture.Heartstone;
using Avalon.Tiles.Furniture.OrangeDungeon;
using Avalon.Tiles.Furniture.PurpleDungeon;
using Avalon.Tiles.Furniture.ResistantWood;
using Avalon.Tiles.Furniture.WildMushroom;
using Avalon.Tiles.Furniture.YellowDungeon;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class TileDrawingHook : ModHook
{
    protected override void Apply()
    {
        On_TileDrawing.PostDrawTiles += On_TileDrawing_PostDrawTiles;
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
