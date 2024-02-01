using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Avalon.Items.Material;

namespace Avalon.Items.Weapons.Magic.Hardmode;

class Sunstorm : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 50;
        Item.autoReuse = true;
        Item.shootSpeed = 12f;
        Item.mana = 17;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 60;
        Item.knockBack = 3f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Sunstorm>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 5);
        Item.useAnimation = 60;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item8;
        Item.noMelee = true;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(10, 0);
    }
}
