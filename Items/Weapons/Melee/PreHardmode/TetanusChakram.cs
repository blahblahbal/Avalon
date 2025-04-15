using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class TetanusChakram : ModItem
{
    int ShootTimes;
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 27;
        Item.noUseGraphic = true;
        Item.shootSpeed = 7.3f;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.knockBack = 2.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TetanusChakram>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 50, 0);
        Item.UseSound = SoundID.Item1;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 2;
    }
}
