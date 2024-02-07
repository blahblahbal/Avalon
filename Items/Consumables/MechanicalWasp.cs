using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Items.Consumables;

class MechanicalWasp : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 45;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.value = 0;
        Item.maxStack = 20;
        Item.useAnimation = 45;
        Item.height = dims.Height;
    }

    public override bool CanUseItem(Player player)
    {
        if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Hardmode.Mechasting>()) || Main.dayTime) return false;
        return true;
    }

    public override bool? UseItem(Player player)
    {
        SoundEngine.PlaySound(SoundID.Roar, player.position);
        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Bosses.Hardmode.Mechasting>());
        return true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Stinger, 6)
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .AddIngredient(ItemID.SoulofFlight, 9)
            .AddIngredient(ItemID.SoulofNight, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        //CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.MosquitoProboscis>(), 9).AddIngredient(ItemID.HallowedBar, 10).AddIngredient(ModContent.ItemType<Material.DragonScale>(), 2).AddIngredient(ItemID.SoulofFlight, 15).AddTile(ModContent.TileType<Tiles.HallowedAltar>()).Register();
    }
}
