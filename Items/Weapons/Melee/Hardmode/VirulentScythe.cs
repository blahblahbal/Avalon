using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

internal class VirulentScythe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 36;
        Item.UseSound = SoundID.Item1;
        Item.damage = 50;
        Item.autoReuse = true;
        Item.shootSpeed = 15f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Yellow;
        Item.useTime = 18;
        Item.knockBack = 2.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.VirulentScythe>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noUseGraphic = true;
        Item.value = Item.sellPrice(0, 20);
        Item.useAnimation = 18;
        Item.UseSound = SoundID.Item1;
    }
}
