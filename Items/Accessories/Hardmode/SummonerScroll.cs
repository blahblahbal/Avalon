using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class SummonerScroll : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.width = 20;
        Item.value = Item.sellPrice(0, 2);
        Item.accessory = true;
        Item.height = 20;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.SummonerEmblem)
            .AddIngredient(ItemID.PapyrusScarab)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Summon) += 0.17f;
		player.GetKnockback(DamageClass.Summon) += 2;
        player.maxMinions++;
    }
}
