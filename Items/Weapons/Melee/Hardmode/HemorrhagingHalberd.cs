using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class HemorrhagingHalberd : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 48;
        Item.UseSound = SoundID.Item1;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 4f;
        Item.rare = ItemRarityID.LightRed;
        Item.noMelee = true;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.knockBack = 4.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.HemorrhagingHalberd>();
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 3);
        Item.UseSound = SoundID.Item1;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}
