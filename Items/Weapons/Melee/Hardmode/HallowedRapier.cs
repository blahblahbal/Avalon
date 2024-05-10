using Avalon.Common.Templates;
using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using System.Threading.Channels;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Empowerments;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class HallowedRapier: ModItem
{
    //unused item, if someone wants to deletes this go ahead
    public float scaleMult = 1.35f; // set this to same as in the projectile file
    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 38;
        Item.rare = 8;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.noMelee = true;
        Item.damage = 80;
        Item.crit = 10;
        Item.knockBack = 4f;
        Item.autoReuse = false;
        Item.shoot = 927;
        Item.shootSpeed = 15f;
        Item.value = 10080;
        Item.useStyle = 13;
        Item.useAnimation = 18;
        Item.useTime = 6;
        Item.DamageType = DamageClass.Melee;
        Item.shoot = ModContent.ProjectileType<HallowedRapierProj>();
    }

    public override bool MeleePrefix()
    {
        return true;
    }
}
