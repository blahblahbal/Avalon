using Avalon.Common.Templates;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
    public class BrownTorch : ModTorch
    {
        public override Vector3 LightColor => new Vector3(1.1f, 0.8f, 0.4f);
        public override int TorchItem => ModContent.ItemType<Items.Placeable.Furniture.BrownTorch>();
        public override int dustType => ModContent.DustType<BrownTorchDust>();
		private static Asset<Texture2D>? flameTexture;
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			var randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)(ulong)i);
			var color = new Color(100, 100, 100, 0);
			int frameX = Main.tile[i, j].TileFrameX;
			int frameY = Main.tile[i, j].TileFrameY;
			var width = 20;
			var offsetY = 0;
			var height = 20;
			if (WorldGen.SolidTile(i, j - 1))
			{
				offsetY = 2;
				if (WorldGen.SolidTile(i - 1, j + 1) || WorldGen.SolidTile(i + 1, j + 1))
				{
					offsetY = 4;
				}
			}
			var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			for (var k = 0; k < 7; k++)
			{
				var x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
				var y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
				Main.spriteBatch.Draw(flameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + x, j * 16 - (int)Main.screenPosition.Y + offsetY + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
		}
	}
}
