using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

[AutoloadEquip(EquipType.Neck)]
class ResourceBarSkin : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = 26;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 45);
        Item.height = 26;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }
}

class ResourceSkinOverlay : ModResourceOverlay
{
	public override void PostDrawResource(ResourceOverlayDrawContext context)
	{
		//if (context.position.X <= Main.screenWidth - 37)
		//{
		//	if (context.texture.Value == TextureAssets.Heart.Value || context.texture.Value == TextureAssets.Heart2.Value)
		//	{
		//		int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
		//		if (slot != -1)
		//		{
		//			if (Main.LocalPlayer.dye[slot].type != ItemID.None)
		//			{
		//				GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
		//				//context.texture = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/DyeableHeart");
		//			}
		//		}
		//	}

		//	if (context.texture.Value == Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/FancyClassic/Heart_Fill").Value || context.texture.Value == Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/FancyClassic/Heart_Fill_B").Value)
		//	{
		//		int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
		//		if (slot != -1)
		//		{
		//			if (Main.LocalPlayer.dye[slot].type != ItemID.None)
		//			{
		//				GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
		//				//context.texture = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Dyeable_HP_Fill");
		//			}
		//		}
		//	}

		//	if (context.texture.Value == Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/HorizontalBars/HP_Fill_Honey").Value || context.texture.Value == Main.Assets.Request<Texture2D>("Images/UI/PlayerResourceSets/HorizontalBars/HP_Fill").Value)
		//	{
		//		int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
		//		if (slot != -1)
		//		{
		//			if (Main.LocalPlayer.dye[slot].type != ItemID.None)
		//			{
		//				GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
		//				//context.texture = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/DyeableFancyHeart");
		//			}
		//		}
		//	}
		//}
	}
}
