using Avalon.Systems;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace Avalon.Emotes
{
    public class LibrarianEmote : ModEmoteBubble
    {
        public override void SetStaticDefaults()
        {
            AddToCategory(EmoteID.Category.Town);
        }
    }
}
