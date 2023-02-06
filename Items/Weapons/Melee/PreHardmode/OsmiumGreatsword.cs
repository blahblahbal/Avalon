using ExxoAvalonOrigins.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Weapons.Melee.PreHardmode
{
    public class OsmiumGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 38;
            Item.damage = 28;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.scale = 1.5f;
            Item.crit += 5;
            Item.rare = ItemRarityID.Orange;
            Item.useTime = 20;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 50000;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<OsmiumBar>(), 14).AddIngredient(ModContent.ItemType<DesertFeather>(), 3).AddTile(TileID.Anvils).Register();
        }
    }
}
