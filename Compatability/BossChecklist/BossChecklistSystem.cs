using Avalon.Items.Consumables;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Placeable.Trophy.Relics;
using Avalon.Items.Vanity;
using Avalon.NPCs.Bosses.PreHardmode;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.Localization;
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

            var customPortraitBP = (SpriteBatch sb, Rectangle rect, Color color) => {
                Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/BossChecklist/BacteriumPrimeBossChecklist").Value;
                Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

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
                    ["spawnInfo"] = Language.GetTextValue("Mods.Avalon.BossChecklist.SpawnInfo.BacteriumPrime"),
                    ["collectibles"] = new List<int> { ModContent.ItemType<BacteriumPrimeMask>(), ModContent.ItemType<BacteriumPrimeTrophy>(), ModContent.ItemType<BacteriumPrimeRelic>() }
                    // Other optional arguments as needed...
                }
            );

            var customPortraitDB = (SpriteBatch sb, Rectangle rect, Color color) => {
                Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/BossChecklist/DesertBeakBossChecklist").Value;
                Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklist.Call(
                "LogBoss",
                Mod,
                nameof(DesertBeak),
                6f,
                () => ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak,
                ModContent.NPCType<DesertBeak>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<DesertHorn>(),
                    ["spawnInfo"] = Language.GetTextValue("Mods.Avalon.BossChecklist.SpawnInfo.DesertBeak"),
                    ["collectibles"] = new List<int> { ModContent.ItemType<DesertBeakMask>(), ModContent.ItemType<DesertBeakTrophy>(), ModContent.ItemType<DesertBeakRelic>() },
                    // Other optional arguments as needed...
                }
            );
        }
    }
}
