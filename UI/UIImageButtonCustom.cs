using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Avalon.UI; 

public class UIImageButtonCustom : UIImageButton
{
    private Asset<Texture2D> arrowDownTexture;
    private Asset<Texture2D> texture;
    private UITextCustom UIText;
    private List<UIImageButtonCustom> siblings;
    private string label;
    public bool Active;
    public bool FadeText;
    private bool firstDraw;
    public UIImageButtonCustom(Asset<Texture2D> texture, string label, MouseEvent clickAction, bool active = false) : base(texture)
    {
        arrowDownTexture = Main.Assets.Request<Texture2D>("Images/UI/TexturePackButtons");

        firstDraw = true;
        Active = active;
        if (Active)
        {
            clickAction.Invoke(null, null);
        }
        this.label = label;
        this.texture = texture;

        OnLeftClick += SetActive;
        OnLeftClick += clickAction;
    }

    public override void OnInitialize()
    {
        UIText = new UITextCustom(label);
        UIText.Hidden = true;
        Append(UIText);

        UIText.HAlign = 0.5f;
        UIText.Top.Set(-(UIText.Height.Pixels / 2 + 16), 0);

        siblings = GetSiblings();

        base.OnInitialize();
    }

    private List<UIImageButtonCustom> GetSiblings()
    {
        List<UIImageButtonCustom> siblingsList = new List<UIImageButtonCustom>();
        Type parentType = Parent.Parent.GetType();
        if (parentType == typeof(UIList) || parentType == typeof(UIListGrid))
        {
            UIList list = (UIList)Parent.Parent;
            foreach (UIElement element in list._items)
            {
                if (element.GetType() == typeof(UIImageButtonCustom))
                {
                    if (element != this)
                    {
                        siblingsList.Add((UIImageButtonCustom)element);
                    }
                }
            }
        }
        return siblingsList;
    }

    private void SetActive(UIMouseEvent evt, UIElement listeningElement)
    {
        if (Active)
        {
            return;
        }
        SoundEngine.PlaySound(SoundID.MenuOpen);
        Active = true;
        FadeText = false;
        SetSiblingsInactive();
    }

    public override void MouseOver(UIMouseEvent evt)
    {
        foreach (UIImageButtonCustom sibling in siblings)
        {
            sibling.FadeText = true;
        }
        base.MouseOver(evt);
    }

    public override void MouseOut(UIMouseEvent evt)
    {
        foreach (UIImageButtonCustom sibling in siblings)
        {
            sibling.FadeText = false;
        }
        base.MouseOut(evt);
    }

    private void SetSiblingsInactive()
    {
        foreach (UIImageButtonCustom sibling in siblings)
        {
            sibling.Active = false;
        }
    }

    public void DrawFromExternal(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsMouseHovering || Active)
        {
            base.Draw(spriteBatch);
        }

        if (Active)
        {
            foreach (UIImageButtonCustom sibling in siblings)
            {
                if (sibling.IsMouseHovering)
                {
                    sibling.DrawFromExternal(spriteBatch);
                }
            }
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (firstDraw)
        {
            // Shifts text to ensure it is within bounds
            CalculatedStyle textDims = UIText.GetOuterDimensions();
            if (textDims.X < Parent.GetOuterDimensions().X)
            {
                UIText.Left.Set(0, 0);
                UIText.HAlign = 0f;
            }
            else if (textDims.X + textDims.Width > Parent.GetOuterDimensions().X + Parent.GetOuterDimensions().Width)
            {
                UIText.Left.Set(0, 0);
                UIText.HAlign = 1f;
            }
            firstDraw = false;
        }

        float alpha;
        CalculatedStyle outerDimensions = GetOuterDimensions();
        UIText.TextColor = Color.White;

        if (IsMouseHovering || Active)
        {
            UIText.Hidden = false;

            if (Active)
            {
                UIText.TextColor = new Color(255, 215, 0) * (FadeText ? 0.7f : 1f);
            }

            alpha = 1;
        }
        else
        {
            UIText.Hidden = true;
            alpha = 0.4f;
        }

        spriteBatch.Draw(texture.Value, base.GetDimensions().Position(), Color.White * alpha);
        if (Active)
        {
            spriteBatch.Draw(arrowDownTexture.Value, new Vector2(outerDimensions.X + outerDimensions.Width / 2, outerDimensions.Y - 10), new Rectangle(32, 0, 32, 32), Color.White, 0f, new Vector2(16, 0), 1f, SpriteEffects.None, 0f);
        }
    }
}