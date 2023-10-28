using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

class FlowerofTheJungle : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 25;
        Item.shootSpeed = 5f;
        Item.mana = 16;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.knockBack = 5f;
        Item.useTime = 16;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.JungleFire>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 60, 0);
        Item.useAnimation = 16;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
}
