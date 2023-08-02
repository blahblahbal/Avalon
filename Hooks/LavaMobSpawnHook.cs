using Avalon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using MonoMod.Cil;
using Terraria.ID;
using Mono.Cecil.Cil;

namespace Avalon.Hooks
{
    internal class LavaMobSpawnHook : ModHook
    {
        protected override void Apply()
        {
            //IL_NPC.SpawnNPC += IL_NPC_SpawnNPC;
        }

        private void IL_NPC_SpawnNPC(ILContext il)
        {
            ILCursor c = new(il);
            if (c.TryGotoNext(i => i.MatchCall<Tile>("lava")))
            {

            }
        }
    }
}
