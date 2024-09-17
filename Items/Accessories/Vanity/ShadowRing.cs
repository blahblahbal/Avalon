using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Items.Accessories.Vanity;

class ShadowRing : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.ShroomiteBar, 12)
            .AddIngredient(ItemID.Diamond, 4)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().ShadowRing = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().ShadowRing = true;
    }
}
public class ShadowRingHook : ModHook
{
    protected override void Apply()
    {
        On_PlayerDrawSet.BoringSetup_End += On_PlayerDrawSet_BoringSetup_End;
		On_PlayerDrawLayers.DrawPlayer_27_HeldItem += On_PlayerDrawLayers_DrawPlayer_27_HeldItem;
    }

	// DOES NOT WORK
	private void On_PlayerDrawLayers_DrawPlayer_27_HeldItem(On_PlayerDrawLayers.orig_DrawPlayer_27_HeldItem orig, ref PlayerDrawSet drawinfo)
	{
		orig.Invoke(ref drawinfo);
		if (drawinfo.drawPlayer.GetModPlayer<AvalonPlayer>().ShadowRing)
		{
			if (drawinfo.drawPlayer.shroomiteStealth || drawinfo.drawPlayer.setVortex)
			{
				drawinfo.itemColor = Color.White; // new Color((byte)((float)(int)drawinfo.itemColor.R), (byte)((float)(int)drawinfo.itemColor.G), (byte)((float)(int)drawinfo.itemColor.B), (byte)((float)(int)drawinfo.itemColor.A));
			}
		}
	}

	private void On_PlayerDrawSet_BoringSetup_End(On_PlayerDrawSet.orig_BoringSetup_End orig, ref PlayerDrawSet self)
    {
        if (self.drawPlayer.GetModPlayer<AvalonPlayer>().ShadowRing)
        {
            if (self.drawPlayer.shroomiteStealth || self.drawPlayer.setVortex)
            {
                self.colorArmorHead = ColorValue(self, Color.White);
                self.colorArmorBody = ColorValue(self, Color.White);
                self.colorArmorLegs = ColorValue(self, Color.White);
                self.colorEyeWhites = ColorValue(self, Color.White);
                self.colorEyes = ColorValue(self, self.drawPlayer.eyeColor);
                self.colorHair = ColorValue(self, self.drawPlayer.hairColor);
                self.colorHead = ColorValue(self, self.drawPlayer.skinColor);
                self.colorBodySkin = ColorValue(self, self.drawPlayer.skinColor);
                self.colorShirt = ColorValue(self, self.drawPlayer.shirtColor);
                self.colorUnderShirt = ColorValue(self, self.drawPlayer.underShirtColor);
                self.colorPants = ColorValue(self, self.drawPlayer.pantsColor);
                self.colorShoes = ColorValue(self, self.drawPlayer.shoeColor);
                self.colorLegs = ColorValue(self, self.drawPlayer.skinColor);
                self.colorMount = Color.White;
                self.headGlowColor = Color.Transparent;
                self.bodyGlowColor = Color.Transparent;
                self.armGlowColor = Color.Transparent;
                self.legsGlowColor = Color.Transparent;
            }
        }
        orig.Invoke(ref self);
    }
    private Color ColorValue(PlayerDrawSet pds, Color c)
    {
        return pds.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)(pds.Position.X + pds.drawPlayer.width * 0.5) / 16, (int)(pds.Position.Y + pds.drawPlayer.height * 0.75) / 16, c), pds.shadow);
    }
}
