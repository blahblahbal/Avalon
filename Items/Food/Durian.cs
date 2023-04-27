using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Food;

public class Durian : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
        ItemID.Sets.FoodParticleColors[Item.type] = new Color[3]
        {
            new Color(184, 130, 35),
            new Color(170, 168, 43),
            new Color(236, 199, 75)
        };
        ItemID.Sets.IsFood[Type] = true;
    }

    public override void SetDefaults()
    {
        // DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
        Item.DefaultToFood(22, 18, BuffID.WellFed3, 60 * 60 * 9); // 57600 is 16 minutes: 16 * 60 * 60
        Item.value = Item.buyPrice(0, 2);
        Item.rare = ItemRarityID.Blue;
    }
}
