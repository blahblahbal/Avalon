using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUINumberInput : ExxoUIList
{
    private int activeNumberIndex;
    private bool flickerToggle;
    private double lastActiveFlicker;
    private int maxNumber = int.MaxValue;
    private int number;
    private ExxoUITextPanel[] numbers = Array.Empty<ExxoUITextPanel>();

    public ExxoUINumberInput(int amountNumbers = 3)
    {
        Direction = Direction.Horizontal;
        FitWidthToContent = true;
        FitHeightToContent = true;
        ListPadding = 0;

        Resize(amountNumbers);
    }

    public event EventHandler<KeyboardUpdateArgs>? OnKeyboardUpdate;
    public event EventHandler<EventArgs>? OnNumberChanged;

    public int MaxNumber
    {
        get => maxNumber;
        set
        {
            if (maxNumber == value)
            {
                return;
            }

            maxNumber = value;
            Resize(maxNumber.ToString(CultureInfo.InvariantCulture).Length);
        }
    }

    public int AmountNumbers { get; private set; }

    public int Number
    {
        get => number;
        set
        {
            string oldString = number.ToString(CultureInfo.InvariantCulture);
            number = (int)MathHelper.Clamp(value, 0, MaxNumber);
            SetActiveIndex(activeNumberIndex + number.ToString(CultureInfo.InvariantCulture).Length - oldString.Length);
            OnNumberChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private ExxoUITextPanel ActiveNumberElement => numbers[activeNumberIndex];

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);

        PlayerInput.WritingText = true;
        Main.instance.HandleIME();
        string currentString = Number.ToString(CultureInfo.InvariantCulture);
        string newString = Main.GetInputText(currentString);

        if (int.TryParse(newString, out int newNumber))
        {
            if (Number != newNumber && newNumber.ToString(CultureInfo.InvariantCulture).Length <= AmountNumbers)
            {
                Number = newNumber;
            }
        }
        else if (newString?.Length == 0)
        {
            Number = 0;
        }

        OnKeyboardUpdate?.Invoke(this, new KeyboardUpdateArgs(Main.inputText));

        string numString = number.ToString(CultureInfo.InvariantCulture);

        for (int i = 0; i < AmountNumbers; i++)
        {
            numbers[i].TextElement.SetText(i < numString.Length ? numString[i].ToString() : "");
        }

        if (gameTime.TotalGameTime.TotalSeconds - lastActiveFlicker > 0.5f)
        {
            if (flickerToggle)
            {
                ActiveNumberElement.BackgroundColor = ExxoUIPanel.DefaultBackgroundColor * 2.5f;
            }
            else
            {
                ActiveNumberElement.BackgroundColor = ExxoUIPanel.DefaultBackgroundColor;
            }

            flickerToggle = !flickerToggle;
            lastActiveFlicker = gameTime.TotalGameTime.TotalSeconds;
        }
    }

    private void Resize(int amountNumbers = 3)
    {
        Clear();
        AmountNumbers = Math.Max(1, amountNumbers);
        Number = 1;

        // Match the text params below, ensures size consistency
        var textSize = new ExxoUIText("000");

        numbers = new ExxoUITextPanel[amountNumbers];

        for (int i = 0; i < amountNumbers; i++)
        {
            var num = new ExxoUITextPanel("");
            num.TextElement.OnInternalTextChange += (_, _) =>
            {
                num.MinWidth = textSize.MinWidth;
                num.MinHeight.Pixels = textSize.MinHeight.Pixels * 2;
            };
            num.TextElement.VAlign = UIAlign.Center;
            num.TextElement.HAlign = UIAlign.Center;
            Append(num);
            numbers[i] = num;
        }

        SetActiveIndex(0);
    }

    private void SetActiveIndex(int index)
    {
        if (activeNumberIndex != index && index >= 0 && index < AmountNumbers)
        {
            Color oldColor = ActiveNumberElement.BackgroundColor;
            ActiveNumberElement.BackgroundColor = ExxoUIPanel.DefaultBackgroundColor;
            activeNumberIndex = index;
            ActiveNumberElement.BackgroundColor = oldColor;
        }
    }

    public class KeyboardUpdateArgs : EventArgs
    {
        public KeyboardUpdateArgs(KeyboardState keyboardState) => KeyboardState = keyboardState;
        public KeyboardState KeyboardState { get; }
    }
}
