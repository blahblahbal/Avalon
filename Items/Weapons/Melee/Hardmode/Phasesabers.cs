using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Avalon.Items.Weapons.Melee.PreHardmode;

namespace Avalon.Items.Weapons.Melee.Hardmode
{
    public class Phasesabers
    {
        public class BrownPhasesaber : ModItem
        {
            public override void SetDefaults()
            {
                Item.CloneDefaults(ItemID.RedPhasesaber);
            }
            public override Color? GetAlpha(Color lightColor)
            {
                return Color.White;
            }
            public override void AddRecipes()
            {
                CreateRecipe()
                    .AddIngredient(ModContent.ItemType<BrownPhaseblade>())
                    .AddIngredient(ItemID.CrystalShard, 25)
                    .AddTile(TileID.MythrilAnvil)
                    .Register();
            }
            public override void MeleeEffects(Player player, Rectangle hitbox)
            {
                Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0.5f, 0.3f, 0f);
            }
        }
        public class CyanPhasesaber : ModItem
        {
            public override void SetDefaults()
            {
                Item.CloneDefaults(ItemID.RedPhasesaber);
            }
            public override Color? GetAlpha(Color lightColor)
            {
                return Color.White;
            }
            public override void AddRecipes()
            {
                CreateRecipe()
                    .AddIngredient(ModContent.ItemType<CyanPhaseblade>())
                    .AddIngredient(ItemID.CrystalShard, 25)
                    .AddTile(TileID.MythrilAnvil)
                    .Register();
            }
            public override void MeleeEffects(Player player, Rectangle hitbox)
            {
                Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0f, 0.5f, 0.5f);
            }
        }
        public class LimePhasesaber : ModItem
        {
            public override void SetDefaults()
            {
                Item.CloneDefaults(ItemID.RedPhasesaber);
            }
            public override Color? GetAlpha(Color lightColor)
            {
                return Color.White;
            }
            public override void AddRecipes()
            {
                CreateRecipe()
                    .AddIngredient(ModContent.ItemType<LimePhaseblade>())
                    .AddIngredient(ItemID.CrystalShard, 25)
                    .AddTile(TileID.MythrilAnvil)
                    .Register();
            }
            public override void MeleeEffects(Player player, Rectangle hitbox)
            {
                Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0.1f, 0.5f, 0f);
            }
        }
    }
}
