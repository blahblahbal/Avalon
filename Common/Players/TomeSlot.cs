using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Common.Players;
public class TomeSlot : ModAccessorySlot
{
	public override bool IsEnabled() => true;
	public override bool IsHidden() => Main.EquipPage != 0;
	public override bool DrawDyeSlot => false;
	public override bool DrawVanitySlot => false;
	public override string FunctionalTexture => "Avalon/Assets/Textures/UI/TomeSlot";
	public override bool CanAcceptItem(Item checkItem, AccessorySlotType context) => checkItem.GetGlobalItem<AvalonGlobalItemInstance>().Tome;
	public override Vector2? CustomLocation
	{
		get
		{
			int top = MainHelper.GetMm() + 174 + 47 - (ModLoader.HasMod("ThoriumMod") ? 48 * 3 - 2 : 0);
			Rectangle r = new(0, 0, (int)(TextureAssets.InventoryBack.Width() * Main.inventoryScale), (int)(TextureAssets.InventoryBack.Height() * Main.inventoryScale));
			r.Y = top + 5 * 47 + 7;

			Vector2 p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47 - 47, r.Top);
			if (Main.mapStyle is 0 or 2)
			{
				p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47 - 47, r.Top /*362 + 54*/);
			}
			return p;
		}
	}
	public override bool PreDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
	{
		int top = MainHelper.GetMm() + 174 + 47 - (ModLoader.HasMod("ThoriumMod") ? 48 * 3 - 2 : 0);
		Rectangle r = new(0, 0, (int)(TextureAssets.InventoryBack.Width() * Main.inventoryScale), (int)(TextureAssets.InventoryBack.Height() * Main.inventoryScale));
		r.Y = top + 5 * 47 + 7;
		Vector2 p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47 - 47, r.Top);
		if (Main.mapStyle is 0 or 2)
		{
			p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47 - 47, r.Top /*362 + 54*/);
		}

		int cX = TextureAssets.InventoryBack3.Value.Width / 2;
		int cY = TextureAssets.InventoryBack3.Value.Height / 2;
		int endX = cX;
		int endY = cY;

		if (item.type != ItemID.None)
		{
			cX -= TextureAssets.Item[item.type].Value.Width / 2;
			cY -= TextureAssets.Item[item.type].Value.Height / 2;
			if (TextureAssets.Item[item.type].Value.Width >= TextureAssets.Item[item.type].Value.Height)
			{
				endX = TextureAssets.Item[item.type].Value.Width - (cY - cY / 4);
				endY = TextureAssets.Item[item.type].Value.Height - (cY - cY / 4);
			}
			else
			{
				endX = (int)(TextureAssets.Item[item.type].Value.Width / 1.05f) - (cX - cX / 4);
				endY = (int)(TextureAssets.Item[item.type].Value.Height / 1.05f) - (cX - cX / 4);
			}
		}

		Main.spriteBatch.Draw(TextureAssets.InventoryBack11.Value, new Rectangle((int)p.X, (int)p.Y, (int)(52 * Main.inventoryScale), (int)(52 * Main.inventoryScale)), new Color(212, 212, 212, 212));
		if (item.type == ItemID.None)
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>(FunctionalTexture).Value, new Rectangle((int)p.X, (int)p.Y, (int)(52 * Main.inventoryScale), (int)(52 * Main.inventoryScale)), new Color(90, 90, 90, 90));
		else
			Main.spriteBatch.Draw(TextureAssets.Item[item.type].Value, new Rectangle((int)p.X + cX, (int)p.Y + cY, endX, endY), Color.White);
		//Main.spriteBatch.Draw(TextureAssets.Item[item.type].Value, p + new Vector2(thing), null, Color.White, 0f, default, Main.inventoryScale * scaleMod, SpriteEffects.None, 1f);
		return false;
	}
	public override void OnMouseHover(AccessorySlotType context)
	{
		switch (context)
		{
			default:
				Main.hoverItemName = Language.GetTextValue("Mods.Avalon.Slots.Tome");
				break;
		}
	}
}
