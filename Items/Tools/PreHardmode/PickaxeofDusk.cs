using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
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
        Item.useTurn = true;
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
        Item.useTurn = true;
        Item.useTime = 15;
        Item.knockBack = 0.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.rare = ItemRarityID.Orange;
        Item.value = 27000;
        Item.useAnimation = 15;
    }
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer && player.ItemAnimationJustStarted)
        {
            if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
            {
                Point p = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
                for (int x = p.X - 1; x <= p.X + 1; x++)
                {
                    for (int y = p.Y - 1; y <= p.Y + 1; y++)
                    {
                        player.PickTile(x, y, 100);
                    }
                }
            }
        }
        return null;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<PickaxeofDusk>());
        }
    }
}
