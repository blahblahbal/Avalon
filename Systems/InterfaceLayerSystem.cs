using System.Collections.Generic;
using Avalon.Common.Templates;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Systems;

public class InterfaceLayerSystem : ModSystem
{
    /// <inheritdoc />
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        //int invIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Radial Hotbars");
        //if (invIndex != -1)
        //{
        //    layers.Insert(invIndex, new LegacyGameInterfaceLayer("Stat Display", delegate
        //    {
        //        ExxoAvalonOrigins.Mod.statDisplayInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
        //        return true;
        //    }, InterfaceScaleType.UI));
        //}

        int staminaBarIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Mouse Text");
        if (staminaBarIndex != -1)
        {
            layers.Insert(staminaBarIndex, new LegacyGameInterfaceLayer("Stamina Bar", delegate
            {
                ExxoAvalonOrigins.Mod.staminaInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                return true;
            }, InterfaceScaleType.UI));
        }

        //if (staminaBarIndex != -1)
        //{
        //    layers.Insert(staminaBarIndex, new LegacyGameInterfaceLayer("Minion Slot Counter", delegate
        //    {
        //        ExxoAvalonOrigins.Mod.minionSlotInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
        //        return true;
        //    }, InterfaceScaleType.UI));
        //}
        layers.Insert(0, new LegacyGameInterfaceLayer(
            $"{Mod.DisplayName}: Update Interfaces",
            delegate
            {
                foreach (ModInterfaceLayer modInterfaceLayer in ModInterfaceLayer.RegisteredInterfaceLayers)
                {
                    modInterfaceLayer.Update();
                }
                ExxoAvalonOrigins.Mod.staminaInterface.Update(Main._drawInterfaceGameTime);
                //ExxoAvalonOrigins.Mod.minionSlotInterface.Update(Main._drawInterfaceGameTime);
                return true;
            },
            InterfaceScaleType.UI)
        );

        foreach (ModInterfaceLayer modInterfaceLayer in ModInterfaceLayer.RegisteredInterfaceLayers)
        {
            modInterfaceLayer.ModifyInterfaceLayers(layers);
        }
    }
}
