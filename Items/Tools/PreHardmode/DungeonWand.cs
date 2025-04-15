using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class DungeonWand : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Green;
        Item.useTurn = true;
        Item.useTime = 7;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup("DungeonBrick", 50)
            .AddIngredient(ItemID.GoldenKey, 2)
            .AddIngredient(ItemID.Bone, 20)
            .AddTile(TileID.BoneWelder)
            .AddCondition(Condition.InGraveyard)
            .Register();
    }
    public override void UseAnimation(Player player)
    {
        if (Main.myPlayer == player.whoAmI)
        {
            int wall = -1;
            for (int q = 0; q < player.inventory.Length; q++)
            {
                int type = player.inventory[q].type;
                if (Data.Sets.Item.DungeonWallItems[type])
                {
                    wall = AvalonGlobalItem.DungeonWallItemToBackwallID(type);
                    break;
                }
            }
            Item.createWall = wall;
        }
    }
    public override void HoldItem(Player player)
    {
        if (player.ItemAnimationEndingOrEnded && player.whoAmI == Main.myPlayer)
        {
            Item.createWall = -1;
        }
    }
}
