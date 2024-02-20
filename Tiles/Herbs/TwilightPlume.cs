using Avalon.Items.Material.Herbs;
using Avalon.Items.Tools.PreHardmode;
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

//A plant with 3 stages, planted, growing and grown.
//Sadly, modded plants are unable to be grown by the flower boots
public class TwilightPlume : ModHerb
{
    public override int HerbDrop => ModContent.ItemType<Items.Material.Herbs.TwilightPlume>();
    public override int SeedDrop => ModContent.ItemType<TwilightPlumeSeeds>();
    public override int[] ValidAnchorTiles => new int[]
    {
        //ModContent.TileType<TropicalGrass>(),
    };
    public override LocalizedText MapName => this.GetLocalization("MapEntry");
    public override Color MapColor => new Color(191, 0, 81);
    public override int Dust => DustID.JungleGrass;
    public override void SetStaticDefaults()
    {
        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;
        base.SetStaticDefaults();
    }

    public override void RandomUpdate(int i, int j)
    {
        Tile tile = Framing.GetTileSafely(i, j); //Safe way of getting a tile instance
        PlantStage stage = GetStage(i, j); //The current stage of the herb

        //Only grow to the next stage if there is a next stage. We dont want our tile turning pink!
        if (stage != PlantStage.Blooming && Main.rand.NextBool(13))
        {
            //Increase the x frame to change the stage
            tile.TileFrameX += 18;

            //If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
    }
}
