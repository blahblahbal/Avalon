using ExxoAvalonOrigins.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ExxoAvalonOrigins.Items.Weapons.Ranged.PreHardmode
{
    public class OsmiumLongbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToBow(17, 9f, true);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 50);
            Item.knockBack = 1.4f;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<OsmiumBar>(), 13).AddIngredient(ModContent.ItemType<DesertFeather>(), 2).AddTile(TileID.Anvils).Register();
        }
    }
}
