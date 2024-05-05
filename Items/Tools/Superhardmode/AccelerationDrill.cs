using System.Collections.Generic;
using Avalon.Common.Players;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Superhardmode;

public class AccelerationDrill : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 25;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.channel = true;
        Item.scale = 1f;
        Item.shootSpeed = 32f;
        Item.pick = 400;
        Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 7;
        Item.knockBack = 1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.AccelerationDrill>();
        Item.UseSound = SoundID.Item23;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 6;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 1016000;
        Item.useAnimation = 9;
        Item.height = dims.Height;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            int pfix = Item.prefix;
            Item.ChangeItemType(ModContent.ItemType<AccelerationDrillSpeed>());
            Item.Prefix(pfix);
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

public class AccelerationDrillSpeed : ModItem
{
    public override string Texture => ModContent.GetInstance<AccelerationDrill>().Texture;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 25;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.channel = true;
        Item.scale = 1f;
        Item.shootSpeed = 32f;
        Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 7;
        Item.knockBack = 1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.AccelerationDrill>();
        Item.UseSound = SoundID.Item23;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 6;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 1016000;
        Item.useAnimation = 9;
        Item.height = dims.Height;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            int pfix = Item.prefix;
            Item.ChangeItemType(ModContent.ItemType<AccelerationDrill>());
            Item.Prefix(pfix);
        }
        if (player.controlUseItem)
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
