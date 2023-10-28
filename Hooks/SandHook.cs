using Terraria;
using Terraria.ModLoader;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.DarkMatter;
using Avalon.Projectiles;
using Avalon.Common;
using Avalon.Items.Placeable.Tile;

namespace Avalon.Hooks
{
    internal class SandHook : ModHook
    {
        protected override void Apply()
        {
            On_WorldGen.GetSandfallProjData += On_WorldGen_GetSandfallProjData;
            On_Player.PickAmmo_Item_refInt32_refSingle_refBoolean_refInt32_refSingle_refInt32_bool += On_Player_PickAmmo_Item_refInt32_refSingle_refBoolean_refInt32_refSingle_refInt32_bool;
        }

        private void On_Player_PickAmmo_Item_refInt32_refSingle_refBoolean_refInt32_refSingle_refInt32_bool(On_Player.orig_PickAmmo_Item_refInt32_refSingle_refBoolean_refInt32_refSingle_refInt32_bool orig, Player self, Item sItem, ref int projToShoot, ref float speed, ref bool canShoot, ref int totalDamage, ref float KnockBack, out int usedAmmoItemId, bool dontConsume)
        {
            orig.Invoke(self, sItem, ref projToShoot, ref speed, ref canShoot, ref totalDamage, ref KnockBack, out usedAmmoItemId, dontConsume);
            if (projToShoot == 42)
            {
                Item item = self.ChooseAmmo(sItem);
                if (item.type == ModContent.ItemType<SnotsandBlock>())
                {
                    projToShoot = ModContent.ProjectileType<SnotsandSandgunProjectile>();
                    totalDamage += 5;
                }
            }
        }

        private void On_WorldGen_GetSandfallProjData(On_WorldGen.orig_GetSandfallProjData orig, int type, out int projType, out int dmg)
        {
            orig.Invoke(type, out projType, out dmg);
            if (type == ModContent.TileType<Snotsand>())
            {
                projType = ModContent.ProjectileType<SnotsandBall>();
            }
        }
    }
}
