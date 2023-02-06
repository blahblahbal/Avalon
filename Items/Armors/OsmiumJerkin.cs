using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ExxoAvalonOrigins.Items.Material.Bars;

namespace ExxoAvalonOrigins.Items.Armors
{
    [AutoloadEquip(EquipType.Body)]
    public class OsmiumJerkin : ModItem
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
            player.GetDamage(DamageClass.Melee) += 0.12f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ModContent.ItemType<OsmiumBar>(), 20)
                .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 6)
                .AddTile(TileID.Anvils).Register();
        }
    }
}
