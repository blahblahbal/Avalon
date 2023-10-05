using Avalon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Avalon.Hooks
{
    internal class WindHook : ModHook
    {
        protected override void Apply()
        {
            On_TileDrawing.DrawMultiTileVinesInWind += On_TileDrawing_DrawMultiTileVinesInWind;
        }
        private void On_TileDrawing_DrawMultiTileVinesInWind(On_TileDrawing.orig_DrawMultiTileVinesInWind orig, TileDrawing self, Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY)
        {
            if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.MonsterBanner>())
            {
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
                Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomChandelier>())
            {
                sizeX = 3;
                sizeY = 3;
            }
            orig.Invoke(self, screenPosition, offSet, topLeftX, topLeftY, sizeX, sizeY);
        }
    }
}
