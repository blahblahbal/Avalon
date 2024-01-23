using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class SanguineSacrifice : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
}
