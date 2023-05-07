using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode
{
    public class NapalmGun : ModItem
    {
        public override void SetDefaults() 
        {
            Item.DefaultToRangedWeapon(ModContent.ProjectileType<NapalmBall>(), AmmoID.Gel, 45, 8, true);
            Item.damage = 45;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item99;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -2);
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type).AddIngredient(ItemID.SlimeGun).AddIngredient(ItemID.HellstoneBar, 15).AddIngredient(ModContent.ItemType<FireShard>()).AddTile(TileID.Hellforge).Register();
        }
    }
}
