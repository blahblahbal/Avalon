using ExxoAvalonOrigins.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items
{
    public class SpawnTool : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().Register();
        }
        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.DefaultToBow(17, 17);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, ModContent.NPCType<BloodshotEye>());
            return false;
        }
    }
}
