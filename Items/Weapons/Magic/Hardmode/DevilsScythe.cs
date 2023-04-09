using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

class DevilsScythe : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item8;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 61;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 1.2f;
        Item.mana = 16;
        Item.rare = ItemRarityID.Purple;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.knockBack = 4.75f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.DevilScythe>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 40000;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
}
