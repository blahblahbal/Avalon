using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

class BismuthCandle : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.noWet = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.BismuthCandle>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 99;
        Item.useAnimation = 15;
        Item.holdStyle = 1;
        Item.flame = true;
        Item.height = dims.Height;
    }

    //This whole section is just copied from the vanilla code for gold/platinum candles, is it possible to just reuse the vanilla code instead?
    public override void HoldItem(Player player)
    {
        if (!player.wet && !player.pulley)
        {
            int maxValue2 = 20;
            if (player.itemAnimation > 0)
            {
                maxValue2 = 7;
            }
            if (player.direction == -1)
            {
                if (Main.rand.Next(maxValue2) == 0)
                {
                    int num52 = Dust.NewDust(new Vector2(player.itemLocation.X - 12f, player.itemLocation.Y - 20f * player.gravDir), 4, 4, 6, 0f, 0f, 100);
                    if (Main.rand.Next(3) != 0)
                    {
                        Main.dust[num52].noGravity = true;
                    }
                    Main.dust[num52].velocity *= 0.3f;
                    Main.dust[num52].velocity.Y -= 1.5f;
                    Main.dust[num52].position = player.RotatedRelativePoint(Main.dust[num52].position);
                }
                Lighting.AddLight(player.RotatedRelativePoint(new Vector2(player.itemLocation.X - 16f + player.velocity.X, player.itemLocation.Y - 14f)), 1f, 0.95f, 0.8f);
            }
            else
            {
                if (Main.rand.Next(maxValue2) == 0)
                {
                    int num53 = Dust.NewDust(new Vector2(player.itemLocation.X + 4f, player.itemLocation.Y - 20f * player.gravDir), 4, 4, 6, 0f, 0f, 100);
                    if (Main.rand.Next(3) != 0)
                    {
                        Main.dust[num53].noGravity = true;
                    }
                    Main.dust[num53].velocity *= 0.3f;
                    Main.dust[num53].velocity.Y -= 1.5f;
                    Main.dust[num53].position = player.RotatedRelativePoint(Main.dust[num53].position);
                }
                Lighting.AddLight(player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 6f + player.velocity.X, player.itemLocation.Y - 14f)), 1f, 0.95f, 0.8f);
            }
        }
    }
}
