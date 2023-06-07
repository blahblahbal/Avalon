using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode
{
    public class ResistantWoodSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AshWoodSword);
            Item.useAnimation += 3;
            Item.damage += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Placeable.Tile.ResistantWood>(), 7)
                .AddTile(TileID.WorkBenches).Register();
        }
    }
}
