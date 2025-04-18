using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class Shurikerang : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 14;
        Item.noUseGraphic = true;
        Item.scale = 1.2f;
        Item.shootSpeed = 12f;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 20;
        Item.knockBack = 3f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Shurikerang>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 30000;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 3;
    }
}
