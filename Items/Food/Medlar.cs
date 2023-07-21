using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Food;

public class Medlar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
        ItemID.Sets.FoodParticleColors[Item.type] = new Color[3]
        {
            new Color(211, 165, 95),
            new Color(153, 94, 48),
            new Color(89, 47, 24)
        };
        ItemID.Sets.IsFood[Type] = true;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Food;
    }
    public override void SetDefaults()
    {
        // DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
        Item.DefaultToFood(22, 18, BuffID.WellFed2, 60 * 60 * 7); // 57600 is 16 minutes: 16 * 60 * 60
        Item.value = Item.buyPrice(0, 2);
        Item.rare = ItemRarityID.Blue;
    }
}
