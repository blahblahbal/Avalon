
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ExxoAvalonOrigins.Items.Materials;

namespace ExxoAvalonOrigins.Items.Tools
{
    public class OsmiumHamaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 38;
            Item.damage = 19;
            Item.autoReuse = true;
            Item.hammer = 70;
            Item.useTurn = true;
            Item.scale = 1.3f;
            Item.axe = 20;
            Item.crit += 5;
            Item.rare = ItemRarityID.Orange;
            Item.useTime = 20;
            Item.knockBack = 2.2f;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 50000;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<OsmiumBar>(), 12).AddIngredient(ModContent.ItemType<DesertFeather>(), 1).AddTile(TileID.Anvils).Register();
        }
    }
}
