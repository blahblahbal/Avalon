using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.NPCs.Bosses.PreHardmode;
using Avalon.Common.Players;
using Avalon.Items.Material;

namespace Avalon.Items.Consumables;

class InfestedCarcass : ModItem
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
        Item.CloneDefaults(ItemID.WormFood);
    }

    public override bool CanUseItem(Player player)
    {
        return !NPC.AnyNPCs(ModContent.NPCType<BacteriumPrime>()) &&
            (player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundContagion ||
            player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion);
    }

    public override bool? UseItem(Player player)
    {
        //NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<BacteriumPrime>());
        //SoundEngine.PlaySound(SoundID.Roar, player.position);
        //return true;
        if (player.whoAmI == Main.myPlayer) // Thanks Examplemod :)
        {
            // If the player using the item is the client
            // (explicitely excluded serverside here)
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            int type = ModContent.NPCType<BacteriumPrime>();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // If the player is not in multiplayer, spawn directly
                NPC.SpawnOnPlayer(player.whoAmI, type);
            }
            else
            {
                // If the player is in multiplayer, request a spawn
                // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in Mi- BacteriumPrime :)
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
            }
        }

        return true;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
			.AddIngredient(ModContent.ItemType<VirulentPowder>(), 30)
			.AddIngredient(ModContent.ItemType<YuckyBit>(), 15)
			.AddTile(TileID.DemonAltar).Register();
    }
}
