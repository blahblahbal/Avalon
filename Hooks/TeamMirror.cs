using ExxoAvalonOrigins.Common;
using Terraria;
using Terraria.ModLoader;
using ExxoAvalonOrigins.Items.Tools;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
class TeamMirror : ModHook
{
    protected override void Apply()
    {
        On_Player.HasUnityPotion += OnHasUnityItem;
        On_Player.TakeUnityPotion += OnTakeUnityItem;
    }
    private static void OnTakeUnityItem(On_Player.orig_TakeUnityPotion orig, Player self)
    {
        if (self.HasItem(ModContent.ItemType<ExxoAvalonOrigins.Items.Tools.TeamMirror>())) return;

        orig(self);
    }

    private static bool OnHasUnityItem(On_Player.orig_HasUnityPotion orig, Player self)
    {
        if (self.HasItem(ModContent.ItemType<ExxoAvalonOrigins.Items.Tools.TeamMirror>())) return true;
        return orig(self);
    }
}
