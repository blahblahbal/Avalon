using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Pets;

class ArgusLantern : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.LightPet;
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
        Item.shoot = ModContent.ProjectileType<Projectiles.ArgusLantern>();
        Item.buffType = ModContent.BuffType<Buffs.Pets.ArgusLantern>();
        Item.value = Item.sellPrice(0, 2, 50);
        Item.rare = ItemRarityID.Orange;
    }
}
