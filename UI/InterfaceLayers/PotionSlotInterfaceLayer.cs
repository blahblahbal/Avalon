using System;
using System.Collections.Generic;
using System.Reflection;
using Avalon.Common.Templates;
using Avalon.UI.PotionSlots;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.InterfaceLayers;

public class PotionSlotInterfaceLayer : ModInterfaceLayer
{
    private PotionSlotUIState? potionSlotState;
    private UserInterface? slotInterface;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        potionSlotState = new PotionSlotUIState();
        slotInterface = new UserInterface();
    }

    /// <inheritdoc />
    public override void Update()
    {
        if (false) //Main.playerInventory &&
            //slotInterface?.CurrentState == null)
        {
            slotInterface?.SetState(potionSlotState);
            typeof(UserInterface)
                .GetField("_clickDisabledTimeRemaining", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(slotInterface, 0);
        }
        //else if (!Main.playerInventory &&
        //         slotInterface?.CurrentState != null)
        //{
        //    slotInterface?.SetState(null);
        //}
        return;
        slotInterface?.Update(Main._drawInterfaceGameTime);
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
        slotInterface?.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
        return true;
    }
}
