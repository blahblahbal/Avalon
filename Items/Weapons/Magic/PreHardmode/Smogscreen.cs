using Avalon.Projectiles.Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode
{
    public class Smogscreen : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Vilethorn);
            Item.shoot = ModContent.ProjectileType<Olivegas>();
            Item.useAnimation = 40;
            Item.useTime = 10;
            Item.damage = 8;
            Item.consumeAmmoOnFirstShotOnly= true;
            Item.shootSpeed = 6.5f;
            Item.ArmorPenetration = 7;
        }
    }
}
