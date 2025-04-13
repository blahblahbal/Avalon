using Avalon.Common.Players;
using Avalon.Items.Accessories.Hardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

class ChaoticDestroyerEye : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = 44;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 9, 75);
        Item.height = 44;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.2f);
        player.GetDamage(DamageClass.Generic) += 0.1f;
        player.GetModPlayer<AvalonPlayer>().ChaosCharm = true;
        player.GetCritChance(DamageClass.Generic) += 8;
        player.GetModPlayer<AvalonPlayer>().TrapImmune = true;
        player.GetModPlayer<AvalonPlayer>().AllMaxCrit(3);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<ChaosEye>())
            .AddIngredient(ModContent.ItemType<HellsteelEmblem>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
