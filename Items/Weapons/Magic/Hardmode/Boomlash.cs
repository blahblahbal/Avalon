using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class Boomlash : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item20;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 80;
        Item.channel = true;
        Item.shootSpeed = 4f;
        Item.mana = 40;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.knockBack = 12f;
        Item.useTime = 30;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Boomlash>();
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }
}
