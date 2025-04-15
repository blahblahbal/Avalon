using Avalon.Dusts;
using Avalon.Items.Material.Ores;
using Avalon.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class LimeTorch : ModItem
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
        CreateRecipe(10).AddIngredient(ItemID.Torch, 10).AddIngredient(ModContent.ItemType<Material.Ores.Peridot>()).Register();
    }
    public override void HoldItem(Player player)
    {
        if (!player.wet)
        {
            if (Main.rand.NextBool(player.itemAnimation > 0 ? 28 : 120))
            {
                int d = Dust.NewDust(new Vector2(player.itemLocation.X + (player.direction == 1 ? 6 : -16), player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<LimeTorchDust>(), 0f, 0f, 100);
                if (!Main.rand.NextBool(3))
                {
                    Main.dust[d].noGravity = true;
                }
                Main.dust[d].velocity *= 0.3f;
                Main.dust[d].velocity.Y -= 1.5f;
                Main.dust[d].position = player.RotatedRelativePoint(Main.dust[d].position);
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
