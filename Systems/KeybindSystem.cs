using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;

namespace Avalon.Systems;

public class KeybindSystem : ModSystem
{
    public static ModKeybind ShadowHotkey { get; private set; }
    public static ModKeybind SprintHotkey { get; private set; }
    public static ModKeybind DashHotkey { get; private set; }
    public static ModKeybind QuintupleHotkey { get; private set; }
    public static ModKeybind BubbleBoostHotkey { get; private set; }
    public static ModKeybind ModeChangeHotkey { get; private set; }
    public static ModKeybind AstralHotkey { get; private set; }
    public static ModKeybind RocketJumpHotkey { get; private set; }
    public static ModKeybind QuickStaminaHotkey { get; private set; }
    public static ModKeybind FlightTimeRestoreHotkey { get; private set; }
    public static ModKeybind MinionGuidingHotkey { get; private set; }

    public override void Load()
    {
        ShadowHotkey = KeybindLoader.RegisterKeybind(Mod, "Shadow Teleport", Keys.V);
        SprintHotkey = KeybindLoader.RegisterKeybind(Mod, "Stamina Sprint", Keys.F);
        DashHotkey = KeybindLoader.RegisterKeybind(Mod, "Stamina Dash", Keys.K);
        QuintupleHotkey = KeybindLoader.RegisterKeybind(Mod, "Toggle Quintuple Jump", Keys.RightControl);
        MinionGuidingHotkey = KeybindLoader.RegisterKeybind(Mod, "Minion Guide", Keys.RightControl);
        BubbleBoostHotkey = KeybindLoader.RegisterKeybind(Mod, "Toggle Bubble Boost", Keys.U);
        ModeChangeHotkey = KeybindLoader.RegisterKeybind(Mod, "Mode Change", Keys.N);
        AstralHotkey = KeybindLoader.RegisterKeybind(Mod, "Activate Astral Projecting", Keys.OemPipe);
        RocketJumpHotkey = KeybindLoader.RegisterKeybind(Mod, "Stamina Rocket Jump", Keys.Z);
        QuickStaminaHotkey = KeybindLoader.RegisterKeybind(Mod, "Quick Stamina", Keys.X);
        FlightTimeRestoreHotkey = KeybindLoader.RegisterKeybind(Mod, "Stamina Flight Time Restore", Keys.G);
    }

    public override void Unload()
    {
        ShadowHotkey = null;
        SprintHotkey = null;
        DashHotkey = null;
        QuintupleHotkey = null;
        BubbleBoostHotkey = null;
        ModeChangeHotkey = null;
        AstralHotkey = null;
        RocketJumpHotkey = null;
        QuickStaminaHotkey = null;
        FlightTimeRestoreHotkey = null;
        MinionGuidingHotkey = null;
    }
}
