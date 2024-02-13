using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Items.Tools;

internal class WaypointSystem : ModSystem
{
    public static Vector2 savedLocation;
    public override void OnWorldLoad()
    {
        savedLocation = Vector2.Zero;
    }
    public override void SaveWorldData(TagCompound tag)
    {
        tag["Avalon:PPSavedLocation"] = savedLocation;
    }
    public override void LoadWorldData(TagCompound tag)
    {
        savedLocation = tag.Get<Vector2>("Avalon:PPSavedLocation");
    }
}
public class PortablePylon : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 25;
        Item.useTurn = true;
        Item.value = Item.sellPrice(0, 2);
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 25;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item6;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.PotionOfReturn, 5)
            .AddIngredient(ItemID.MagicMirror)
            .AddIngredient(ItemID.HellstoneBar, 8)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.PotionOfReturn, 5)
            .AddIngredient(ItemID.IceMirror)
            .AddIngredient(ItemID.HellstoneBar, 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        if (player.altFunctionUse == 2 && player.itemTime == Item.useTime / 2)
        {
            WaypointSystem.savedLocation = player.Center + new Vector2(0, -15);
            Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.SetWaypoint"));
        }
        else
        {
            if (player.itemTime == 0)
            {
                player.itemTime = Item.useTime;
            }
            else if (player.itemTime == Item.useTime / 2)
            {
                if (WaypointSystem.savedLocation != Vector2.Zero)
                {
                    for (int num345 = 0; num345 < 70; num345++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default(Color), 1.5f);
                    }
                    player.grappling[0] = -1;
                    player.grapCount = 0;
                    for (int num346 = 0; num346 < 1000; num346++)
                    {
                        if (Main.projectile[num346].active && Main.projectile[num346].owner == player.whoAmI && Main.projectile[num346].aiStyle == 7)
                        {
                            Main.projectile[num346].Kill();
                        }
                    }
                    player.Teleport(WaypointSystem.savedLocation);
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, WaypointSystem.savedLocation.X, WaypointSystem.savedLocation.Y, 0);
                    for (int num347 = 0; num347 < 70; num347++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
                    }
                }
                else
                {
                    Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.PortablePylon.NoWaypointFound"), 250, 0, 0);
                }
            }
        }
    }
}
