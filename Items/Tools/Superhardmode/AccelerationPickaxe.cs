using System.Collections.Generic;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using Avalon.Common.Players;

namespace Avalon.Items.Tools.Superhardmode;

public class AccelerationPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 28;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.pick = 400;
        Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
        Item.width = dims.Width;
        Item.useTime = 12;
        Item.knockBack = 2f;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 6;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 1016000;
        Item.useAnimation = 12;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<AccelerationDrill>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        Recipe.Create(ModContent.ItemType<AccelerationDrill>())
            .AddIngredient(Type)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<AccelerationPickaxeSpeed>());
        }
        if (player.controlUseItem)
        {
            if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
            {
                Tile t = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
                if (TileID.Sets.Ore[t.TileType])
                {
                    ClassExtensions.VeinMine(new Point(Player.tileTargetX, Player.tileTargetY), t.TileType);
                }
            }
        }
    }
}

public class AccelerationPickaxeSpeed : ModItem
{
    public override string Texture => ModContent.GetInstance<AccelerationPickaxe>().Texture;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 28;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.pick = 400;
        Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
        Item.width = dims.Width;
        Item.useTime = 12;
        Item.knockBack = 2f;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 6;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 1016000;
        Item.useAnimation = 12;
        Item.height = dims.Height;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ModContent.ItemType<AccelerationDrill>())
    //        .AddTile(TileID.TinkerersWorkbench)
    //        .Register();

    //    Recipe.Create(ModContent.ItemType<AccelerationDrill>())
    //        .AddIngredient(Type)
    //        .AddTile(TileID.TinkerersWorkbench)
    //        .Register();
    //}

    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<AccelerationPickaxe>());
        }
        if (player.controlUseItem && player.whoAmI == Main.myPlayer)
        {
            if (player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetX && (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f >= Player.tileTargetX && player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost <= Player.tileTargetY && (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f >= Player.tileTargetY)
            {
                Point p = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
                for (int x = p.X - 1; x <= p.X + 1; x++)
                {
                    for (int y = p.Y - 1; y <= p.Y + 1; y++)
                    {
                        if (Main.tile[x, y].HasTile && !Main.tileHammer[Main.tile[x, y].TileType] && !Main.tileAxe[Main.tile[x, y].TileType])
                        {
                            if (!TileID.Sets.BasicChest[Main.tile[x, y].TileType])
                            {
                                WorldGen.KillTile(x, y);
                                if (Main.netMode == NetmodeID.MultiplayerClient)
                                {
                                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 0, x, y, 0f, 0);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
