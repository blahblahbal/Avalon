using Avalon.Dusts;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class PathogenTorch : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
        ItemID.Sets.SingleUseInGamepad[Type] = true;
        ItemID.Sets.Torches[Type] = true;
        ItemID.Sets.WaterTorches[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.DefaultToTorch(ModContent.TileType<Tiles.Furniture.PathogenTorch>(), 0, true);
        Item.value = Item.sellPrice(0, 0, 0, 40);
        Item.notAmmo = true;
        Item.flame = true;
        Item.ammo = 8;
    }
    public override void AddRecipes()
    {
        CreateRecipe(33).AddIngredient(ItemID.Torch, 33).AddIngredient(ModContent.ItemType<Pathogen>()).Register();
    }
    public override void HoldItem(Player player)
    {
        if (Main.rand.NextBool(player.itemAnimation > 0 ? 10 : 20))
        {
            Dust d = Dust.NewDustDirect(new Vector2(player.itemLocation.X + (player.direction == 1 ? 6 : -16), player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, Main.rand.NextFloat(0.5f, 1));
            d.velocity.Y = Main.rand.NextFloat(-0.5f, -2);
            d.velocity.X *= 0.2f;
            d.noGravity = true;
        }
        Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
        Lighting.AddLight(position, 0.5f, 0, 2f);
    }

    public override void PostUpdate()
    {
        Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.5f, 0, 2f);
    }
}
