using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class GoblinDagger : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GoldShortsword);
        Item.damage = 30;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.GoblinDagger>();
        Item.scale = 0.95f;
        Item.value = Item.sellPrice(0, 0, 50, 0);
        Item.rare = ItemRarityID.Green;
    }
}
