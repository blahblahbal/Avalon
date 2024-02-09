using Avalon.NPCs.Bosses.Hardmode;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

internal class EctoplasmicBeacon : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTime = 45;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.value = 0;
        Item.maxStack = 20;
        Item.useAnimation = 45;
        Item.height = dims.Height;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
    }

    public override bool CanUseItem(Player player)
    {
        return !NPC.AnyNPCs(ModContent.NPCType<Phantasm>()) && player.InModBiome<Biomes.Hellcastle>() &&
               NPC.downedMoonlord && Main.hardMode;
    }

    public override bool? UseItem(Player player)
    {
        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Phantasm>());
        SoundEngine.PlaySound(SoundID.Roar, player.position);
        return true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Ectoplasm, 10)
            .AddIngredient(ItemID.LunarBar, 5)
            .AddIngredient(ItemID.BeetleHusk, 8) // might change later lol
            .AddTile(ModContent.TileType<LibraryAltar>())
            .Register();
    }
}
