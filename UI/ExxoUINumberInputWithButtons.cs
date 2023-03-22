using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUINumberInputWithButtons : ExxoUIList
{
    public ExxoUINumberInputWithButtons(int amountNumbers = 3)
    {
        Direction = Direction.Horizontal;
        FitWidthToContent = true;
        FitHeightToContent = true;
        ContentVAlign = UIAlign.Center;

        NumberInput = new ExxoUINumberInput(amountNumbers);
        Append(NumberInput);

        var buttonColumn = new ExxoUIList
        {
            FitWidthToContent = true, FitHeightToContent = true, Justification = Justification.Center,
        };

        var incrementButton = new ExxoUIImageButton(
            Main.Assets.Request<Texture2D>("Images/UI/Minimap/Default/MinimapButton_ZoomIn"));
        incrementButton.OnLeftClick += delegate
        {
            NumberInput.Number++;
        };
        buttonColumn.Append(incrementButton);
        var decrementButton =
            new ExxoUIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Minimap/Default/MinimapButton_ZoomOut"));
        decrementButton.OnLeftClick += delegate
        {
            NumberInput.Number--;
        };
        buttonColumn.Append(decrementButton);
        Append(buttonColumn);
    }

    public ExxoUINumberInput NumberInput { get; }
}
