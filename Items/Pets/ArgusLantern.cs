using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Pets;

public class ArgusLantern : ModItem
{
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
        {
            player.AddBuff(Item.buffType, 3600);
        }
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.WispinaBottle);
        Item.shoot = ModContent.ProjectileType<Projectiles.Pets.ArgusLantern>();
        Item.buffType = ModContent.BuffType<Buffs.Pets.ArgusLantern>();
        Item.value = Item.sellPrice(0, 2, 50);
        Item.rare = ItemRarityID.Orange;
    }
}
