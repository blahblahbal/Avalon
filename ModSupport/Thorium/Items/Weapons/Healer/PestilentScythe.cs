using Avalon.Items.Material;
using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;

namespace Avalon.ModSupport.Thorium.Items.Weapons.Healer
{
    [ExtendsFromMod("ThoriumMod")]
    public class PestilentScythe : ScytheItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ExxoAvalonOrigins.ThoriumContentEnabled;
        }
        public override void SetStaticDefaults()
        {
            SetStaticDefaultsToScythe();
        }
        public override void SetDefaults()
        {
            SetDefaultsToScythe();
            scytheSoulCharge = 1;
            Item.damage = 13;
            Item.rare = ItemRarityID.Blue;
            Item.Size = new Vector2(24);
            Item.shoot = ModContent.ProjectileType<Projectiles.Healer.PestilentScythe>();
            Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ModContent.ItemType<BacciliteBar>(), 8).AddTile(TileID.Anvils).Register();
        }
    }
}
