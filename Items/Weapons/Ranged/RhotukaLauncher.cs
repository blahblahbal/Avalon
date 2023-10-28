using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

internal class RhotukaLauncher : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 18;
        Item.UseSound = SoundID.Item34;
        Item.damage = 30;
        Item.autoReuse = false;
        Item.useAmmo = ModContent.ItemType<Ammo.RhotukaSpinner>();
        Item.shootSpeed = 5f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.useTime = 20;
        Item.knockBack = 0.625f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.RhotukaSpinnerScrambler>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 1000000;
        Item.useAnimation = 20;
    }
    public override Vector2? HoldoutOffset() => new Vector2(-10, 0);
}
