using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

public class SolarSystem : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return true;
    }
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = Item.useAnimation = 40;
        Item.channel = true;
        Item.damage = 200;
        Item.knockBack = 8;
        Item.scale = 1.1f;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Pink;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Melee;
        Item.value = 999000;
        Item.shootSpeed = 12f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.SolarSystem.Sun>();
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Melee.SolarSystem.Sun>()] == 0;
    }
    public override void HoldItemFrame(Player player)
    {
        if (player.channel)
        {
            player.bodyFrame.Y = player.bodyFrame.Height * 3;
        }
    }
}
