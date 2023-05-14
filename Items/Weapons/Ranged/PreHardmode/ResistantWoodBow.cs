using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode
{
    public class ResistantWoodBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AshWoodBow);
            Item.useAnimation += 2;
            Item.useTime += 2;
            Item.damage += 1;
            Item.shootSpeed += 2;
        }
    }
}
