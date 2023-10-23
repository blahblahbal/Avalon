using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class PickaxeofDusk : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.UseSound = SoundID.Item1;
        Item.damage = 12;
        Item.autoReuse = true;
        Item.pick = 100;
        Item.useTime = 15;
        Item.knockBack = 0.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.rare = ItemRarityID.Orange;
        Item.value = 27000;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.NightmarePickaxe)
            .AddIngredient(ModContent.ItemType<SapphirePickaxe>())
            .AddIngredient(ModContent.ItemType<JunglePickaxe>())
            .AddIngredient(ItemID.MoltenPickaxe)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.DeathbringerPickaxe)
            .AddIngredient(ModContent.ItemType<SapphirePickaxe>())
            .AddIngredient(ModContent.ItemType<JunglePickaxe>())
            .AddIngredient(ItemID.MoltenPickaxe)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<GoldminePickaxe>())
            .AddIngredient(ModContent.ItemType<SapphirePickaxe>())
            .AddIngredient(ModContent.ItemType<JunglePickaxe>())
            .AddIngredient(ItemID.MoltenPickaxe)
            .AddTile(TileID.DemonAltar)
            .Register();
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<PickaxeofDusk3x3>());
        }
        if (player.inventory[player.selectedItem].type == Type)
        {
            player.pickSpeed -= 0.3f;
        }
    }
}
public class PickaxeofDusk3x3 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.UseSound = SoundID.Item1;
        Item.damage = 12;
        Item.autoReuse = true;
        Item.pick = 100;
        Item.useTime = 15;
        Item.knockBack = 0.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.rare = ItemRarityID.Orange;
        Item.value = 27000;
        Item.useAnimation = 15;
    }

    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<PickaxeofDusk>());
        }
        if (player.controlUseItem)
        {
            if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
            {
                Point p = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
                for (int x = p.X - 1; x <= p.X + 1; x++)
                {
                    for (int y = p.Y - 1; y <= p.Y + 1; y++)
                    {
                        if (ClassExtensions.GetTileMinPick(Main.tile[x, y]) <= Item.pick && Main.tile[x, y].HasTile &&
                            !Main.tileHammer[Main.tile[x, y].TileType] && !Main.tileAxe[Main.tile[x, y].TileType])
                        {
                            if (!TileID.Sets.BasicChest[Main.tile[x, y].TileType])
                            {
                                WorldGen.KillTile(x, y);
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 0, x, y);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
