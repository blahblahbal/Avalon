using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

class ShadowRing : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Shadow Ring");
        //Tooltip.SetDefault("Negates visual cloaking from stealth armors\nWorks in the vanity slot");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.height = dims.Height;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ItemID.ShroomiteBar, 5)
    //        .AddIngredient(ModContent.ItemType<Material.Ores.Onyx>(), 2)
    //        .AddTile(TileID.MythrilAnvil).Register();
    //}
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
        On_PlayerDrawSet.BoringSetup_2 += On_PlayerDrawSet_BoringSetup_2;
        On_PlayerDrawSet.BoringSetup_End += On_PlayerDrawSet_BoringSetup_End;
    }

    private void On_PlayerDrawSet_BoringSetup_End(On_PlayerDrawSet.orig_BoringSetup_End orig, ref PlayerDrawSet self)
    {
        if (self.drawPlayer.GetModPlayer<AvalonPlayer>().ShadowRing)
        {
            if (self.drawPlayer.shroomiteStealth || self.drawPlayer.setVortex)
            {
                self.colorArmorHead = Color.White;
                self.colorArmorBody = Color.White;
                self.colorArmorLegs = Color.White;
                self.colorEyeWhites = Color.White;
                self.colorEyes = self.drawPlayer.eyeColor;
                self.colorHair = self.drawPlayer.hairColor;
                self.colorHead = self.drawPlayer.skinColor;
                self.colorBodySkin = self.drawPlayer.skinColor;
                self.colorShirt = self.drawPlayer.shirtColor;
                self.colorUnderShirt = self.drawPlayer.underShirtColor;
                self.colorPants = self.drawPlayer.pantsColor;
                self.colorShoes = self.drawPlayer.shoeColor;
                self.colorLegs = self.drawPlayer.skinColor;
                self.colorMount = Color.White;
                self.headGlowColor = Color.White;
                self.bodyGlowColor = Color.White;
                self.armGlowColor = Color.White;
                self.legsGlowColor = Color.White;
            }
        }
        orig.Invoke(ref self);
    }

    private void On_PlayerDrawSet_BoringSetup_2(On_PlayerDrawSet.orig_BoringSetup_2 orig, ref PlayerDrawSet self, Player player, System.Collections.Generic.List<DrawData> drawData, System.Collections.Generic.List<int> dust, System.Collections.Generic.List<int> gore, Vector2 drawPosition, float shadowOpacity, float rotation, Vector2 rotationOrigin)
    {
        //if (player.GetModPlayer<AvalonPlayer>().ShadowRing)
        //{
        //    if (player.shroomiteStealth || player.setVortex) return;
        //}
        orig.Invoke(ref self, player, drawData, dust, gore, drawPosition, shadowOpacity, rotation, rotationOrigin);
    }

}
