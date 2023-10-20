using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode
{
    public class ResistantWoodHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AshWoodHammer);
            Item.useAnimation += 2;
            Item.useTime += 2;
            Item.damage += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Placeable.Tile.ResistantWood>(), 8)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
