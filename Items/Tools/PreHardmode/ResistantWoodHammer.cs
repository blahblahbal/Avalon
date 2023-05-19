using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode
{
    public class ResistantWoodHammer : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AshWoodHammer);
            Item.useAnimation += 2;
            Item.useTime += 2;
            Item.damage += 2;
        }
    }
}