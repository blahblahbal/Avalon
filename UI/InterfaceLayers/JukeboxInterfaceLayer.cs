using System;
using System.Collections.Generic;
using System.Reflection;
using Avalon.Common.Players;
using Avalon.Common.Templates;
using Avalon.UI.Jukebox;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.InterfaceLayers;

public class JukeboxInterfaceLayer : ModInterfaceLayer
{
    private JukeboxUIState? jukeboxState;
    private UserInterface? jukeboxUserInterface;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        jukeboxState = new JukeboxUIState();
        jukeboxUserInterface = new UserInterface();
    }

    /// <inheritdoc />
    public override void Update()
    {
        if (Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().DisplayJukeboxInterface && Main.playerInventory &&
            jukeboxUserInterface?.CurrentState == null)
        {
            jukeboxUserInterface?.SetState(jukeboxState);
            typeof(UserInterface)
                .GetField("_clickDisabledTimeRemaining", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(jukeboxUserInterface, 0);
        }
        else if (!(Main.playerInventory &&
                   Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().DisplayJukeboxInterface) &&
                 jukeboxUserInterface?.CurrentState != null)
        {
            jukeboxUserInterface?.SetState(null);
        }

        jukeboxUserInterface?.Update(Main._drawInterfaceGameTime);
    }

    /// <inheritdoc />
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int inventoryIndex =
            layers.FindIndex(layer => layer.Name.Equals("Vanilla: Radial Hotbars", StringComparison.Ordinal));
        layers.Insert(inventoryIndex, GameInterfaceLayer);
    }

    /// <inheritdoc />
    protected override bool Draw()
    {
        jukeboxUserInterface?.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
        return true;
    }
}
