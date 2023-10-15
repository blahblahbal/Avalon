using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class MusicBoxes : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.Origin = new Point16(0, 1);
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.addTile(Type);
        TileID.Sets.DisableSmartCursor[Type] = true;
        AddMapEntry(new Color(200, 200, 200));
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        Tile t = Framing.GetTileSafely(i, j);
        int item = 0;
        switch (t.TileFrameY / 36)
        {
            case 0:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxContagion>();
                break;
            case 1:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxBacteriumPrime>();
                break;
            case 2:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxArmageddonSlime>();
                break;
            case 3:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxDesertBeak>();
                break;
            case 4:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxUndergroundContagion>();
                break;
            case 5:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxTropics>();
                break;
            case 6:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxPhantasm>();
                break;
            case 7:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxDarkMatter>();
                break;
            case 8:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxHellCastle>();
                break;
            case 9:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxTuhrtlOutpost>();
                break;
            case 10:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxSkyFortress>();
                break;
            case 11:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxDesertBeakOtherworldly>();
                break;
        }
        yield return new Item(item);
    }
    public override void HitWire(int i, int j)
    {
        //int num = i;
        //int num2 = j;
        //Tile tile = Main.tile[i, j];
        //int num3;
        //for (num3 = tile.TileFrameY / 18; num3 >= 2; num3 -= 2)
        //{
        //}
        //tile = Main.tile[i, j];
        //int num4 = tile.TileFrameX / 18;
        //if (num4 >= 2)
        //{
        //    num4 -= 2;
        //}
        //num = i - num4;
        //num2 = j - num3;
        //for (int k = num; k < num + 2; k++)
        //{
        //    for (int l = num2; l < num2 + 2; l++)
        //    {
        //        tile = Main.tile[k, l];
        //        if (!tile.HasTile)
        //        {
        //            continue;
        //        }
        //        tile = Main.tile[k, l];
        //        //if (tile.type != 139)
        //        //{
        //        //    tile = Main.tile[k, l];
        //        //    if (tile.TileType != 35 && !TileLoader.IsModMusicBox(Main.tile[k, l]))
        //        //    {
        //        //        continue;
        //        //    }
        //        //}
        //        tile = Main.tile[k, l];
        //        if (tile.TileFrameX < 36)
        //        {
        //            tile = Main.tile[k, l];
        //            tile.TileFrameX += 36;
        //        }
        //        else
        //        {
        //            tile = Main.tile[k, l];
        //            tile.TileFrameX -= 36;
        //        }
        //    }
        //}
        //if (Wiring.running)
        //{
        //    Wiring.SkipWire(num, num2);
        //    Wiring.SkipWire(num + 1, num2);
        //    Wiring.SkipWire(num, num2 + 1);
        //    Wiring.SkipWire(num + 1, num2 + 1);
        //}
        //NetMessage.SendTileSquare(-1, num, num2, 2, 2);
        WorldGen.SwitchMB(i, j);
    }
    public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
    {
        if (Main.tile[i, j].TileFrameX == 36 && Main.tile[i, j].TileFrameY % 36 == 0 && (int)Main.timeForVisualEffects % 7 == 0 && Main.rand.NextBool(3))
        {
            int num12 = Main.rand.Next(570, 573);
            Vector2 position = new Vector2(i * 16 + 8, j * 16 - 8);
            Vector2 velocity = new Vector2(Main.WindForVisuals * 2f, -0.5f);
            velocity.X *= 1f + Main.rand.Next(-50, 51) * 0.01f;
            velocity.Y *= 1f + Main.rand.Next(-50, 51) * 0.01f;
            if (num12 == 572)
            {
                position.X -= 8f;
            }
            if (num12 == 571)
            {
                position.X -= 4f;
            }
            Gore.NewGore(Entity.GetSource_None(), position, velocity, num12, 0.8f);
        }
    }
    public override void MouseOver(int i, int j)
    {
        int item = 0;
        switch (Main.tile[i, j].TileFrameY / 36)
        {
            case 0:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxContagion>();
                break;
            case 1:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxBacteriumPrime>();
                break;
            case 2:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxArmageddonSlime>();
                break;
            case 3:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxDesertBeak>();
                break;
            case 4:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxUndergroundContagion>();
                break;
            case 5:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxTropics>();
                break;
            case 6:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxPhantasm>();
                break;
            case 7:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxDarkMatter>();
                break;
            case 8:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxHellCastle>();
                break;
            case 9:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxTuhrtlOutpost>();
                break;
            case 10:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxSkyFortress>();
                break;
            case 11:
                item = ModContent.ItemType<Items.MusicBoxes.MusicBoxDesertBeakOtherworldly>();
                break;
        }
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = item;
    }
}
