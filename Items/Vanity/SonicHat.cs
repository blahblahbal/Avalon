using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    class SonicHat : ModItem
    {
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 1, 20);
            Item.rare = ItemRarityID.Orange;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.MushroomGrassSeeds, 5)
                .AddIngredient(ItemID.Silk, 20)
                .AddTile(TileID.Loom)
                .Register();
        }
    }
}
