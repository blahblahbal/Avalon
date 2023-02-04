using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Materials
{
    public class LifeDew : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        { 
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = 400000;
            Item.Size = new Vector2(24);
        }
    }
}