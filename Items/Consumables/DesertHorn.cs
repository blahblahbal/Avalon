using Avalon.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class DesertHorn : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
    }
    public override void SetDefaults()
    {
        //Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.width = 32;
        Item.maxStack = 9999;
        Item.channel = true;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.height = 28;
        Item.useAnimation = Item.useTime = 180;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Beak>(), 6)
            .AddIngredient(ItemID.SandBlock, 30)
            .AddIngredient(ItemID.FossilOre, 10)
            .AddTile(TileID.Hellforge)
            .Register();
    }
    public override bool CanUseItem(Player player)
    {
        return !NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.PreHardmode.DesertBeak>()) && player.ZoneDesert && Main.dayTime;
    }

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().DesertBeakSpawnTimer = 60 * 3;
        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/DesertHorn"), player.position);
        return true;
    }
}
