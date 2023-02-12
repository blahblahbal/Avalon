using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.UI;

namespace ExxoAvalonOrigins.UI
{
    public class ListItem : UIPanel
    {

        private Texture2D dividerTexture;
        private Texture2D innerPanelTexture;
        private readonly UIImage worldIcon;
        private readonly UIText UIText;

        private readonly string itemName;

        public ListItem(string name, Texture2D tex, UIElement.MouseEvent submitEvent)
        {
            itemName = name;
            LoadTextures();

            var mouseOverBox = new UIElement();
            mouseOverBox.Width.Set(0f, 1f);
            mouseOverBox.Height.Set(0f, 1f);
            mouseOverBox.OnMouseOver += delegate
            {
                SoundEngine.PlaySound(SoundID.MenuTick);
            };
            Append(mouseOverBox);

            OnLeftClick += ClickAction;
            OnLeftClick += submitEvent;

            UIText = new UIText(itemName, 0.45f, true);
            Append(UIText);

            worldIcon = new UIImage(tex);
            worldIcon.Left.Set(4f, 0);
            worldIcon.VAlign = 0.5f;
            Append(worldIcon);

            Height.Set(worldIcon.Height.Pixels + 16f, 0f);
            Width.Set(0f, 1f);
            SetPadding(6f);
            BorderColor = new Color(89, 116, 213) * 0.7f;
            UIText.TextColor = Color.White * 0.9f;
        }

        private void LoadTextures()
        {
            dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider").Value;
            innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground").Value;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            InitializeAppearance();
        }

        private void InitializeAppearance()
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();
            CalculatedStyle dimensions = worldIcon.GetDimensions();
            UIText.Left.Set(dimensions.Width + 11f + 5f, 0);
            UIText.Top.Set((innerDimensions.Height * 0.25f) + innerPanelTexture.Height / 4, 0);
        }

        private void ClickAction(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuOpen);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

            BackgroundColor = new Color(73, 94, 171);
            BorderColor = new Color(89, 116, 213);
            UIText.TextColor = new Color(255, 215, 0);
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);
            BackgroundColor = new Color(63, 82, 151) * 0.7f;
            BorderColor = new Color(89, 116, 213) * 0.7f;
            UIText.TextColor = Color.White * 0.9f;
        }

        private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
        {
            spriteBatch.Draw(innerPanelTexture, position, new Rectangle(0, 0, 8, innerPanelTexture.Height), Color.White);
            spriteBatch.Draw(innerPanelTexture, new Vector2(position.X + 8f, position.Y), new Rectangle(8, 0, 8, innerPanelTexture.Height), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
            spriteBatch.Draw(innerPanelTexture, new Vector2(position.X + width - 8f, position.Y), new Rectangle(16, 0, 8, innerPanelTexture.Height), Color.White);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            CalculatedStyle innerDimensions = GetInnerDimensions();
            CalculatedStyle dimensions = worldIcon.GetDimensions();
            float num = dimensions.X + dimensions.Width;
            var vector = new Vector2(num + 6f, innerDimensions.Y + (innerDimensions.Height * 0.25f));
            DrawPanel(spriteBatch, vector, innerDimensions.Width - dimensions.Width - 12f);
            spriteBatch.Draw(dividerTexture, new Vector2(num, vector.Y + innerPanelTexture.Height + 5), null, Color.White, 0f, Vector2.Zero, new Vector2((GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), SpriteEffects.None, 0f);
        }
    }
}
