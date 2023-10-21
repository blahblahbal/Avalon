using Avalon.Buffs;
using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;

public class DesertGamblerHook : ModHook
{
    protected override void Apply()
    {
        On_PlayerDrawLayers.DrawPlayer_01_BackHair += On_PlayerDrawLayers_DrawPlayer_01_BackHair;
        On_PlayerDrawLayers.DrawPlayer_21_Head += On_PlayerDrawLayers_DrawPlayer_21_Head;
    }

    private void On_PlayerDrawLayers_DrawPlayer_21_Head(On_PlayerDrawLayers.orig_DrawPlayer_21_Head orig, ref PlayerDrawSet drawinfo)
    {
        if ((drawinfo.drawPlayer.merman && !drawinfo.drawPlayer.hideMerman) ||
            (drawinfo.drawPlayer.wereWolf && !drawinfo.drawPlayer.hideWolf) ||
            (drawinfo.drawPlayer.GetModPlayer<AvalonPlayer>().lavaMerman && !drawinfo.drawPlayer.GetModPlayer<AvalonPlayer>().HideVarefolk) ||
            drawinfo.drawPlayer.head > 0)
        {
            orig.Invoke(ref drawinfo);
            return;
        }
        if (drawinfo.drawPlayer.HasItemInArmor(ModContent.ItemType<Items.Accessories.PreHardmode.DesertGambler>()) &&
            drawinfo.drawPlayer.GetModPlayer<AvalonPlayer>().DesertGamblerVisible && drawinfo.drawPlayer.head == 0)
        {
            MethodInfo dynMethod = typeof(Terraria.DataStructures.PlayerDrawLayers).GetMethod("DrawPlayer_21_Head_TheFace",
                BindingFlags.NonPublic | BindingFlags.Static);
            dynMethod.Invoke(null, new object[] { drawinfo });
            Vector2 vector = new(-drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2, drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4);
            Vector2 position = (drawinfo.Position - Main.screenPosition + vector).Floor() + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
            position += drawinfo.hairOffset;
            DrawData item13 = new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, position, drawinfo.hairFrontFrame, drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect);
            item13.shader = drawinfo.hairDyePacked;
            drawinfo.DrawDataCache.Add(item13);
            
            return;
        }
        orig.Invoke(ref drawinfo);
    }

    private void On_PlayerDrawLayers_DrawPlayer_01_BackHair(On_PlayerDrawLayers.orig_DrawPlayer_01_BackHair orig, ref PlayerDrawSet drawinfo)
    {
        if (drawinfo.drawPlayer.HasItemInArmor(ModContent.ItemType<Items.Accessories.PreHardmode.DesertGambler>()) && drawinfo.drawPlayer.GetModPlayer<AvalonPlayer>().DesertGamblerVisible && !drawinfo.drawPlayer.merman && !drawinfo.drawPlayer.wereWolf && !drawinfo.drawPlayer.GetModPlayer<AvalonPlayer>().lavaMerman)
        {
            //drawinfo.hatHair = true;
        }
        orig.Invoke(ref drawinfo);
    }
}
public class DesertGamblerHeadLayer : PlayerDrawLayer
{
    public override bool IsHeadLayer => true;

    public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Head);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0f || drawInfo.drawPlayer.head > 0 ||
            (drawInfo.drawPlayer.merman && !drawInfo.drawPlayer.hideMerman) ||
            (drawInfo.drawPlayer.wereWolf && !drawInfo.drawPlayer.hideWolf) ||
            (drawInfo.drawPlayer.GetModPlayer<AvalonPlayer>().lavaMerman && !drawInfo.drawPlayer.GetModPlayer<AvalonPlayer>().HideVarefolk))
        {
            return;
        }

        Player p = drawInfo.drawPlayer;
        var helmoffset = drawInfo.helmetOffset;
        if (p.HasItemInArmor(ModContent.ItemType<Items.Accessories.PreHardmode.DesertGambler>()) && p.GetModPlayer<AvalonPlayer>().DesertGamblerVisible)
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



            DrawData value = new DrawData(Mod.Assets.Request<Texture2D>("Items/Accessories/PreHardmode/DesertGambler_Head").Value,
                helmoffset + new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - drawInfo.drawPlayer.bodyFrame.Width / 2 + drawInfo.drawPlayer.width / 2),
                (int)(drawInfo.Position.Y - Main.screenPosition.Y + drawInfo.drawPlayer.height - drawInfo.drawPlayer.bodyFrame.Height + 4f)) - new Vector2(0, 2) + drawInfo.drawPlayer.headPosition + drawInfo.headVect,
                drawInfo.drawPlayer.bodyFrame, drawInfo.colorArmorHead, drawInfo.drawPlayer.headRotation, drawInfo.headVect, 1f, drawInfo.playerEffect);
            value.shader = drawInfo.cFace;
            drawInfo.DrawDataCache.Add(value);
        }
    }
}
