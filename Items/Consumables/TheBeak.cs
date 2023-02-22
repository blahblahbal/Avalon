using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ExxoAvalonOrigins.Items.Consumables;

class TheBeak : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        //Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.width = 32;
        Item.useTime = 40;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 40;
        Item.height = 28;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Beak>(), 6)
            .AddIngredient(ItemID.SandBlock, 30)
            .AddTile(TileID.DemonAltar)
            .Register();
    }
    public override bool CanUseItem(Player player)
    {
        return !NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.PreHardmode.DesertBeak>()) && player.ZoneDesert;
    }

    public override bool? UseItem(Player player)
    {
        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Bosses.PreHardmode.DesertBeak>());
        SoundEngine.PlaySound(SoundID.Roar, player.position);
        return true;
    }
}
