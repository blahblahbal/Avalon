using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Food;

public class RockCandy : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
        ItemID.Sets.FoodParticleColors[Item.type] = new Color[4]
        {
            new Color(255, 147, 160),
            new Color(255, 82, 93),
            new Color(206, 25, 35),
            new Color(129, 111, 111)
        };
        ItemID.Sets.IsFood[Type] = true;
    }

    public override void SetDefaults()
    {
        // DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
        Item.DefaultToFood(22, 18, BuffID.WellFed3, 60 * 60 * 5); // 57600 is 16 minutes: 16 * 60 * 60
    }
}
