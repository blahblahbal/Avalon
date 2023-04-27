using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Pets;

class SnotOrb : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
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
        Item.shoot = ModContent.ProjectileType<Projectiles.SnotOrb>();
        Item.buffType = ModContent.BuffType<Buffs.SnotOrb>();
        Item.value = Item.sellPrice(0, 0, 54);
        Item.rare = ItemRarityID.Blue;
    }
}
