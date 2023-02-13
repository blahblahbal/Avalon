using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Accessories.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Accessories.Hardmode 
{
    [AutoloadEquip(EquipType.Neck)]
    public class BloodyNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Bloody Whetstone");
            //Tooltip.SetDefault("Melee attacks inflict bleeding");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.Size = new Vector2(16);
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 6);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExxoEquipEffects>().BloodyWhetstone = true;
            player.GetArmorPenetration(DamageClass.Melee) += 15;
        }
        public override void AddRecipes()
        {
            Recipe.Create(1)
                .AddIngredient(ModContent.ItemType<BlackWhetstone>())
                .AddIngredient(ModContent.ItemType<BloodyWhetstone>())
                .AddIngredient(ItemID.SharkToothNecklace)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
