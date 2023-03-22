using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.UI;

namespace Avalon.UI;

public class ExxoUITooltipState : ExxoUIState
{
    public ExxoUITextPanel? TooltipText { get; private set; }
    protected override bool DisableRecipeScrolling => false;
    protected override bool FocusInteractionsToUI => false;
    protected override bool HideItemHoverIcon => false;

    public override void OnInitialize()
    {
        base.OnInitialize();
        Width = StyleDimension.Fill;
        Height = StyleDimension.Fill;
        TooltipText =
            new ExxoUITextPanel(LocalizedText.Empty) { BackgroundColor = new Color(23, 25, 81, 255) * 0.925f * 0.85f };
        Append(TooltipText);
    }

    /// <inheritdoc />
    public override void Update(GameTime gameTime)
    {
        if (TooltipText != null)
        {
            Vector2 position = UserInterface.ActiveInstance.MousePosition + new Vector2(6f);
            TooltipText.Left.Pixels = position.X + TooltipText.PaddingLeft;
            TooltipText.Top.Pixels = position.Y + TooltipText.PaddingTop;
        }

        base.Update(gameTime);
    }

    /// <inheritdoc />
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (string.IsNullOrEmpty(TooltipText?.TextElement.Text))
        {
            return;
        }

        base.Draw(spriteBatch);
        TooltipText?.TextElement.SetText(string.Empty);
    }
}
