using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

internal class BagofFrost : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 1);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            UpdateVanity(player);
        }
    }

    public override void UpdateVanity(Player player)
    {
        if (!(player.velocity.Length() > 0))
        {
            return;
        }

        for (int num73 = 0; num73 < 5; num73++)
        {
            float num74 = player.velocity.X / 3f * num73;
            float num75 = player.velocity.Y / 3f * num73;
            int num76 = 4;
            int num77 = Dust.NewDust(new Vector2(player.position.X + num76 - num74, player.position.Y + num76 - num75),
                player.width - (num76 * 2), player.height - (num76 * 2), DustID.IceTorch, 0f, 0f, 100, default,
                1.8f);
            Main.dust[num77].noGravity = true;
            Main.dust[num77].velocity *= 0.1f;
            Main.dust[num77].velocity += player.velocity * 0.1f;
        }

        if (Main.rand.Next(3) == 0)
        {
            int num78 = 4;
            int num79 = Dust.NewDust(new Vector2(player.position.X + num78, player.position.Y + num78),
                player.width - (num78 * 2), player.height - (num78 * 2), DustID.MagicMirror, 0f, 0f, 100, default,
                0.6f);
            Main.dust[num79].noGravity = true;
            Main.dust[num79].velocity *= 0.25f;
            Main.dust[num79].velocity += player.velocity * 0.5f;
        }
    }
}
