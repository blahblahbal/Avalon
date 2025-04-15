using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Superhardmode;

public class MagicCleaver : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 85;
        Item.autoReuse = true;
        Item.shootSpeed = 20;
        Item.mana = 16;
        Item.rare = ModContent.RarityType<BlueRarity>();
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = dims.Width;
        Item.useTime = 18;
        Item.knockBack = 5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.MagicCleaver>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 36000;
        Item.useAnimation = 18;
        Item.height = dims.Height;
    }
}
