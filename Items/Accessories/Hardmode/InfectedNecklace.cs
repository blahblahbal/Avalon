using Avalon.Common.Players;
using Avalon.Items.Accessories.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Avalon.Items.Accessories.Hardmode
{
    [AutoloadEquip(EquipType.Neck)]
    public class InfectedNecklace : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.Size = new Vector2(24);
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 10);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AvalonPlayer>().MutatedStocking = true;
            player.slotsMinions += 1;
        }
        public override void AddRecipes()
        {
            Recipe.Create(1)
                .AddIngredient(ModContent.ItemType<MutatedStocking>())
                .AddIngredient(ItemID.PygmyNecklace)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
