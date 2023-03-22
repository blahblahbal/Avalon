using System.Collections.Generic;
using Avalon.Common.Templates;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace Avalon.UI.InterfaceLayers;

public class ExxoUIElementTooltipInterfaceLayer : ModInterfaceLayer
{
    private ExxoUITooltipState? mainState;
    private UserInterface? userInterface;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        mainState = new ExxoUITooltipState();
        userInterface = new UserInterface();
    }

    /// <inheritdoc />
    public override void Update()
    {
        userInterface?.SetState(mainState);
        userInterface?.Update(Main._drawInterfaceGameTime);
    }

    /// <inheritdoc />
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) => layers.Add(GameInterfaceLayer);

    public void SetText(string text) => mainState?.TooltipText?.TextElement.SetText(text);

    public void SetText(LocalizedText text) => mainState?.TooltipText?.TextElement.SetText(text);

    /// <inheritdoc />
    protected override bool Draw()
    {
        userInterface?.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
        return true;
    }
}
