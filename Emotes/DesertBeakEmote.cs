using Avalon.Systems;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace Avalon.Emotes
{
    public class DesertBeakEmote : ModEmoteBubble
    {
        public override void SetStaticDefaults()
        {
            // The default emote command name will be a lowercase version of the classname to match other vanilla commands.
            // This can be changed in the localization files.

            // Add the emote to "bosses" category
            AddToCategory(EmoteID.Category.Dangers);
        }

        public override bool IsUnlocked()
        {
            // This emote only shows when minion boss is downed, just as vanilla do.
            return ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak;
        }
    }
}
