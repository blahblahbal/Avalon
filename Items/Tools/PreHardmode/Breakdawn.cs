using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class Breakdawn : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.UseSound = SoundID.Item1;
        Item.damage = 26;
        Item.autoReuse = true;
        Item.hammer = 70;
        Item.axe = 22;
        Item.useTime = 24;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.rare = ItemRarityID.Orange;
        Item.value = 27000;
        Item.useAnimation = 20;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.TheBreaker)
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ModContent.ItemType<JungleHammer>())
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.FleshGrinder)
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ModContent.ItemType<JungleHammer>())
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<MucusHammer>())
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ModContent.ItemType<JungleHammer>())
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<Breakdawn3x3>());
        }
    }
}
public class Breakdawn3x3 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.UseSound = SoundID.Item1;
        Item.damage = 26;
        Item.autoReuse = true;
        Item.hammer = 70;
        Item.axe = 22;
        Item.useTime = 24;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.rare = ItemRarityID.Orange;
        Item.value = 27000;
        Item.useAnimation = 20;
    }

    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<Breakdawn>());
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
                        if (Main.tile[x, y].HasTile && Main.tileAxe[Main.tile[x, y].TileType])
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
                        if (Main.tile[x, y].WallType > 0)
                        {
                            WorldGen.KillWall(x, y);
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 2, x, y);
                            }
                        }
                    }
                }
            }
        }
    }
}
