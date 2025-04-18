using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class DevilsScythe : ModItem
{
    public override void SetDefaults()
    {
        Item.UseSound = SoundID.Item8;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 150;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 0.2f;
        Item.mana = 16;
        Item.rare = ItemRarityID.Purple;
        Item.noMelee = true;
        Item.width = 26;
        Item.height = 28;
        Item.useTime = 30;
        Item.knockBack = 4.75f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.DevilScythe>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 40000;
        Item.useAnimation = 30;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(6, 2);
    }
}
