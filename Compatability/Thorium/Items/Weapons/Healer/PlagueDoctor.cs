using Avalon.Items.Material;
using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;

namespace Avalon.Compatability.Thorium.Items.Weapons.Healer
{
    [ExtendsFromMod("ThoriumMod")]
    public class PlagueDoctor : ScytheItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.HasMod("ThoriumMod");
        }
        public override void SetStaticDefaults()
        {
            SetStaticDefaultsToScythe();
        }
        public override void SetDefaults()
        {
            SetDefaultsToScythe();
            scytheSoulCharge = 1;
            Item.damage = 23;
            Item.rare = ItemRarityID.Orange;
            Item.Size = new Vector2(24);
            Item.shoot = ModContent.ProjectileType<Projectiles.Healer.PlagueDoctor>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PestilentScythe>())
                .AddIngredient(ModContent.ItemType<BoneReaper>())
                .AddIngredient(ModContent.ItemType<BountifulHarvest>())
                .AddIngredient(ModContent.ItemType<MoltenThresher>())
                .AddTile(TileID.Anvils).Register();
        }
    }
}
