using Avalon.Rarities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Avalon.Common
{
    public class AvalonGlobalBuff : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (Avalon.Data.Sets.Buffs.Elixr[type])
            {
                rare = ModContent.RarityType<ElixrBuffNameRarity>();
            }

            base.ModifyBuffText(type, ref buffName, ref tip, ref rare);
        }
    }
}
