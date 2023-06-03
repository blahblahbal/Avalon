using Avalon.Items.Potions.Other;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Common.Templates;

public abstract class ModTorch : ModTile
{
    public virtual int DustType => 0;
    public virtual int TorchItem => 0;
    public virtual Vector3 LightColor => Vector3.One;
    public virtual bool WaterDeath => true;
    public virtual bool NoDustGravity => true;
    public override void SetStaticDefaults()
    {
        RegisterItemDrop(TorchItem);

        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileWaterDeath[Type] = WaterDeath;
        TileID.Sets.Torch[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.StyleTorch);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
        TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
        TileObjectData.newAlternate.AnchorAlternateTiles = new[] { 124 };
        TileObjectData.addAlternate(1);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
        TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
        TileObjectData.newAlternate.AnchorAlternateTiles = new[] { 124 };
        TileObjectData.addAlternate(2);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
        TileObjectData.newAlternate.AnchorWall = true;
        TileObjectData.addAlternate(0);
        TileObjectData.addTile(Type);
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
        AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Torch"));
        //DustType = DustID.JungleSpore;
        AdjTiles = new int[] { TileID.Torches };
    }
    public override bool CreateDust(int i, int j, ref int type)
    {
        type = DustType;
        return true;
    }

    public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
    {
        if (Main.rand.NextBool(100) && Main.tile[i, j].TileFrameX < 66)
        {
            Dust d = Dust.NewDustDirect(new Vector2(i * 16,j * 16) + new Vector2(6,-6), 0, 0, DustType, 0, 0, 128, default, Main.rand.NextFloat(0.5f, 1));
            d.velocity.Y = Main.rand.NextFloat(-0.5f, -2);
            d.velocity.X *= 0.2f;
            d.noGravity = NoDustGravity;
        }
    }
    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = Main.rand.Next(1, 3);
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        var tile = Main.tile[i, j];
        if (tile.TileFrameX < 66)
        {
            //int style = Main.tile[i, j].TileFrameY / 18;
            //switch (style)
            //{
            //    case 0:
            //        r = 0f;
            //        g = 2f;
            //        b = 2f;
            //        break;
            //    case 1:
            //        r = 1.427451f;
            //        g = 2f;
            //        b = 0f;
            //        break;
            //    case 2:
            //        r = 1.51372552f;
            //        g = 1.16078436f;
            //        b = 0.9254902f;
            //        break;
            //    default:
            //        r = 1f;
            //        g = 0.95f;
            //        b = 0.8f;
            //        break;
            //}
            r = LightColor.X;
            g = LightColor.Y;
            b = LightColor.Z;
        }
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        offsetY = 0;
        if (WorldGen.InWorld(i, j - 1) && WorldGen.SolidTile(i, j - 1))
        {
            offsetY = 2;
            if (WorldGen.InWorld(i - 1, j + 1) && WorldGen.SolidTile(i - 1, j + 1) || WorldGen.InWorld(i + 1, j + 1) && WorldGen.SolidTile(i + 1, j + 1))
            {
                offsetY = 4;
            }
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        var randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)(ulong)i);
        var color = new Color(100, 100, 100, 0);
        int frameX = Main.tile[i, j].TileFrameX;
        int frameY = Main.tile[i, j].TileFrameY;
        var width = 20;
        var offsetY = 0;
        var height = 20;
        if (WorldGen.SolidTile(i, j - 1))
        {
            offsetY = 2;
            if (WorldGen.SolidTile(i - 1, j + 1) || WorldGen.SolidTile(i + 1, j + 1))
            {
                offsetY = 4;
            }
        }
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        for (var k = 0; k < 7; k++)
        {
            var x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
            var y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Flame").Value, new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + x, j * 16 - (int)Main.screenPosition.Y + offsetY + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
    }
    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = TorchItem;
    }
    public override bool RightClick(int i, int j)
    {
        WorldGen.KillTile(i, j, false, false, true);
        if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
        {
            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
        }
        return true;
    }

    //public override void MouseOver(int i, int j)
    //{
    //    Player player = Main.LocalPlayer;
    //    player.noThrow = 2;
    //    player.cursorItemIconEnabled = true;
    //    var style = Main.tile[i, j].TileFrameY / 22;

    //    switch (style)
    //    {
    //        case 0:
    //            player.cursorItemIconID = ModContent.ItemType<HoneyTorch>();
    //            break;
    //        case 1:
    //            player.cursorItemIconID = ModContent.ItemType<PathogenTorch>();
    //            break;
    //        case 2:
    //            player.cursorItemIconID = ModContent.ItemType<SlimeTorch>();
    //            break;
    //        case 3:
    //            player.cursorItemIconID = ModContent.ItemType<CyanTorch>();
    //            break;
    //        case 4:
    //            player.cursorItemIconID = ModContent.ItemType<LimeTorch>();
    //            break;
    //        case 5:
    //            player.cursorItemIconID = ModContent.ItemType<BrownTorch>();
    //            break;
    //    }
    //}
}
