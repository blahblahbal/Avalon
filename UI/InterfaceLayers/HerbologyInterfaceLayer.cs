using System;
using System.Collections.Generic;
using System.Reflection;
using Avalon.Common.Players;
using Avalon.Common.Templates;
using Avalon.UI.Herbology;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.InterfaceLayers;

public class HerbologyInterfaceLayer : ModInterfaceLayer
{
    private HerbologyUIState? herbologyState;
    private UserInterface? herbologyUserInterface;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        herbologyState = new HerbologyUIState();
        herbologyUserInterface = new UserInterface();
    }

    /// <inheritdoc />
    public override void Update()
    {
        if (Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().DisplayHerbologyMenu && Main.playerInventory &&
            herbologyUserInterface?.CurrentState == null)
        {
            herbologyUserInterface?.SetState(herbologyState);
            typeof(UserInterface)
                .GetField("_clickDisabledTimeRemaining", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(herbologyUserInterface, 0);
        }
        else if (!(Main.playerInventory &&
                   Main.LocalPlayer.GetModPlayer<AvalonHerbologyPlayer>().DisplayHerbologyMenu) &&
                 herbologyUserInterface?.CurrentState != null)
        {
            herbologyUserInterface?.SetState(null);
        }

        herbologyUserInterface?.Update(Main._drawInterfaceGameTime);
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
        herbologyUserInterface?.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
        return true;
    }
}
