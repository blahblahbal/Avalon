using Avalon.Items.Material.Ores;
using Avalon.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

class LimeTorch : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
        ItemID.Sets.SingleUseInGamepad[Type] = true;
        ItemID.Sets.Torches[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.DefaultToTorch(ModContent.TileType<Tiles.Furniture.LimeTorch>(), 0, false);
        Item.value = Item.sellPrice(0, 0, 0, 40);
        Item.notAmmo = true;
        Item.flame = true;
        Item.ammo = 8;
    }
    public override void AddRecipes()
    {
        CreateRecipe(3).AddIngredient(ItemID.Torch, 3).AddIngredient(ModContent.ItemType<Material.Ores.Peridot>()).Register();
    }
    public override void HoldItem(Player player)
    {
        if (!player.wet)
        {
            if (Main.rand.NextBool(player.itemAnimation > 0 ? 10 : 20))
            {
                Dust d = Dust.NewDustDirect(new Vector2(player.itemLocation.X + (player.direction == 1 ? 6 : -16), player.itemLocation.Y - 14f * player.gravDir), 4, 4, DustID.GreenTorch, 0, 0, 128, default, Main.rand.NextFloat(0.5f, 1));
                d.noLightEmittence = true; //temporary until unique dust
                d.velocity.Y = Main.rand.NextFloat(-0.5f, -2);
                d.velocity.X *= 0.2f;
            }
            Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
            Lighting.AddLight(position, 0.714f, 1f, 0);
        }
    }

    public override void PostUpdate()
    {
        if (!Item.wet)
        {
            Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.714f, 1f, 0);
        }
    }
}
