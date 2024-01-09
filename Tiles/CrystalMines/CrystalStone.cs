using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System;
using System.Runtime.CompilerServices;
using Terraria.WorldBuilding;

namespace Avalon.Tiles.CrystalMines;

public class CrystalStone : ModTile
{
    Color c1 = new Color(123, 186, 228);
    Color c2 = new Color(144, 171, 221);
    Color c3 = new Color(163, 160, 216);
    Color c4 = new Color(176, 153, 214);
    Color c5 = new Color(186, 146, 212);
    Color c6 = new Color(200, 138, 209);
    Color c7 = new Color(216, 129, 205);
    Color c8 = new Color(227, 123, 203);
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(154, 149, 247));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[Type][TileID.SnowBlock] = true;
        Main.tileMerge[TileID.SnowBlock][Type] = true;
        Main.tileMerge[Type][TileID.Ebonstone] = true;
        Main.tileMerge[TileID.Ebonstone][Type] = true;
        Main.tileMerge[Type][TileID.Crimstone] = true;
        Main.tileMerge[TileID.Crimstone][Type] = true;
        Main.tileMerge[Type][TileID.Stone] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[Type][TileID.Mud] = true;
        Main.tileMerge[TileID.Mud][Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.CrystalDust>();
        MinPick = 400;
    }

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (Main.rand.NextBool(7000))
        {
            int num162 = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.CrystalDust>(), 0f, 0f, 128, default,
                0.75f);
            Main.dust[num162].noGravity = true;
            Main.dust[num162].velocity *= 0.1f;
            Main.dust[num162].fadeIn = 1f;
            //Main.dust[num162].scale;
        }
    }
    public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawInfo)
    {
        if (i % 14 == 0 || i % 14 == 13)
        {
            drawInfo.finalColor = c1;
        }
        else if (i % 14 == 1 || i % 14 == 12)
        {
            drawInfo.finalColor = c2;
        }
        else if (i % 14 == 2 || i % 14 == 11)
        {
            drawInfo.finalColor = c3;
        }
        else if (i % 14 == 3 || i % 14 == 10)
        {
            drawInfo.finalColor = c4;
        }
        else if (i % 14 == 4 || i % 14 == 9)
        {
            drawInfo.finalColor = c5;
        }
        else if (i % 14 == 5 || i % 14 == 8)
        {
            drawInfo.finalColor = c6;
        }
        else if (i % 14 == 6 || i % 14 == 7)
        {
            drawInfo.finalColor = c7;
        }
        else if (i % 14 == 7)
        {
            drawInfo.finalColor = c8;
        }
    }
    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        if ((int)(Math.Sin(Math.Tan(i + j) + Math.Tan(j * 0.3f)) * 200) % 10 == 0)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            Vector2 pos = new Vector2(i * 16, j * 16) + zero - Main.screenPosition;
            Rectangle frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
            //Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/CrystalMines/CrystalStone_Glow").Value, pos, frame, Color.White);

            float lerp = (float)Math.Sin((Main.timeForVisualEffects + (i * 8)) * 0.008f);
            float colorMultiply = Math.Abs(Math.Clamp(Main.player[Main.myPlayer].Center.Distance(new Vector2(i * 16, j * 16)) * 0.003f, 0.2f, 0.8f) - 1);

            Color color = Color.Lerp(Color.White * colorMultiply, Lighting.GetColor(i, j), lerp);
            color.A = 0;
           
            for(int r = 0; r < 8; r++)
            {
                Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/CrystalMines/CrystalStone_Glow").Value, pos + new Vector2(0,Math.Clamp(-lerp,0,1) * 2).RotatedBy((MathHelper.PiOver4 * r) + (0.01f * Main.timeForVisualEffects)), frame, color * 0.2f);
            
            }
            color.A = 0;
            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/CrystalMines/CrystalStone_Glow").Value, pos, frame, color);
        }
    }
}
