using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode; 

public class DesertLongsword : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.AntlionClaw);
        Item.scale = 0.78f;
    }
}