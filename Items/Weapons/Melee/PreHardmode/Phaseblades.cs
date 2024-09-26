using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode
{
    public class BrownPhaseblade : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RedPhaseblade);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AddRecipes()
        {
            Recipe.Create(Type)
				.AddIngredient(ModContent.ItemType<Zircon>(), 10)
				.AddIngredient(ItemID.MeteoriteBar, 15)
				.AddTile(TileID.Anvils)
				.SortAfterFirstRecipesOf(ItemID.YellowPhaseblade)
				.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0.5f, 0.3f, 0f);
        }
    }
    public class CyanPhaseblade : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RedPhaseblade);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AddRecipes()
        {
            Recipe.Create(Type)
				.AddIngredient(ModContent.ItemType<Tourmaline>(), 10)
				.AddIngredient(ItemID.MeteoriteBar, 15)
				.AddTile(TileID.Anvils)
				.SortAfterFirstRecipesOf(ItemID.YellowPhaseblade)
				.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0f, 0.5f, 0.5f);
        }
    }
    public class LimePhaseblade : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RedPhaseblade);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AddRecipes()
        {
            Recipe.Create(Type)
				.AddIngredient(ModContent.ItemType<Peridot>(), 10)
				.AddIngredient(ItemID.MeteoriteBar, 15)
				.AddTile(TileID.Anvils)
				.SortAfterFirstRecipesOf(ItemID.YellowPhaseblade)
				.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0.1f, 0.5f, 0f);
        }
    }
}
