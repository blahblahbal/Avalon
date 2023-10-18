using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
class AncientLeggings : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 25;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 30, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.SolarFlareLeggings)
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();
        
        CreateRecipe()
            .AddIngredient(ItemID.NebulaLeggings)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();
        
        CreateRecipe()
            .AddIngredient(ItemID.StardustLeggings)
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();
        
        CreateRecipe()
            .AddIngredient(ItemID.VortexLeggings)
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddIngredient(ModContent.ItemType<Material.GhostintheMachine>())
            .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
            .Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.maxMinions += 6;
        player.statManaMax2 += 100;
    }
}
