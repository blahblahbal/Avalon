
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ExxoAvalonOrigins.Items.Material.Bars;

namespace ExxoAvalonOrigins.Items.Tools
{
    public class OsmiumPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 38;
            Item.damage = 13;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.crit += 6;
            Item.pick = 82;
            Item.rare = ItemRarityID.Orange;
            Item.useTime = 13;
            Item.knockBack = 3f;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 50000;
            Item.useAnimation = 13;
            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<OsmiumBar>(), 13).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2).AddTile(TileID.Anvils).Register();
        }
        public override void HoldItem(Player player)
        {
            if (player.inventory[player.selectedItem].type == Mod.Find<ModItem>("OsmiumPickaxe").Type)
            {
                player.pickSpeed -= 0.5f;
            }
        }
    }
}
