using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class ContagionTreeLeaf : ModGore
{
    public override string Texture => "Avalon/Tiles/Contagion/ContagionTree_Leaf";

    public override void SetStaticDefaults()
    {
        
        GoreID.Sets.SpecialAI[Type] = 3;
    }
}
