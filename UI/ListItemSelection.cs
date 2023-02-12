using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.UI;

namespace ExxoAvalonOrigins.UI
{
	public class ListItemSelection : UIPanel
	{
		private Texture2D dividerTexture;
		private Texture2D innerPanelTexture;
		private UIListGrid optionList;
		private UIText UIText;
		private Color textColor;

		private string itemName;

		public ListItemSelection(string name, UIListGrid list)
		{
			itemName = name;
			LoadTextures();

			UIText = new UIText(itemName, 0.45f, true);
			Append(UIText);

			optionList = list;
			optionList.Width.Set(0, 1f);
			optionList.Height.Set(0f, 1f);
			optionList.Top.Set(0f, 0);
			optionList.MarginY = 55f;
			optionList.ListPadding = 5f;
			Append(optionList);
		}

		public override void OnInitialize()
		{
			InitializeAppearance();
			base.OnInitialize();
		}

		private void LoadTextures()
		{
			dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider").Value;
			innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground").Value;
		}

		private void InitializeAppearance()
		{
			Width.Set(0f, 1f);
			SetPadding(3f);
			Recalculate();

			Height.Set(optionList.GetTotalHeight() + PaddingTop + PaddingBottom, 0f);
			optionList.MarginX = ((GetInnerDimensions().Width - optionList.GetTotalWidth()) / 2f);
			optionList.Recalculate();
			optionList.Width.Set(optionList.GetTotalWidth() + optionList.MarginX, 0f);
			optionList.HAlign = 0.5f;

			UIText.Left.Set(5f, 0);
			UIText.Top.Set(innerPanelTexture.Height / 2, 0);

			BorderColor = new Color(89, 116, 213) * 0.7f;
			textColor = Color.White * 0.9f;
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
			textColor = Color.White;
		}

		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			BackgroundColor = new Color(63, 82, 151) * 0.7f;
			BorderColor = new Color(89, 116, 213) * 0.7f;
			textColor = Color.White * 0.9f;
		}

		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(innerPanelTexture, position, new Rectangle(0, 0, 8, innerPanelTexture.Height), Color.White);
			spriteBatch.Draw(innerPanelTexture, new Vector2(position.X + 8f, position.Y), new Rectangle(8, 0, 8, innerPanelTexture.Height), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(innerPanelTexture, new Vector2(position.X + width - 8f, position.Y), new Rectangle(16, 0, 8, innerPanelTexture.Height), Color.White);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			//Height.Set(optionList.GetTotalHeight() + 50f, 0f);
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = GetInnerDimensions();
			CalculatedStyle outerDimensions = GetOuterDimensions();
			float num = innerDimensions.X;
			Vector2 vector = new Vector2(num, innerDimensions.Y + 5f);
			this.DrawPanel(spriteBatch, vector, innerDimensions.Width);

			//Utils.DrawBorderStringBig(spriteBatch, itemName, new Vector2(num + 5f, innerDimensions.Y + innerPanelTexture.Height / 2), textColor, 0.45f);
			spriteBatch.Draw(this.dividerTexture, new Vector2(outerDimensions.X, innerDimensions.Y + 35f), null, Color.White, 0f, Vector2.Zero, new Vector2(outerDimensions.Width, 1f), SpriteEffects.None, 0f);
		}
	}
}
