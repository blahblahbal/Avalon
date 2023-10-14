using Avalon.Items.Consumables;
using Avalon.Items.Placeable.Furniture;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Vanity;
using Avalon.NPCs.Bosses.PreHardmode;
using Avalon.Systems;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Avalon.Compatability.BossChecklist
{
    public class BossChecklistSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                return;
            }
            bossChecklist.Call(
                "LogBoss",
                Mod,
                nameof(BacteriumPrime),
                3f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime,
                ModContent.NPCType<BacteriumPrime>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<InfestedCarcass>(),
                    ["collectibles"] = new List<int> { ModContent.ItemType<BacteriumPrimeMask>(), ModContent.ItemType<BacteriumPrimeTrophy>(), ModContent.ItemType<BacteriumPrimeRelic>() }
                    // Other optional arguments as needed...
                }
            );

            bossChecklist.Call(
                "LogBoss",
                Mod,
                nameof(DesertBeak),
                6f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak,
                ModContent.NPCType<DesertBeak>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<TheBeak>(),
                    ["collectibles"] = new List<int> { ModContent.ItemType<DesertBeakMask>(), ModContent.ItemType<DesertBeakTrophy>() } //, ModContent.ItemType<BacteriumPrimeRelic>() }
                    // Other optional arguments as needed...
                }
            );
        }
    }
}
