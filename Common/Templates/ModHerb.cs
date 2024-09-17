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
using Avalon.Tiles;

namespace Avalon.Common.Templates;

public abstract class ModHerb : ModTile
{
    public enum PlantStage : byte
    {
        Planted,
        Mature,
        Blooming
    }
    public virtual int[] ValidAnchorTiles => new int[2];
    public virtual int[] AlternateAnchorTiles => new int[3];
    public virtual int HerbDrop => 0;
    public virtual int SeedDrop => 0;
    public virtual LocalizedText MapName => LanguageManager.Instance.GetText("");
    public virtual Color MapColor => Color.White;
    public virtual int Dust => DustID.Dirt;
	public virtual bool FlipSprite => true;

	public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileCut[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileSpelunker[Type] = true;
        AddMapEntry(MapColor, MapName);
        HitSound = SoundID.Grass;
        TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
        TileObjectData.newTile.AnchorValidTiles = ValidAnchorTiles;
        TileObjectData.newTile.AnchorAlternateTiles = new int[]
        {
            TileID.ClayPot,
            TileID.PlanterBox,
            ModContent.TileType<Tiles.Herbs.BarfbushPlanterBox>(),
            ModContent.TileType<Tiles.Herbs.SweetstemPlanterBox>(),
            ModContent.TileType<Tiles.Herbs.TwilightPlumePlanterBox>(),
            ModContent.TileType<Tiles.Herbs.HolybirdPlanterBox>()
        };
        TileObjectData.addTile(Type);
        DustType = Dust;
    }
    public override bool CanPlace(int i, int j)
    {
        return Data.Sets.Tile.SuitableForPlantingHerbs[Main.tile[i, j + 1].TileType] &&
               (!Main.tile[i, j].HasTile || Main.tile[i, j].TileType == TileID.Plants || Main.tile[i, j].TileType == Type);
    }
    public override bool IsTileSpelunkable(int i, int j)
    {
        PlantStage stage = GetStage(i, j);

        // Only glow if the herb is grown
        return stage == PlantStage.Blooming;
    }
    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        Player p = ClassExtensions.GetPlayerForTile(i, j);

        if (p.HeldItem.type == ItemID.StaffofRegrowth || p.HeldItem.type == ItemID.AcornAxe)
        {
            PlantStage stage = GetStage(i, j);
            if (stage == PlantStage.Blooming)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return base.CanKillTile(i, j, ref blockDamaged);
    }
    public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
        if (FlipSprite && i % 2 == 1)
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
            yield return new Item(HerbDrop, dropItemStack);
        }
		else if (stage == PlantStage.Mature)
		{
			yield return new Item(HerbDrop, 1);
		}
    }
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        Player p = ClassExtensions.GetPlayerForTile(i, j);
        int secondaryItemStack = Main.rand.Next(3) + 1;
        PlantStage stage = GetStage(i, j);
        bool flag = p.HeldItem.type == ItemID.StaffofRegrowth || p.HeldItem.type == ItemID.AcornAxe;
        if (stage == PlantStage.Blooming)
        {
            if (flag)
            {
                secondaryItemStack = Main.rand.Next(5) + 1;
            }
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i, j).ToWorldCoordinates(), SeedDrop, secondaryItemStack);
        }
    }
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        bool intoRenderTargets = true;
        bool flag = intoRenderTargets || Main.LightingEveryFrame;

        if (Main.tile[i, j].TileFrameX > 16 && flag)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, 3);
            return false;
        }

        return true;
	}
	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = -2;
	}
	public PlantStage GetStage(int i, int j)
    {
        Tile tile = Framing.GetTileSafely(i, j); //Always use Framing.GetTileSafely instead of Main.tile as it prevents any errors caused from other mods
        return (PlantStage)(tile.TileFrameX / 18);
    }
}
