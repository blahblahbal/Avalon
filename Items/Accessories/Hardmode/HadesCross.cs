using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class HadesCross : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 42;
		Item.height = 46;
        Item.accessory = true;
        Item.value = Item.buyPrice(0, 9);
        Item.rare = ItemRarityID.Yellow;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().AccLavaMerman = true;
        if (hideVisual)
        {
            player.GetModPlayer<AvalonPlayer>().HideVarefolk = true;
        }
        player.lavaImmune = true;
        player.fireWalk = true;
        player.ignoreWater = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.LavaWaders)
            .AddIngredient(ItemID.Hellstone, 20)
            .AddIngredient(ItemID.LavaBucket)
            .AddIngredient(ItemID.SoulofFright, 6)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().HideVarefolk = false;
        player.GetModPlayer<AvalonPlayer>().ForceVarefolk = true;
    }
    public override bool IsVanitySet(int head, int body, int legs) => true;
}
