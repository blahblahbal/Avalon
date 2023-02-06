using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ExxoAvalonOrigins.Items.Material.Bars;

namespace ExxoAvalonOrigins.Items.Armors
{
    [AutoloadEquip(EquipType.Legs)]
    public class OsmiumTreads : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.defense = 8;
            Item.rare = ItemRarityID.Orange;
            Item.Size = new Vector2(32);
            Item.value = Item.sellPrice(0, 1, 20, 0);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ModContent.ItemType<OsmiumBar>(), 17)
                .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 5)
                .AddTile(TileID.Anvils).Register();
        }
    }
}
