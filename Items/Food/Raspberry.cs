using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Food;

public class Raspberry : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
        ItemID.Sets.FoodParticleColors[Item.type] = new Color[3]
        {
            new Color(247, 122, 185),
            new Color(198, 59, 98),
            new Color(113, 35, 47)
        };
        ItemID.Sets.IsFood[Type] = true;
    }

    public override void SetDefaults()
    {
        // DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
        Item.DefaultToFood(22, 18, BuffID.WellFed2, 60 * 60 * 4); // 57600 is 16 minutes: 16 * 60 * 60
        Item.value = Item.buyPrice(0, 2);
        Item.rare = ItemRarityID.Blue;
    }
}
