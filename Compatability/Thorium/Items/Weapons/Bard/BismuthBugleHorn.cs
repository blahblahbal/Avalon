using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Projectiles.Bard;

namespace Avalon.Compatability.Thorium.Items.Weapons.Bard
{
    public class BismuthBugleHorn : GoldBugleHorn
    {
        public override void SetBardDefaults()
        {
            base.SetBardDefaults();
            Item.shoot = ModContent.ProjectileType<BismuthBugleHornPro>();
            Item.damage = 12;
            Item.useTime = Item.useAnimation = 25;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ModContent.ItemType<Avalon.Items.Material.Bars.BismuthBar>(), 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class BismuthBugleHornPro : GoldBugleHornPro
    {
        public override int DustType => 86;
    }
}