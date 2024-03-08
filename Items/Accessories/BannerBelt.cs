using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Avalon.Common;
using Microsoft.Xna.Framework;

namespace Avalon.Items.Accessories;

public class BannerBelt : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.rare = ItemRarityID.Lime;
        Item.accessory = true;
        Item.value = Item.sellPrice(gold: 5);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BannerBeltPlayer>().BannerBelt = true;
    }
}
public class BannerBeltPlayer : ModPlayer
{
    public bool BannerBelt = false;
    public override void ResetEffects()
    {
        BannerBelt = false;
    }
    public void UpdateBanners(Player Player)
    {
        List<int> banners = new();
        for (int i = 0; i < Player.inventory.Length; i++)
        {
            if (Player.inventory[i].createTile == TileID.Banners)
            {
                banners.Add(Player.inventory[i].placeStyle - 21);
            }
        }
        for (int i = 0; i < Player.bank.item.Length; i++)
        {
            if (Player.bank.item[i].createTile == TileID.Banners)
            {
                banners.Add(Player.bank.item[i].placeStyle - 21);
            }
        }
        for (int i = 0; i < Player.bank2.item.Length; i++)
        {
            if (Player.bank2.item[i].createTile == TileID.Banners)
            {
                banners.Add(Player.bank2.item[i].placeStyle - 21);
            }
        }
        for (int i = 0; i < Player.bank3.item.Length; i++)
        {
            if (Player.bank3.item[i].createTile == TileID.Banners)
            {
                banners.Add(Player.bank3.item[i].placeStyle - 21);
            }
        }
        for (int i = 0; i < Player.bank4.item.Length; i++)
        {
            if (Player.bank4.item[i].createTile == TileID.Banners)
            {
                banners.Add(Player.bank4.item[i].placeStyle - 21);
            }
        }

        if (banners.Count > 0)
        {
            Player.AddBuff(BuffID.MonsterBanner, 2, false);
            for (int j = 0; j < banners.Count; j++)
            {
                Main.SceneMetrics.hasBanner = true;
                Main.SceneMetrics.NPCBannerBuff[banners[j]] = true;
            }
        }
    }
    public override void UpdateEquips()
    {
        if (BannerBelt)
        {
            UpdateBanners(Player);
        }
    }
}

public class BannerBeltHook : ModHook
{
    protected override void Apply()
    {
        On_SceneMetrics.Reset += On_SceneMetrics_Reset;
    }

    private void On_SceneMetrics_Reset(On_SceneMetrics.orig_Reset orig, SceneMetrics self)
    {
        orig.Invoke(self);
        if (Main.gameMenu)
            return;
        if (Main.LocalPlayer.GetModPlayer<BannerBeltPlayer>().BannerBelt)
        {
            Main.LocalPlayer.GetModPlayer<BannerBeltPlayer>().UpdateBanners(Main.LocalPlayer);
            for (int i = 0; i < 255; i++)
            {
                if (Main.myPlayer != i && Vector2.Distance(Main.player[i].Center, Main.LocalPlayer.Center) < 16 * 75)
                {
                    Main.player[i].GetModPlayer<BannerBeltPlayer>().UpdateBanners(Main.LocalPlayer);
                }
            }
        }
    }
}
