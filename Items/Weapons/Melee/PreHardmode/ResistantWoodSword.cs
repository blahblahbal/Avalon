using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode
{
    public class ResistantWoodSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AshWoodSword);
            Item.useAnimation += 3;
            Item.damage += 2;
        }
    }
}
