using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Pets;

class PetriDish : ModItem
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
        Item.rare = ItemRarityID.Master;
        Item.shoot = ModContent.ProjectileType<Projectiles.Pets.BacteriumPet>();
        Item.buffType = ModContent.BuffType<Buffs.Pets.Bacterium>();
        Item.value = Item.sellPrice(0, 1, 50);
    }
}
