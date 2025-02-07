using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using Avalon.Data.Sets;
using Terraria.ID;
using Avalon.Tiles;
using ReLogic.Content;
using Avalon.Common.Players;
using Terraria.DataStructures;

namespace Avalon.Items.Placeable.Furniture
{
	public class MirageChest : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.DeadMansChest;
			ItemID.Sets.ShimmerTransformToItem[ItemID.DeadMansChest] = Type;
		}

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.maxStack = Terraria.Item.CommonMaxStack;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AngelChest>();
			Item.width = 26;
			Item.height = 22;
			Item.value = 500;
		}

		public override void HoldItem(Player player)
		{
			if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
			{
				player.cursorItemIconEnabled = true;
				player.cursorItemIconID = ItemID.GoldChest;
			}
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			Main.instance.LoadItem(ItemID.GoldChest);
			Texture2D texture = TextureAssets.Item[ItemID.GoldChest].Value;
			Rectangle frame = texture.Frame();
			Vector2 vector = frame.Size() / 2f;
			Vector2 vector2 = new((float)(Item.width / 2) - vector.X, (float)(Item.height - frame.Height));
			Vector2 vector3 = Item.position - Main.screenPosition + vector + vector2;
			spriteBatch.Draw(texture, vector3, (Rectangle?)frame, alphaColor, rotation, vector, scale, (SpriteEffects)0, 0f);
			
			Vector2 vector4 = new Vector2(-4f, -4f) * scale;
			Texture2D value = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/AngelIcon", AssetRequestMode.ImmediateLoad).Value;
			Rectangle rectangle = value.Frame();
			spriteBatch.Draw(value, vector3 + vector4 + frame.Size().RotatedBy(rotation) * 0.45f * Item.scale, (Rectangle?)rectangle, alphaColor, rotation, rectangle.Size() / 2f, 1f, (SpriteEffects)0, 0f);
			return false;
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Main.instance.LoadItem(ItemID.GoldChest);
			Texture2D value = TextureAssets.Item[ItemID.GoldChest].Value;
			SpriteEffects effects = (SpriteEffects)0;
			Texture2D value2 = TextureAssets.InventoryBack.Value;
			Vector2 position2 = position - ((value2.Size() * scale) / 2f);

			spriteBatch.Draw(value, position, (Rectangle?)frame, drawColor, 0f, origin, scale, effects, 0f);

			Vector2 vector2 = new Vector2(-4f, -4f) * scale;
			Texture2D value8 = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/AngelIcon", AssetRequestMode.ImmediateLoad).Value;
			Rectangle rectangle2 = value8.Frame();
			spriteBatch.Draw(value8, position2 + vector2 + new Vector2(40f, 40f) * scale, (Rectangle?)rectangle2, drawColor, 0f, rectangle2.Size() / 2f, 1f, (SpriteEffects)0, 0f);
			return false;
		}
	}
}
