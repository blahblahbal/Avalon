using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Contagion
{
    public class ContagionCampfire : ModCampfire
    {
        public override Vector3 LightColor => new Vector3(0.8f, 1.4f, 0);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.ContagionCampfire>();
        public override int dustType => DustID.JungleTorch;
		private static Asset<Texture2D>? flameTexture;
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			var tile = Main.tile[i, j];
			if (tile.TileFrameY < 36)
			{
				Color color = new Color(255, 255, 255, 0);

				Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
				if (Main.drawToScreen)
				{
					zero = Vector2.Zero;
				}

				int width = 16;
				int offsetY = 0;
				int height = 16;
				short frameX = tile.TileFrameX;
				short frameY = tile.TileFrameY;
				int addFrX = 0;
				int addFrY = 0;

				TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY); // calculates the draw offsets
				TileLoader.SetAnimationFrame(Type, i, j, ref addFrX, ref addFrY); // calculates the animation offsets

				Rectangle drawRectangle = new Rectangle(tile.TileFrameX, tile.TileFrameY + addFrY, 16, 16);

				// The flame is manually drawn separate from the tile texture so that it can be drawn at full brightness.
				Main.spriteBatch.Draw(flameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + offsetY) + zero, drawRectangle, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
		}
	}
}
