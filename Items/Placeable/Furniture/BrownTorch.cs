using Avalon.Items.Material.Ores;
using Avalon.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

class BrownTorch : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }
    public override void SetDefaults()
    {
        Item.DefaultToTorch(ModContent.TileType<Tiles.Furniture.BrownTorch>(), 0, false);
        Item.value = Item.sellPrice(0, 0, 0, 40);
        Item.notAmmo = true;
        Item.flame = true;
        Item.ammo = 8;
    }
    public override void AddRecipes()
    {
        CreateRecipe(3).AddIngredient(ItemID.Torch, 3).AddIngredient(ModContent.ItemType<Zircon>()).Register();
    }
    public override void HoldItem(Player player)
    {
        if (Main.rand.NextBool(player.itemAnimation > 0 ? 10 : 20))
        {
            Dust d = Dust.NewDustDirect(new Vector2(player.itemLocation.X + (player.direction == 1 ? 6 : -16), player.itemLocation.Y - 14f * player.gravDir), 4, 4, DustID.WhiteTorch, 0, 0, 128, default, Main.rand.NextFloat(0.5f, 1));
            d.velocity.Y = Main.rand.NextFloat(-0.5f, -2);
            d.velocity.X *= 0.2f;
        }
        Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
        Lighting.AddLight(position, 1.51372552f, 1.16078436f, 0.9254902f);
    }

    public override void PostUpdate()
    {
        if (!Item.wet)
        {
            Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 1.51372552f, 1.16078436f, 0.9254902f);
        }
    }
}
