using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items
{
    public class ResearchEverything : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().Register();
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IronAxe);
        }
        public override bool? UseItem(Player player)
        {
            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                CreativeUI.ResearchItem(i);
            }
                return base.UseItem(player);
        }
    }
}
