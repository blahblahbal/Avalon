using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;

namespace Avalon.Items.Accessories.Hardmode;

class RiftGoggles : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 50000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().RiftGoggles = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ItemID.CursedFlame, 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ItemID.Ichor, 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ModContent.ItemType<Pathogen>(), 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();

        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ItemID.CursedFlame, 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ItemID.Ichor, 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ModContent.ItemType<Pathogen>(), 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
    }
}
class RiftGogglesPlayer : ModPlayer
{
    public override void PostUpdate()
    {
        #region rift goggles
        // mobs
        if (Player.ZoneCrimson || Player.ZoneCorrupt || Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion)
        {
            if (Main.rand.NextBool(5500) && Player.GetModPlayer<AvalonPlayer>().RiftGoggles)
            {
                Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-20 * 16, 21 * 16), Main.rand.Next(-20 * 16, 21 * 16));
                Point pt = pposTile2.ToTileCoordinates();
                if (!Main.tile[pt.X, pt.Y].HasTile)
                {
                    int proj = NPC.NewNPC(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16, pt.Y * 16, ModContent.NPCType<NPCs.MobRift>(), 0);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, proj);
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        int num893 = Dust.NewDust(Main.npc[proj].position, Main.npc[proj].width, Main.npc[proj].height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
                        Main.dust[num893].velocity *= 2f;
                        Main.dust[num893].scale = 0.9f;
                        Main.dust[num893].noGravity = true;
                        Main.dust[num893].fadeIn = 3f;
                    }
                }
            }
        }
        if (ExxoAvalonOrigins.Confection != null)
        {
            if (Player.ZoneHallow || Player.InModBiome(ExxoAvalonOrigins.Confection.Find<ModBiome>("ConfectionBiome")))
            {
                if (Main.rand.NextBool(5500) && Player.GetModPlayer<AvalonPlayer>().RiftGoggles)
                {
                    Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-20 * 16, 21 * 16), Main.rand.Next(-20 * 16, 21 * 16));
                    Point pt = pposTile2.ToTileCoordinates();
                    if (!Main.tile[pt.X, pt.Y].HasTile)
                    {
                        int proj = NPC.NewNPC(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16, pt.Y * 16, ModContent.NPCType<NPCs.MobRift>(), ai1: 1);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, proj);
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            int num893 = Dust.NewDust(Main.npc[proj].position, Main.npc[proj].width, Main.npc[proj].height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
                            Main.dust[num893].velocity *= 2f;
                            Main.dust[num893].scale = 0.9f;
                            Main.dust[num893].noGravity = true;
                            Main.dust[num893].fadeIn = 3f;
                        }
                    }
                }
            }
        }
        // ores
        if (Player.GetModPlayer<AvalonPlayer>().RiftGoggles && Main.rand.NextBool(3200))
        {
            if (Player.ZoneRockLayerHeight)
            {
                Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-50 * 16, 50 * 16), Main.rand.Next(-35 * 16, 35 * 16));
                Point pt = pposTile2.ToTileCoordinates();
                for (int q = 0; q < 50; q++)
                {
                    if (!Data.Sets.Tile.RiftOres[Main.tile[pt.X, pt.Y].TileType] || Main.tile[pt.X, pt.Y].LiquidType == LiquidID.Honey)
                    {
                        pposTile2 = Player.position + new Vector2(Main.rand.Next(-50 * 16, 50 * 16), Main.rand.Next(-35 * 16, 35 * 16));
                        pt = pposTile2.ToTileCoordinates();
                    }
                    else break;
                }
                if (Data.Sets.Tile.RiftOres[Main.tile[pt.X, pt.Y].TileType])
                {
                    Tile t = Main.tile[pt.X, pt.Y];
                    t.LiquidType = LiquidID.Honey;
                    t.LiquidAmount = 54;
                    int rift = Item.NewItem(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16 + 10, pt.Y * 16 + 10, 8, 8, ModContent.ItemType<OreRift>());
                    Main.item[rift].playerIndexTheItemIsReservedFor = Player.whoAmI;
                    Main.item[rift].keepTime = 600;
                    Main.item[rift].GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft = 300;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, rift);
                    }
                }
            }
        }
        // fishing
        /*if (Player.ZoneCrimson || Player.ZoneCorrupt || Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion)
        {
            if (Main.rand.NextBool(15) && RiftGoggles)
            {
                Vector2 pposTile2 = Player.position + new Vector2(Main.rand.Next(-30 * 16, 21 * 16), Main.rand.Next(-30 * 16, 21 * 16));
                Point pt = pposTile2.ToTileCoordinates();
                //can spawn underwater if there's an overhang, needs to be fixed
                if (Main.tile[pt.X, pt.Y].LiquidType == LiquidID.Water && Main.tile[pt.X, pt.Y].LiquidAmount > 100 &&
                    Main.tile[pt.X, pt.Y - 3].LiquidAmount == 0 && Main.tile[pt.X, pt.Y - 2].LiquidAmount > 1) //  && (!Main.tile[pt.X, pt.Y - 3].HasTile || Main.tile[pt.X, pt.Y - 3].HasUnactuatedTile)
                {
                    if (ClassExtensions.CanSpawnFishingRift(new Vector2(pt.X * 16, pt.Y * 16), ModContent.NPCType<NPCs.FishingRift>(), 16 * 20))
                    {
                        int proj = NPC.NewNPC(Player.GetSource_TileInteraction(pt.X, pt.Y), pt.X * 16, pt.Y * 16, ModContent.NPCType<NPCs.FishingRift>(), 0);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, proj);
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            int num893 = Dust.NewDust(Main.npc[proj].position, Main.npc[proj].width, Main.npc[proj].height, DustID.Enchanted_Pink, 0f, 0f, 0, default, 1f);
                            Main.dust[num893].velocity *= 2f;
                            Main.dust[num893].scale = 0.9f;
                            Main.dust[num893].noGravity = true;
                            Main.dust[num893].fadeIn = 3f;
                        }
                    }
                }
            }
        }*/
        #endregion rift goggles
    }
}
class RiftGogglesGlobalTile : GlobalTile
{
    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        if (!Main.tileSolid[Main.tile[i, j].TileType])
        {
            return;
        }
        if (Main.tile[i, j].LiquidAmount >= 0 && Main.tile[i, j].LiquidType == LiquidID.Honey)
        {
            if (Main.tile[i, j].LiquidAmount == 0)
            {
                Tile tt = Main.tile[i, j];
                tt.LiquidType = LiquidID.Water;
                return;
            }

            Tile tile = Main.tile[i, j];
            var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            int frameY = (54 - Main.tile[i, j].LiquidAmount) / 3;
            int frameX = 0;
            if (frameY > 6 && frameY <= 13)
            {
                frameY -= 7;
                frameX = 1;
            }
            else if (frameY > 13)
            {
                frameY -= 14;
                frameX = 2;
            }
            Vector2 pos = new Vector2(i, j) * 16 + zero - Main.screenPosition;
            var frame = new Rectangle(tile.TileFrameX + frameX * 288, tile.TileFrameY + frameY * 270, 16, 16);
            var halfFrame = new Rectangle(tile.TileFrameX + frameX * 288, tile.TileFrameY + frameY * 270, 16, 8);

            Texture2D tex = ModContent.Request<Texture2D>("Avalon/Assets/Textures/OreRiftAnimation").Value;
            if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
            {
                spriteBatch.Draw(tex, pos, frame, Color.White);
            }
            else if (tile.IsHalfBlock)
            {
                pos = new Vector2(i * 16, (j * 16) + 8) + zero - Main.screenPosition;
                spriteBatch.Draw(tex, pos, halfFrame, Color.White);
            }
            else
            {
                Vector2 screenOffset = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen)
                {
                    screenOffset = Vector2.Zero;
                }
                Vector2 vector = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + screenOffset;
                int slopeType = (int)tile.Slope;
                int num5 = 2;
                int addFrY = Main.tileFrame[type] * 90;
                int addFrX = 0;
                for (int q = 0; q < 8; q++)
                {
                    int num6 = q * -2;
                    int num7 = 16 - q * 2;
                    int num8 = 16 - num7;
                    int num9;
                    switch (slopeType)
                    {
                        case 1:
                            num6 = 0;
                            num9 = q * 2;
                            num7 = 14 - q * 2;
                            num8 = 0;
                            break;
                        case 2:
                            num6 = 0;
                            num9 = 16 - q * 2 - 2;
                            num7 = 14 - q * 2;
                            num8 = 0;
                            break;
                        case 3:
                            num9 = q * 2;
                            break;
                        default:
                            num9 = 16 - q * 2 - 2;
                            break;
                    }
                    Main.spriteBatch.Draw(tex, vector + new Vector2(num9, q * num5 + num6), (Rectangle?)new Rectangle(tile.TileFrameX + frameX * 288 + addFrX + num9, tile.TileFrameY + frameY * 270 + addFrY + num8, num5, num7), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
                int num10 = ((slopeType <= 2) ? 14 : 0);
                Main.spriteBatch.Draw(tex, vector + new Vector2(0f, num10), (Rectangle?)new Rectangle(tile.TileFrameX + frameX * 288 + addFrX, tile.TileFrameY + frameY * 270 + addFrY + num10, 16, 2), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            Main.tile[i, j].LiquidAmount--;
        }
    }
}
public class OreRift : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemNoGravity[Type] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.alpha = 120;
    }
    public override bool CanPickup(Player player)
    {
        return false;
    }
    public override void PostUpdate()
    {
        Point tile = Item.Center.ToTileCoordinates();
        if (Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft > 260)
        {
            Item.alpha -= 3;
        }
        if (Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft < 150)
        {
            Item.alpha += 5;
        }
        if (Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft == 250)
        {
            #region copper
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Copper)
            {
                Honeyify(tile, TileID.Copper);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tin)
            {
                Honeyify(tile, TileID.Tin);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BronzeOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.BronzeOre>());
            }
            #endregion
            #region iron
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Iron)
            {
                Honeyify(tile, TileID.Iron);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Lead)
            {
                Honeyify(tile, TileID.Lead);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.NickelOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.NickelOre>());
            }
            #endregion
            #region silver
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Silver)
            {
                Honeyify(tile, TileID.Silver);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tungsten)
            {
                Honeyify(tile, TileID.Tungsten);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.ZincOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.ZincOre>());
            }
            #endregion
            #region gold
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Gold)
            {
                Honeyify(tile, TileID.Gold);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Platinum)
            {
                Honeyify(tile, TileID.Platinum);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BismuthOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.BismuthOre>());
            }
            #endregion
            #region rhodium
            if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.RhodiumOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.RhodiumOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.OsmiumOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.OsmiumOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.IridiumOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.IridiumOre>());
            }
            #endregion
            #region evil
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Demonite)
            {
                Honeyify(tile, TileID.Demonite);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Crimtane)
            {
                Honeyify(tile, TileID.Crimtane);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BacciliteOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.BacciliteOre>());
            }
            #endregion
            #region cobalt
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Cobalt)
            {
                Honeyify(tile, TileID.Cobalt);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Palladium)
            {
                Honeyify(tile, TileID.Palladium);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.DurataniumOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.DurataniumOre>());
            }
            #endregion
            #region mythril
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Mythril)
            {
                Honeyify(tile, TileID.Mythril);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Orichalcum)
            {
                Honeyify(tile, TileID.Orichalcum);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.NaquadahOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.NaquadahOre>());
            }
            #endregion
            #region adamantite
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Adamantite)
            {
                Honeyify(tile, TileID.Adamantite);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Titanium)
            {
                Honeyify(tile, TileID.Titanium);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.TroxiniumOre>())
            {
                Honeyify(tile, ModContent.TileType<Tiles.Ores.TroxiniumOre>());
            }
            #endregion
        }
        if (Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft == 150)
        {
            #region copper
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Copper)
            {
                RiftReplace(tile, TileID.Copper, TileID.Tin);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tin)
            {
                RiftReplace(tile, TileID.Tin, ModContent.TileType<Tiles.Ores.BronzeOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BronzeOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.BronzeOre>(), TileID.Copper);
            }
            #endregion
            #region iron
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Iron)
            {
                RiftReplace(tile, TileID.Iron, TileID.Lead);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Lead)
            {
                RiftReplace(tile, TileID.Lead, ModContent.TileType<Tiles.Ores.NickelOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.NickelOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.NickelOre>(), TileID.Iron);
            }
            #endregion
            #region silver
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Silver)
            {
                RiftReplace(tile, TileID.Silver, TileID.Tungsten);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tungsten)
            {
                RiftReplace(tile, TileID.Tungsten, ModContent.TileType<Tiles.Ores.ZincOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.ZincOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.ZincOre>(), TileID.Silver);
            }
            #endregion
            #region gold
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Gold)
            {
                RiftReplace(tile, TileID.Gold, TileID.Platinum);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Platinum)
            {
                RiftReplace(tile, TileID.Platinum, ModContent.TileType<Tiles.Ores.BismuthOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BismuthOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.BismuthOre>(), TileID.Gold);
            }
            #endregion
            #region rhodium
            if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.RhodiumOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.RhodiumOre>(), ModContent.TileType<Tiles.Ores.OsmiumOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.OsmiumOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.OsmiumOre>(), ModContent.TileType<Tiles.Ores.IridiumOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.IridiumOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.IridiumOre>(), ModContent.TileType<Tiles.Ores.RhodiumOre>());
            }
            #endregion
            #region evil
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Demonite)
            {
                RiftReplace(tile, TileID.Demonite, TileID.Crimtane);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Crimtane)
            {
                RiftReplace(tile, TileID.Crimtane, ModContent.TileType<Tiles.Ores.BacciliteOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BacciliteOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.BacciliteOre>(), TileID.Demonite);
            }
            #endregion
            #region cobalt
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Cobalt)
            {
                RiftReplace(tile, TileID.Cobalt, TileID.Palladium);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Palladium)
            {
                RiftReplace(tile, TileID.Palladium, ModContent.TileType<Tiles.Ores.DurataniumOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.DurataniumOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.DurataniumOre>(), TileID.Cobalt);
            }
            #endregion
            #region mythril
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Mythril)
            {
                RiftReplace(tile, TileID.Mythril, TileID.Orichalcum);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Orichalcum)
            {
                RiftReplace(tile, TileID.Orichalcum, ModContent.TileType<Tiles.Ores.NaquadahOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.NaquadahOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.NaquadahOre>(), TileID.Mythril);
            }
            #endregion
            #region adamantite
            if (Main.tile[tile.X, tile.Y].TileType == TileID.Adamantite)
            {
                RiftReplace(tile, TileID.Adamantite, TileID.Titanium);
            }
            else if (Main.tile[tile.X, tile.Y].TileType == TileID.Titanium)
            {
                RiftReplace(tile, TileID.Titanium, ModContent.TileType<Tiles.Ores.TroxiniumOre>());
            }
            else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.TroxiniumOre>())
            {
                RiftReplace(tile, ModContent.TileType<Tiles.Ores.TroxiniumOre>(), TileID.Adamantite);
            }
            #endregion
        }
        Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft--;
        if (Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft == 0)
        {
            Item.active = false;
        }
    }
    public static List<List<Point>> AddValidNeighbors(List<List<Point>> p, Point start)
    {
        p.Add(new List<Point>()
        {
            start + new Point(0, -1), start + new Point(0, 1), start + new Point(-1, 0), start + new Point(1, 0)
        });

        return p;
    }

    public static void Honeyify(Point p, int type, int maxTiles = 600)
    {
        int tiles = 0;

        Tile tile = Framing.GetTileSafely(p);
        if (!tile.HasTile || tile.TileType != type)
        {
            return;
        }

        List<List<Point>> points = new List<List<Point>>();
        points = AddValidNeighbors(points, p);

        int index = 0;
        
        while (points.Count > 0 && tiles < maxTiles && index < points.Count)
        {
            List<Point> tilePos = points[index];
            foreach (Point a in tilePos)
            {
                Tile t = Framing.GetTileSafely(a.X, a.Y);
                if (t.HasTile && t.TileType == type && t.LiquidAmount == 0)
                {
                    t.LiquidAmount = 54;
                    t.LiquidType = LiquidID.Honey;
                    tiles++;
                    AddValidNeighbors(points, a);
                }
                
            }
            index++;
        }
    }

    public static void RiftReplace(Point p, int type, int replace, bool honeyify = false, int maxTiles = 600)
    {
        int tiles = 0;

        Tile tile = Framing.GetTileSafely(p);
        if (!tile.HasTile || tile.TileType != type)
        {
            return;
        }

        List<List<Point>> points = new List<List<Point>>();
        points = AddValidNeighbors(points, p);

        int index = 0;
        while (points.Count > 0 && tiles < maxTiles && index < points.Count)
        {
            List<Point> tilePos = points[index];

            foreach (Point a in tilePos)
            {
                Tile t = Framing.GetTileSafely(a.X, a.Y);
                if (t.HasTile && t.TileType == type && t.LiquidType == LiquidID.Honey)
                {
                    Tile q = Framing.GetTileSafely(a.X, a.Y);
                    q.TileType = (ushort)replace;
                    WorldGen.SquareTileFrame(a.X, a.Y);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 21, a.X, a.Y, replace);
                    }
                    tiles++;
                    AddValidNeighbors(points, a);
                }
            }
            index++;
        }
    }
}
