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
using ReLogic.Content;
using Terraria.GameContent;
using Avalon.Tiles.Contagion;

namespace Avalon.Tiles.CrystalMines;

public class CrystalStoneCrystals : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(184, 179, 255));
        Main.tileSolid[Type] = true;
        Main.tileMerge[ModContent.TileType<CrystalStone>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<CrystalStone>()] = true;
        Common.TileMerge.MergeWith(Type, ModContent.TileType<CrystalStone>());
        Main.tileLighted[Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.CrystalDust>();
        MinPick = 400;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Common.TileMerge.MergeWithFrame(i, j, Type, ModContent.TileType<CrystalStone>(), false, false, false, false, resetFrame);
        return false;
    }
    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (Main.rand.NextBool(4000))
        {
            int num162 = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.CrystalDust>(), 0f, 0f, 128, default,
                0.75f);
            Main.dust[num162].noGravity = true;
            Main.dust[num162].velocity *= 0.1f;
            Main.dust[num162].fadeIn = 1f;
            //Main.dust[num162].scale;
        }
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.06f;
        g = 0.04f;
        b = 0.08f;
    }
}
