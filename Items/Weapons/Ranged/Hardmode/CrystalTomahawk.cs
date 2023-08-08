using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode
{
    public class CrystalTomahawk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 2000;
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.CrystalTomahawk>();
            Item.consumable = true;
            Item.shootSpeed = 17;
            Item.damage = 44;
            Item.maxStack = 9999;
            Item.knockBack = 5;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
        }
        public override void AddRecipes()
        {
            CreateRecipe(555).AddIngredient(ItemID.CrystalShard, 5).AddIngredient(ItemID.SoulofLight, 1).AddTile(TileID.MythrilAnvil).Register();
        }
    }
}
