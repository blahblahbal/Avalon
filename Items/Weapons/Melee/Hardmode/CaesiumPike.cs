using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class CaesiumPike : ModItem
{
    //public override void SetStaticDefaults()
    //{
    //    DisplayName.SetDefault("Caesium Pike");
    //    Tooltip.SetDefault("Explodes with each hit\n'Poke!'");
    //    SacrificeTotal = 1;
    //}
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 120;
        Item.UseSound = SoundID.Item1;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 4f;
        Item.rare = ItemRarityID.Lime;
        Item.noMelee = true;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.knockBack = 4.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.CaesiumPike>();
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 20, 0, 0);
        Item.UseSound = SoundID.Item1;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}
