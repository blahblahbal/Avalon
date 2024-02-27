using System;
using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Avalon.Hooks;

public class PlayerLegFrame : ModHook
{
    protected override void Apply()
    {
        IL_Player.PlayerFrame += IL_Player_PlayerFrame;
    }

    private void IL_Player_PlayerFrame(ILContext il)
    {
        ILCursor c = new(il);
        c.GotoNext(MoveType.After, i => i.MatchLdarg0(), i => i.MatchLdflda<Player>("legFrame"), i => i.MatchLdfld<Microsoft.Xna.Framework.Rectangle>("Y"), i => i.MatchLdarg0(), i => i.MatchLdflda<Player>("legFrame"), i => i.MatchLdfld<Microsoft.Xna.Framework.Rectangle>("Height"), i => i.MatchLdcI4(7));
        c.EmitDelegate<Func<int, int>>(frame => 6);
        c.GotoNext(MoveType.After, i => i.MatchLdarg0(), i => i.MatchLdflda<Player>("legFrame"), i => i.MatchLdarg0(), i => i.MatchLdflda<Player>("legFrame"), i => i.MatchLdfld<Microsoft.Xna.Framework.Rectangle>("Height"), i => i.MatchLdcI4(7));
        c.EmitDelegate<Func<int, int>>(frame => 6);
    }
}
