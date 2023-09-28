using Avalon.Common.Players;
using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonGlobalTile : GlobalTile
{
    public override void SetStaticDefaults()
    {
        int[] spelunkers = { TileID.Crimtane, TileID.Meteorite, TileID.Obsidian, TileID.Hellstone };
        int[] ores = { TileID.Topaz, TileID.Ruby, TileID.Amethyst, TileID.Diamond, TileID.Emerald, TileID.Sapphire, TileID.AmberStoneBlock };
        foreach (int tile in spelunkers)
        {
            Main.tileSpelunker[tile] = true;
        }
        foreach(int tile in ores)
        {
            TileID.Sets.Ore[tile] = true;
        }
    }
    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        if (Main.tile[i, j].LiquidAmount > 0 && Main.tile[i, j].LiquidType == LiquidID.Honey)
        {
            Main.tile[i, j].LiquidAmount--;
            Tile tile = Main.tile[i, j];
            var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            int frameY = (54 - Main.tile[i, j].LiquidAmount) / 3;
            int frameX = 0;
            if (frameY > 7)
            {
                frameY -= 7;
                frameX++;
            }

            Main.NewText(tile.TileFrameX + frameX * 288);

            Vector2 pos = new Vector2(i, j) + (zero - Main.screenPosition) / 16;
            //Main.NewText(pos);
            var frame = new Rectangle(tile.TileFrameX + frameX * 288, tile.TileFrameY + frameY * 270, 16, 16);
            var halfFrame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 8);

            Texture2D tex = ModContent.Request<Texture2D>("Avalon/Assets/Textures/OreRiftAnimation").Value;
            if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
            {
                spriteBatch.Draw(tex, pos, frame, Color.White);
            }



            //int num9 = 16;
            //int num10 = 0;
            //int num11 = 16;
            
            ////Main.NewText(zero);
            ////Main.NewText(new Vector2(i - (int)Main.screenPosition.X / 16 - (num9 - 16f) / 2f, j - (int)Main.screenPosition.Y / 16 + num10));

            

            //spriteBatch.Draw(tex,
            //    new Vector2(i - (int)Main.screenPosition.X / 16 - (num9 - 16f) / 2f, j - (int)Main.screenPosition.Y / 16 + num10),
            //    //new Vector2((float)(i - (num9 - 16f) / 2f), (j + num10)) + zero,
            //    new Rectangle?(new Rectangle(Main.tile[i, j].TileFrameX * frameX, Main.tile[i, j].TileFrameY * frameY, num9, num11)),
            //    Color.White,
            //    0f,
            //    default(Vector2),
            //    1f,
            //    SpriteEffects.None,
            //    0f);

            //Vector2 unscaledPosition = Main.Camera.UnscaledPosition;

            //Rectangle rectangle = new(Main.tile[i, j].TileFrameX, Main.tile[i, j].TileFrameY, 16, 16 - 8);
            //Vector2 vector = new Vector2((float)(i - (int)unscaledPosition.X) - ((float)16 - 16f) / 2f, (float)(j - (int)unscaledPosition.Y + drawData.tileTop + drawData.halfBrickHeight)) + screenOffset;

        }
    }

    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (Data.Sets.Tile.RiftOres[type])
        {
            Main.NewText(new Vector2(i, j));
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16 + 32, j * 16 + 32, 8, 8, ModContent.ItemType<Items.OreRift>());

        }
        if (Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)].GetModPlayer<AvalonPlayer>().OreDupe && TileID.Sets.Ore[Main.tile[i, j].TileType])
        {
            if (Data.Sets.Tile.OresToChunks.ContainsKey(Main.tile[i, j].TileType))
            {
                int drop = Data.Sets.Tile.OresToChunks[Main.tile[i, j].TileType];
                if (Main.rand.NextBool(3))
                {
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop);
                }
                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop);
                noItem = true;
            }
        }
        // four leaf clover drops
        if (type is TileID.CorruptPlants or TileID.JunglePlants or TileID.JunglePlants2 or TileID.CrimsonPlants or TileID.Plants or TileID.Plants2 || type == ModContent.TileType<Tiles.Contagion.ContagionShortGrass>())
        {
            if (Main.rand.NextBool(8000))
            {
                int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<FourLeafClover>(), 1, false, 0);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.FromLiteral(""), a, 0f, 0f, 0f, 0);
                    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
                }
            }
            else if (Main.rand.NextBool(500))
            {
                int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<FakeFourLeafClover>(), 1, false, 0);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.FromLiteral(""), a, 0f, 0f, 0f, 0);
                    Main.item[a].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[a].position, 8, 8);
                }
            }
        }
    }
}
