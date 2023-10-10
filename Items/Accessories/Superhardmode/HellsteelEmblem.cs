using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

class HellsteelEmblem : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 9);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.15f;
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.3f);
        player.GetModPlayer<AvalonPlayer>().TrapImmune = true;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ModContent.ItemType<ChaosEmblem>())
    //        .AddIngredient(ModContent.ItemType<GuardianBoots>())
    //        .AddIngredient(ModContent.ItemType<Material.HellsteelPlate>(), 20)
    //        .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
    //        .Register();
    //}
}
