using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace ExxoAvalonOrigins.UI;

public class GenericSelectionMenu : UIState
{
    private readonly UIList optionList;
    private readonly UITextPanel<LocalizedText> backPanel;
    private readonly UIElement uIElement;

    private bool skipDraw;

    public GenericSelectionMenu(string title, UIList list, MouseEvent cancelAction)
    {
        uIElement = new UIElement();
        uIElement.Width.Set(0f, 0.8f);
        uIElement.MaxWidth.Set(400f, 0f);
        uIElement.Top.Set(220f, 0f);
        uIElement.Height.Set(-(220), 1f);
        uIElement.HAlign = 0.5f;

        UIPanel uIPanel = new UIPanel();
        uIPanel.Width.Set(0f, 1f);
        uIPanel.Height.Set(-110f, 1f);
        uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
        uIElement.Append(uIPanel);

        optionList = list;
        optionList.Width.Set(-25f, 1f);
        optionList.Height.Set(0f, 1f);
        optionList.ListPadding = 5f;
        uIPanel.Append(optionList);

        UIScrollbar uIScrollbar = new UIScrollbar();
        uIScrollbar.SetView(100f, 1000f);
        uIScrollbar.Height.Set(0f, 1f);
        uIScrollbar.HAlign = 1f;
        uIPanel.Append(uIScrollbar);
        optionList.SetScrollbar(uIScrollbar);

        UITextPanel<string> uITextPanel = new UITextPanel<string>(title, 0.8f, large: true)
        {
            BackgroundColor = new Color(73, 94, 171),
            HAlign = 0.5f
        };
        uITextPanel.Top.Set(-35f, 0f);
        uITextPanel.SetPadding(15f);
        uIElement.Append(uITextPanel);

        backPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, large: true);
        backPanel.Width.Set(-10f, 1f);
        backPanel.Height.Set(50f, 0f);
        backPanel.VAlign = 1f;
        backPanel.Top.Set(-45f, 0f);
        backPanel.OnMouseOver += FadedMouseOver;
        backPanel.OnMouseOut += FadedMouseOut;
        backPanel.OnLeftClick += BackAction;
        backPanel.OnLeftClick += cancelAction;
        uIElement.Append(backPanel);

        base.Append(uIElement);
    }

    public GenericSelectionMenu(string title, UIList list, MouseEvent cancelAction, MouseEvent submitAction) : this(title, list, cancelAction)
    {
        backPanel.Width.Set(-10f, 0.5f);

        UITextPanel<LocalizedText> submitPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Submit"), 0.7f, large: true);
        submitPanel.CopyStyle(backPanel);
        submitPanel.HAlign = 1f;
        submitPanel.OnMouseOver += FadedMouseOver;
        submitPanel.OnMouseOut += FadedMouseOut;
        submitPanel.OnLeftClick += SubmitAction;
        submitPanel.OnLeftClick += submitAction;
        uIElement.Append(submitPanel);
    }
    private void SubmitAction(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(SoundID.MenuOpen);
    }

    private void BackAction(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(SoundID.MenuClose);
    }

    private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(SoundID.MenuTick);
        ((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
    }

    private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
        ((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
    }

    public override void OnActivate()
    {
        if (PlayerInput.UsingGamepadUI)
        {
            UILinkPointNavigator.ChangePoint(3000 + ((optionList.Count == 0) ? 1 : 2));
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (skipDraw)
        {
            skipDraw = false;
            return;
        }
        base.Draw(spriteBatch);
    }
}
