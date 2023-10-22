using Avalon.Items.Material.Herbs;
using Avalon.Items.Tools.PreHardmode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Herbs;

//A plant with 3 stages, planted, growing and grown.
//Sadly, modded plants are unable to be grown by the flower boots
public class Bloodberry : ModTile
{
    private const int FrameWidth = 18; //a field for readibilty and to kick out those magic numbers

    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileCut[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileSpelunker[Type] = true;
        AddMapEntry(Color.IndianRed, LanguageManager.Instance.GetText("Bloodberry"));
        HitSound = SoundID.Grass;
        TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
        TileObjectData.newTile.AnchorValidTiles = new int[]
        {
            TileID.CrimsonGrass,
            TileID.Crimstone
        };
        TileObjectData.newTile.AnchorAlternateTiles = new int[]
        {
            TileID.ClayPot,
            TileID.PlanterBox,
            ModContent.TileType<PlanterBoxes>()
        };

        TileObjectData.addTile(Type);
    }
    public override bool CanPlace(int i, int j)
    {
        return Data.Sets.Tile.SuitableForPlantingHerbs[Main.tile[i, j + 1].TileType] &&
               (!Main.tile[i, j].HasTile || Main.tile[i, j].TileType == TileID.Plants || Main.tile[i, j].TileType == Type);
    }
    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
        if (i % 2 == 1)
            spriteEffects = SpriteEffects.FlipHorizontally;
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        PlantStage stage = GetStage(i, j);

        if (stage == PlantStage.Blooming)
        {
            Player p = ClassExtensions.GetPlayerForTile(i, j);
            int dropItemStack = 1;
            if (p.HeldItem.type == ItemID.StaffofRegrowth || p.HeldItem.type == ItemID.AcornAxe)
            {
                dropItemStack = Main.rand.Next(2) + 1;
            }
            yield return new Item(ModContent.ItemType<Items.Material.Herbs.Bloodberry>(), dropItemStack);
        }
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        Player p = ClassExtensions.GetPlayerForTile(i, j);
        int secondaryItemStack = Main.rand.Next(3) + 1;
        PlantStage stage = GetStage(i, j);
        bool flag = p.HeldItem.type == ItemID.StaffofRegrowth || p.HeldItem.type == ItemID.AcornAxe;
        if (flag && stage == PlantStage.Mature)
        {
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i, j).ToWorldCoordinates(), ModContent.ItemType<BloodberrySeeds>(), secondaryItemStack);
        }
        if (stage == PlantStage.Blooming)
        {
            if (flag)
            {
                secondaryItemStack = Main.rand.Next(5) + 1;
            }
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i, j).ToWorldCoordinates(), ModContent.ItemType<BloodberrySeeds>(), secondaryItemStack);
        }
    }

    public override void RandomUpdate(int i, int j)
    {
        Tile tile = Framing.GetTileSafely(i, j); //Safe way of getting a tile instance
        PlantStage stage = GetStage(i, j); //The current stage of the herb

        //Only grow to the next stage if there is a next stage. We dont want our tile turning pink!
        if (stage == PlantStage.Planted && Main.rand.Next(8) == 0)
        {
            tile.TileFrameX += FrameWidth;
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
        if (stage == PlantStage.Mature)
        {
            if (Main.bloodMoon)
            {
                tile.TileFrameX = 36;
            }
            else tile.TileFrameX = 18;
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
        if (stage == PlantStage.Blooming && !Main.bloodMoon)
        {
            tile.TileFrameX = 18;
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
        //if (stage != PlantStage.Grown)
        //{
        //    //Increase the x frame to change the stage
        //    tile.frameX += FrameWidth;

        //    //If in multiplayer, sync the frame change
        //    if (Main.netMode != NetmodeID.SinglePlayer)
        //        NetMessage.SendTileSquare(-1, i, j, 1);
        //}
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        bool intoRenderTargets = true;
        bool flag = intoRenderTargets || Main.LightingEveryFrame;

        if (Main.tile[i, j].TileFrameX > 18 && flag)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, 3);
            return false;
        }

        return true;
    }

    //A method to quickly get the current stage of the herb
    private PlantStage GetStage(int i, int j)
    {
        Tile tile = Framing.GetTileSafely(i, j); //Always use Framing.GetTileSafely instead of Main.tile as it prevents any errors caused from other mods
        return (PlantStage)(tile.TileFrameX / FrameWidth);
    }
}
