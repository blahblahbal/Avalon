using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

class ChaosTome : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item20;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 24;
        Item.autoReuse = true;
        Item.shootSpeed = 8f;
        Item.mana = 8;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 25;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.ChaosBolt>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 18400;
        Item.useAnimation = 25;
        Item.height = dims.Height;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(10, 0);
    }
}
