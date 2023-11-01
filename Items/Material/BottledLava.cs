using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class BottledLava : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 50;
        Item.height = dims.Height;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
    }
    public override bool? UseItem(Player player)
    {
        player.AddBuff(BuffID.OnFire3, 60 * 20);
        player.AddBuff(BuffID.OnFire, 60 * 20);
        ExxoAvalonOrigins.Achievements?.Call("Event", "DrinkBottledLava");
        return true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Bottle)
            .AddIngredient(ItemID.Obsidian)
            .AddCondition(Condition.NearLava)
            .Register();
    }
}
