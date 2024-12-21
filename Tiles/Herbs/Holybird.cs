using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Avalon.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Avalon.Common.Templates;

namespace Avalon.Tiles.Herbs;

public class Holybird : ModHerb
{
    public override int HerbDrop => ModContent.ItemType<Items.Material.Herbs.Holybird>();
    public override int SeedDrop => ModContent.ItemType<HolybirdSeeds>();
    public override int[] ValidAnchorTiles => new int[]
    {
        TileID.Pearlstone,
        TileID.HallowedGrass
    };
    public override LocalizedText MapName => this.GetLocalization("MapEntry");
    public override Color MapColor => new Color(98, 52, 228);
    public override int Dust => DustID.EnchantedNightcrawler;
	public override FlipSprite FlipSpriteStyle => FlipSprite.Custom;
	public override void SetStaticDefaults()
    {
        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;
        base.SetStaticDefaults();
    }
    public override void RandomUpdate(int i, int j)
    {
        Tile tile = Framing.GetTileSafely(i, j); //Safe way of getting a tile instance
        PlantStage stage = GetStage(i, j); //The current stage of the herb

        if (stage == PlantStage.Planted && Main.rand.NextBool(8))
        {
            tile.TileFrameX += 18;
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
        if (stage == PlantStage.Mature)
        {
            if (Main.rand.NextBool(12))
            {
                tile.TileFrameX = 36;
            }
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
    }
}
