using Avalon.Items.Material.Herbs;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Tiles.Contagion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Avalon.Reflection;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Herbs;

//An enum on the 3 stages of herb growth.
public enum PlantStage : byte
{
    Planted,
    Mature,
    Blooming
}

//A plant with 3 stages, planted, growing and grown.
//Sadly, modded plants are unable to be grown by the flower boots
public class Barfbush : ModTile
{
    private const int FrameWidth = 18; //a field for readibilty and to kick out those magic numbers

    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        Main.tileCut[Type] = true;
        Main.tileNoFail[Type] = true;
        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.IgnoredInHouseScore[Type] = true;
        TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
        Main.tileAlch[Type] = true;
        TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);
        //Main.tileSpelunker[Type] = true;
        AddMapEntry(new Color(0, 200, 50), LanguageManager.Instance.GetText("Barfbush"));
        HitSound = SoundID.Grass;
        TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);

        TileObjectData.newTile.AnchorValidTiles = new int[]
        {
            ModContent.TileType<Ickgrass>(),
            ModContent.TileType<Chunkstone>()
        };

        TileObjectData.newTile.AnchorAlternateTiles = new int[]
        {
            TileID.ClayPot,
            TileID.PlanterBox,
            ModContent.TileType<PlanterBoxes>()
        };

        TileObjectData.addTile(Type);
        DustType = 2;
    }
    public override bool IsTileSpelunkable(int i, int j)
    {
        PlantStage stage = GetStage(i, j);

        // Only glow if the herb is grown
        return stage == PlantStage.Blooming;
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
    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        Player p = ClassExtensions.GetPlayerForTile(i, j);
        if (p.HeldItem.type == ItemID.StaffofRegrowth || p.HeldItem.type == ItemID.AcornAxe)
            return true;
        return base.CanKillTile(i, j, ref blockDamaged);
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
            yield return new Item(ModContent.ItemType<Items.Material.Herbs.Barfbush>(), dropItemStack);
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
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i, j).ToWorldCoordinates(), ModContent.ItemType<BarfbushSeeds>(), secondaryItemStack);
        }
        if (stage == PlantStage.Blooming)
        {
            if (flag)
            {
                secondaryItemStack = Main.rand.Next(5) + 1;
            }
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i, j).ToWorldCoordinates(), ModContent.ItemType<BarfbushSeeds>(), secondaryItemStack);
        }
    }
    public override void RandomUpdate(int i, int j)
    {
        Tile tile = Framing.GetTileSafely(i, j); //Safe way of getting a tile instance
        PlantStage stage = GetStage(i, j); //The current stage of the herb

        if (stage == PlantStage.Planted && Main.rand.NextBool(12))
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

        ////Only grow to the next stage if there is a next stage. We dont want our tile turning pink!
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
