using System;
using Avalon.Items.Armor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;

public class SuperSonicBodyLegsLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new BeforeParent(Terraria.DataStructures.PlayerDrawLayers.HandOnAcc);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0f)
        {
            return;
        }

        Player p = drawInfo.drawPlayer;
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (p.gravDir == 1f)
        {
            if (p.direction == 1)
            {
                spriteEffects = SpriteEffects.None;
            }
            else
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (!p.dead)
            {
                p.legPosition.Y = 0f;
                p.headPosition.Y = 0f;
                p.bodyPosition.Y = 0f;
            }
        }
        else
        {
            if (p.direction == 1)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            else
            {
                spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
            }

            if (!p.dead)
            {
                p.legPosition.Y = 6f;
                p.headPosition.Y = 6f;
                p.bodyPosition.Y = 6f;
            }
        }

        var vector2 = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.75f);
        var origin = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.5f);
        var vector3 = new Vector2(p.legFrame.Width * 0.5f, p.legFrame.Height * 0.4f);

        if ((p.dye[1].type == ItemID.YellowDye || p.dye[1].type == ItemID.BrightYellowDye) && p.body == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "SonicShirt", EquipType.Body))
        {
            Vector2 vector = new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - drawInfo.drawPlayer.bodyFrame.Width / 2 + drawInfo.drawPlayer.width / 2), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2((float)(drawInfo.drawPlayer.bodyFrame.Width / 2), (float)(drawInfo.drawPlayer.bodyFrame.Height / 2));
            Vector2 value = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
            value.Y -= 2f;
            vector += value * (-drawInfo.playerEffect.HasFlag((Enum)(object)(SpriteEffects)2).ToDirectionInt());
            float bodyRotation = drawInfo.drawPlayer.bodyRotation;
            Vector2 val = vector;
            Vector2 bodyVect = drawInfo.bodyVect;
            Vector2 compositeOffset_FrontArm = GetCompositeOffset_FrontArm(ref drawInfo);
            vector = val + compositeOffset_FrontArm;
            bodyVect += compositeOffset_FrontArm;

            Vector2 valBack = vector;
            Vector2 bodyVectBack = drawInfo.bodyVect;
            Vector2 compositeOffset_BackArm = GetCompositeOffset_BackArm(ref drawInfo);
            Vector2 vectorBack = valBack + compositeOffset_BackArm;
            bodyVectBack += compositeOffset_BackArm;
            if (drawInfo.playerEffect == SpriteEffects.None || drawInfo.playerEffect == SpriteEffects.FlipVertically)
            {
                vector.X += 5f;
                vectorBack.X--;
                if (drawInfo.playerEffect == SpriteEffects.FlipVertically) vectorBack.Y += 2f;
                else vectorBack.Y -= 2f;
            }
            else if (drawInfo.playerEffect == SpriteEffects.FlipHorizontally || drawInfo.playerEffect == (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically))
            {
                vector.X -= 5f;
                vectorBack.X++;
                if (drawInfo.playerEffect == (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically)) vectorBack.Y += 2f;
                else vectorBack.Y -= 2f;
            }
            if (!drawInfo.drawPlayer.invis)
            {
                Rectangle bodyFrame = drawInfo.drawPlayer.bodyFrame;
                int num = drawInfo.armorAdjust;
                bodyFrame.X += num;
                bodyFrame.Width -= num;
                Texture2D texture = Mod.Assets.Request<Texture2D>("Items/Vanity/SuperSonicShirt_Body").Value;
                Terraria.DataStructures.PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.Torso, new DrawData(texture, vector, drawInfo.compTorsoFrame, drawInfo.colorArmorBody, bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect));
                //DrawData item3 = new DrawData(texture, new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)) + num), (float)(int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2((float)(drawInfo.drawPlayer.bodyFrame.Width / 2), (float)(drawInfo.drawPlayer.bodyFrame.Height / 2)), bodyFrame, Color.White, drawInfo.drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect);
                ////item3.shader = drawInfo.cBody;
                //drawInfo.DrawDataCache.Add(item3);


                Texture2D value2 = Mod.Assets.Request<Texture2D>("Items/Vanity/SuperSonicShirt_Body").Value;
                DrawData drawData2 = new DrawData(value2, vector, drawInfo.compFrontArmFrame, Color.White, bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
                //drawData2.shader = drawInfo.cBody;
                DrawData drawData = drawData2;
                drawInfo.DrawDataCache.Add(drawData);

                DrawData drawData3 = new DrawData(value2, vectorBack, drawInfo.compBackArmFrame, Color.White, bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
                //drawData3.shader = drawInfo.cBody;
                DrawData drawData4 = drawData3;
                drawInfo.DrawDataCache.Add(drawData4);
            }
        }

        if ((p.dye[2].type == ItemID.YellowDye || p.dye[2].type == ItemID.BrightYellowDye) && p.legs == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "SonicShoes", EquipType.Legs))
        {
            var value = new DrawData(Mod.Assets.Request<Texture2D>("Items/Vanity/SuperSonicShoes_Legs").Value,
                new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (p.legFrame.Width / 2) + (p.width / 2)),
                    (int)(drawInfo.Position.Y - Main.screenPosition.Y + p.height - p.legFrame.Height + 4f)) +
                p.legPosition + vector2, p.legFrame, Color.White, p.legRotation, vector2, 1f, spriteEffects, 0);
            //value.shader = drawInfo.cLegs;
            drawInfo.DrawDataCache.Add(value);
        }
    }

    private static Vector2 GetCompositeOffset_BackArm(ref PlayerDrawSet drawinfo) => new Vector2(
        6 * (!drawinfo.playerEffect.HasFlag((SpriteEffects)1) ? 1 : -1),
        (float)(2 * (!drawinfo.playerEffect.HasFlag((Enum)(object)(SpriteEffects)2) ? 1 : -1)));

    private static Vector2 GetCompositeOffset_FrontArm(ref PlayerDrawSet drawinfo) =>
        new(-5 * (!drawinfo.playerEffect.HasFlag((SpriteEffects)1) ? 1 : -1), 0f);
}
