using Avalon.Common.Players;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class RiftGoggles : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 50000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().RiftGoggles = true;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ItemID.CursedFlame, 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ItemID.Ichor, 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Goggles)
            .AddIngredient(ModContent.ItemType<BloodshotLens>(), 2)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ModContent.ItemType<Pathogen>(), 15)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 20)
            .AddTile(TileID.DemonAltar)
            .Register();
        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ItemID.CursedFlame, 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ItemID.Ichor, 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
        //CreateRecipe(1).AddIngredient(ItemID.Goggles).AddIngredient(ModContent.ItemType<BloodshotLens>(), 2).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 10).AddIngredient(ModContent.ItemType<Pathogen>(), 15).AddIngredient(ItemID.SoulofNight, 10).AddIngredient(ModContent.ItemType<Sulphur>(), 20).AddTile(TileID.DemonAltar).Register();
    }
}
