using Avalon.Common;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class GemRobeTimSpawning : ModHook
{
    protected override void Apply()
    {
        IL_NPC.SpawnNPC += RobeTimIncrease;
    }
    private void RobeTimIncrease(ILContext il)
    {
        ILCursor c = new ILCursor(il);
        ILLabel Illabel = null;
        if (!c.TryGotoNext(MoveType.After,
            i => i.MatchLdsfld<Main>("player"),
            i => i.MatchLdloc(14),
            i => i.MatchLdelemRef(),
            i => i.MatchLdfld<Player>("armor"),
            i => i.MatchLdcI4(1),
            i => i.MatchLdelemRef(),
            i => i.MatchLdfld<Item>("type"),
            i => i.MatchLdcI4(4256),
            i => i.MatchBeq(out Illabel)))
        {
            return;
        }
        if (Illabel == null) return;
        c.EmitLdloc(14);
        c.EmitDelegate((int k) =>
        {
            return Main.player[k].armor[1].type == ModContent.ItemType<Items.Armor.PreHardmode.TourmalineRobe>() ||
            Main.player[k].armor[1].type == ModContent.ItemType<Items.Armor.PreHardmode.PeridotRobe>() ||
            Main.player[k].armor[1].type == ModContent.ItemType<Items.Armor.PreHardmode.ZirconRobe>();
        });
        c.EmitBrtrue(Illabel);
    }
}
