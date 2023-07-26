using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Contagion;

public class ContagionShortGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;
        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.SwaysInWindBasic[Type] = true;
        TileID.Sets.DrawFlipMode[Type] = 1;
        TileID.Sets.SlowlyDiesInWater[Type] = true;
        Main.tileCut[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.AnchorValidTiles = new int[]
        {
            ModContent.TileType<Ickgrass>(),
            ModContent.TileType<ContagionJungleGrass>()
        };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<Dusts.ContagionDust>();
        HitSound = SoundID.Grass;
        AddMapEntry(new Color(133, 150, 39));
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (Main.tile[i, j].TileFrameX / 18 == 8)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<VirulentMushroom>());
        }
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        offsetY = 2;
    }
    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
        //Flips the sprite if x coord is odd. Makes the tile more interesting
        if (i % 2 == 1)
            spriteEffects = SpriteEffects.FlipHorizontally;
    }
    //public override void PlaceInWorld(int i, int j, Item item)
    //{
    //    Main.tile[i, j].frameX = (short)(Main.rand.Next(0, 8) * 18);
    //}
}
