using Avalon.Buffs;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;
public class DesertGamblerHeadLayer : PlayerDrawLayer
{
    public override bool IsHeadLayer => true;

    public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Head);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0f)
            //(drawInfo.drawPlayer.merman && !drawInfo.drawPlayer.hideMerman) ||
            //(drawInfo.drawPlayer.wereWolf && !drawInfo.drawPlayer.hideWolf) ||
            //(drawInfo.drawPlayer.GetModPlayer<AvalonPlayer>().lavaMerman && !drawInfo.drawPlayer.GetModPlayer<AvalonPlayer>().HideVarefolk))
        {
            return;
        }

        Player p = drawInfo.drawPlayer;
        var helmoffset = drawInfo.helmetOffset;
        if (p.head == EquipLoader.GetEquipSlot(Mod, "DesertGambler", EquipType.Head) &&
            (p.GetModPlayer<AvalonPlayer>().DesertGambler && p.GetModPlayer<AvalonPlayer>().DesertGamblerVisible || p.GetModPlayer<AvalonPlayer>().ForceGambler))
        {
            if (p.GetModPlayer<DeadeyePlayer>().Deadeye)
            {
                float val = p.GetModPlayer<DeadeyePlayer>().DeadeyeTimer * 255;
                Color c = new Color(val, val, val, val);

                DrawData glow = new DrawData(Mod.Assets.Request<Texture2D>("Items/Accessories/PreHardmode/DesertGambler_Head_Glow").Value,
                    helmoffset + new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - drawInfo.drawPlayer.bodyFrame.Width / 2 + drawInfo.drawPlayer.width / 2),
                    (int)(drawInfo.Position.Y - Main.screenPosition.Y + drawInfo.drawPlayer.height - drawInfo.drawPlayer.bodyFrame.Height + 4f)) - new Vector2(0, 2) + drawInfo.drawPlayer.headPosition + drawInfo.headVect,
                    drawInfo.drawPlayer.bodyFrame, c, drawInfo.drawPlayer.headRotation, drawInfo.headVect, 1f, drawInfo.playerEffect);
                drawInfo.DrawDataCache.Add(glow);
            }
            DrawData value = new DrawData(Mod.Assets.Request<Texture2D>("Items/Accessories/PreHardmode/DesertGambler_Head_Real").Value,
                helmoffset + new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - drawInfo.drawPlayer.bodyFrame.Width / 2 + drawInfo.drawPlayer.width / 2),
                (int)(drawInfo.Position.Y - Main.screenPosition.Y + drawInfo.drawPlayer.height - drawInfo.drawPlayer.bodyFrame.Height + 4f)) - new Vector2(0, 2) + drawInfo.drawPlayer.headPosition + drawInfo.headVect,
                drawInfo.drawPlayer.bodyFrame, drawInfo.colorArmorHead, drawInfo.drawPlayer.headRotation, drawInfo.headVect, 1f, drawInfo.playerEffect);
            value.shader = drawInfo.cHead;
            drawInfo.DrawDataCache.Add(value);
        }
    }
}
