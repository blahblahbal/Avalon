using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode
{
    [AutoloadEquip(EquipType.Body)]
    public class ResistantWoodBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.Size = new Vector2(16);
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<ResistantWood>(), 30).AddTile(TileID.WorkBenches).Register();
        }
    }
}
