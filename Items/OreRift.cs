using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items
{
    public class OreRift : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Heart);
        }
        public override bool CanPickup(Player player)
        {
            return false;
        }
        public override void PostUpdate()
        {
            Point tile = Item.Center.ToTileCoordinates();

            if (Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft == 150)
            {
                #region copper
                if (Main.tile[tile.X, tile.Y].TileType == TileID.Copper)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Copper, TileID.Tin);
                }
                else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tin)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Tin, ModContent.TileType<Tiles.Ores.BronzeOre>());
                }
                else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BronzeOre>())
                {
                    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.BronzeOre>(), TileID.Copper);
                }
                #endregion
                #region iron
                if (Main.tile[tile.X, tile.Y].TileType == TileID.Iron)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Iron, TileID.Lead);
                }
                else if (Main.tile[tile.X, tile.Y].TileType == TileID.Lead)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Lead, ModContent.TileType<Tiles.Ores.NickelOre>());
                }
                else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.NickelOre>())
                {
                    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.NickelOre>(), TileID.Iron);
                }
                #endregion
                #region silver
                if (Main.tile[tile.X, tile.Y].TileType == TileID.Silver)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Silver, TileID.Tungsten);
                }
                else if (Main.tile[tile.X, tile.Y].TileType == TileID.Tungsten)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Tungsten, ModContent.TileType<Tiles.Ores.ZincOre>());
                }
                else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.ZincOre>())
                {
                    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.ZincOre>(), TileID.Silver);
                }
                #endregion
                #region gold
                if (Main.tile[tile.X, tile.Y].TileType == TileID.Gold)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Gold, TileID.Platinum);
                }
                else if (Main.tile[tile.X, tile.Y].TileType == TileID.Platinum)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Platinum, ModContent.TileType<Tiles.Ores.BismuthOre>());
                }
                else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BismuthOre>())
                {
                    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.BismuthOre>(), TileID.Gold);
                }
                #endregion
                #region rhodium
                if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.RhodiumOre>())
                {
                    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.RhodiumOre>(), ModContent.TileType<Tiles.Ores.OsmiumOre>());
                }
                else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.OsmiumOre>())
                {
                    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.OsmiumOre>(), ModContent.TileType<Tiles.Ores.IridiumOre>());
                }
                else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.IridiumOre>())
                {
                    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.IridiumOre>(), ModContent.TileType<Tiles.Ores.RhodiumOre>());
                }
                #endregion
                #region evil
                if (Main.tile[tile.X, tile.Y].TileType == TileID.Demonite)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Demonite, TileID.Crimtane);
                }
                else if (Main.tile[tile.X, tile.Y].TileType == TileID.Crimtane)
                {
                    ClassExtensions.RiftReplace(tile, TileID.Crimtane, ModContent.TileType<Tiles.Ores.BacciliteOre>());
                }
                else if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<Tiles.Ores.BacciliteOre>())
                {
                    ClassExtensions.RiftReplace(tile, ModContent.TileType<Tiles.Ores.BacciliteOre>(), TileID.Demonite);
                }
                #endregion
            }
            Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft--;
            if (Item.GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft == 0)
            {
                Item.active = false;
            }
        }
    }
}
