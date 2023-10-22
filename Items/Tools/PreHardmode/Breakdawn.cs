using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System.Reflection;
using Avalon.Reflection;
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
        Item.axe = 32;
        Item.useTime = 24;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.rare = ItemRarityID.Orange;
        Item.value = 27000;
        Item.useAnimation = 24;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.TheBreaker)
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ItemID.AcornAxe)
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.FleshGrinder)
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ItemID.AcornAxe)
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<MucusHammer>())
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ItemID.AcornAxe)
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<Breakdawn3x3>());
        }
        if (player.whoAmI == Main.myPlayer && player.ItemAnimationJustStarted)
        {
            Point tilePosPoint = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
            int tileType = Main.tile[tilePosPoint.X, tilePosPoint.Y].TileType;
            int dmgAmt = (int)(Item.axe * 1.2f);
            if (Main.tileAxe[tileType])
            {
                if (!WorldGen.CanKillTile(tilePosPoint.X, tilePosPoint.Y))
                {
                    dmgAmt = 0;
                }
                Main.NewText(player.hitTile.AddDamage(tileType, dmgAmt));
                if (player.hitTile.AddDamage(tileType, dmgAmt) >= 100)
                {
                    player.ClearMiningCacheAt(tilePosPoint.X, tilePosPoint.Y, 1);
                    bool flag = player.IsBottomOfTreeTrunkNoRoots(tilePosPoint.X, tilePosPoint.Y);

                    // kill the tile, with mp support
                    WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y);
                    }

                    // replant the sapling
                    if (flag)
                    {
                        player.TryReplantingTree(tilePosPoint.X, tilePosPoint.Y);
                    }
                    player.hitTile.Clear(tileType);
                }
                else
                {
                    WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y, fail: true);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y, 1f);
                    }
                }
                if (dmgAmt != 0)
                {
                    player.hitTile.Prune();
                }
                player.ApplyItemTime(Item);
            }
        }
    }
    //public override bool? UseItem(Player player)
    //{
    //    if (player.whoAmI == Main.myPlayer && player.controlUseItem)
    //    {
    //        Point tilePosPoint = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
    //        int tileType = Main.tile[tilePosPoint.X, tilePosPoint.Y].TileType;
    //        int dmgAmt = (int)(Item.axe / 2 * 1.2f);
    //        if (Main.tileAxe[tileType])
    //        {
    //            if (player.hitTile.AddDamage(tileType, dmgAmt) >= 100)
    //            {
    //                MethodInfo clearCache = typeof(Player).GetMethod("ClearMiningCacheAt", BindingFlags.NonPublic | BindingFlags.Instance);
    //                clearCache.Invoke(player, new object[] { tilePosPoint.X, tilePosPoint.Y, 1 });

    //                MethodInfo bottomOfTree = typeof(Player).GetMethod("IsBottomOfTreeTrunkNoRoots", BindingFlags.NonPublic | BindingFlags.Instance);
    //                bool flag = (bool)bottomOfTree.Invoke(player, new object[] { tilePosPoint.X, tilePosPoint.Y });
    //                WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y);
    //                if (Main.netMode == NetmodeID.MultiplayerClient)
    //                {
    //                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y);
    //                }
    //                if (flag)
    //                {
    //                    MethodInfo tryReplantingTree = typeof(Player).GetMethod("TryReplantingTree", BindingFlags.NonPublic | BindingFlags.Instance);
    //                    tryReplantingTree.Invoke(player, new object[] { tilePosPoint.X, tilePosPoint.Y });
    //                }
    //            }
    //            else
    //            {
    //                WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y, fail: true);
    //                if (Main.netMode == NetmodeID.MultiplayerClient)
    //                {
    //                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y, 1f);
    //                }
    //            }
    //            if (dmgAmt != 0)
    //            {
    //                player.hitTile.Prune();
    //            }
    //        }
    //    }
    //    return null;
    //}
}
public class BreakdawnHook : ModHook
{
    protected override void Apply()
    {
        IL_WorldGen.KillTile_GetItemDrops += IL_WorldGen_KillTile_GetItemDrops;
    }

    private void IL_WorldGen_KillTile_GetItemDrops(MonoMod.Cil.ILContext il)
    {
        Hooks.Utilities.AddAlternativeIdChecks(il, (ushort)ItemID.AcornAxe, id => ItemID.Sets.Factory.CreateBoolSet(ModContent.ItemType<Breakdawn>())[id]);
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
        Item.useAnimation = 24;
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
