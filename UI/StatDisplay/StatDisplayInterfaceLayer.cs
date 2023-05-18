using Avalon.Common.Players;
using Avalon.Common.Templates;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.StatDisplay;

public class StatDisplayInterfaceLayer : ModInterfaceLayer
{
    private StatDisplayUIState? statDisplayState;
    private UserInterface? statDisplayInterface;

    public override void Load()
    {
        base.Load();
        statDisplayState = new StatDisplayUIState();
        statDisplayInterface = new UserInterface();
    }
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int inventoryIndex =
            layers.FindIndex(layer => layer.Name.Equals("Vanilla: Radial Hotbars", StringComparison.Ordinal));
        layers.Insert(inventoryIndex, GameInterfaceLayer);
    }

    public override void Update()
    {
        if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().DisplayStats && Main.playerInventory &&
            statDisplayInterface?.CurrentState == null)
        {
            statDisplayInterface?.SetState(statDisplayState);
            typeof(UserInterface)
                .GetField("_clickDisabledTimeRemaining", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(statDisplayInterface, 0);
        }
        else if (!(Main.playerInventory &&
                   Main.LocalPlayer.GetModPlayer<AvalonPlayer>().DisplayStats) &&
                 statDisplayInterface?.CurrentState != null)
        {
            statDisplayInterface?.SetState(null);
        }

        statDisplayInterface?.Update(Main._drawInterfaceGameTime);
    }

    /// <inheritdoc />
    protected override bool Draw()
    {
        statDisplayInterface?.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
        return true;
    }
}
