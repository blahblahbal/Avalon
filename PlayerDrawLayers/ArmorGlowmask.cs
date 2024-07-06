using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Avalon.Items.Armor.PreHardmode;
using Avalon.Items.Armor.Hardmode;
using Terraria.ID;
using ReLogic.Content;

namespace Avalon.PlayerDrawLayers;

/// <summary>
/// Adapted from lion8cake's depths same thing very cool.
/// </summary>
public class ArmorGlowmask : GlobalItem
{
    //public int glowOffsetX;
    //public int glowOffsetY;
    public Texture2D glowTexture = Asset<Texture2D>.DefaultValue;
    public int glowAlpha = 255;

    public override bool InstancePerEntity => true;
}
public class HeadGlowmask : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Head);
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		if (drawInfo.drawPlayer.dead)
		{
			return;
		}

        Terraria.Item slot;

        if (drawPlayer.armor[10].type == ItemID.None && drawPlayer.armor[0].type != ItemID.None)
        {
            slot = drawPlayer.armor[0];
        }
        else if (drawPlayer.armor[10].type != ItemID.None)
        {
            slot = drawPlayer.armor[10];
        }
        else
        {
            return;
        }

        if (drawPlayer.head != slot.headSlot)
        {
            return;
        }
        Color color = drawPlayer.GetImmuneAlphaPure(new Color(255, 255, 255, slot.GetGlobalItem<ArmorGlowmask>().glowAlpha) * drawPlayer.stealth, drawInfo.shadow);

        Texture2D texture = slot.GetGlobalItem<ArmorGlowmask>().glowTexture;

        Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
        Vector2 headVect = drawInfo.headVect;
        DrawData drawData = new DrawData(texture, drawPos.Floor() + headVect, drawPlayer.bodyFrame, color, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0)
        {
            shader = drawInfo.cHead
        };
        drawInfo.DrawDataCache.Add(drawData);
    }
}

public class BodyGlowmask : PlayerDrawLayer
{
    public override Position GetDefaultPosition()
    {
        return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Torso);
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Player drawPlayer = drawInfo.drawPlayer;
        if (drawInfo.drawPlayer.dead)
        {
            return;
        }

        Terraria.Item slot;

        if (drawPlayer.armor[11].type == ItemID.None && drawPlayer.armor[1].type != ItemID.None)
        {
            slot = drawPlayer.armor[1];
        }
        else if (drawPlayer.armor[11].type != ItemID.None)
        {
            slot = drawPlayer.armor[11];
        }
        else
        {
            return;
        }

        if (drawPlayer.body != slot.bodySlot)
        {
            return;
        }
        Color color = drawPlayer.GetImmuneAlphaPure(new Color(255, 255, 255, slot.GetGlobalItem<ArmorGlowmask>().glowAlpha) * drawPlayer.stealth, drawInfo.shadow);

        Texture2D texture = slot.GetGlobalItem<ArmorGlowmask>().glowTexture;

        float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
        float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
        Vector2 origin = drawInfo.bodyVect;
        Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

        Rectangle frame = drawInfo.compTorsoFrame;
        int walkFrame = drawPlayer.bodyFrame.Y / 56;
        if (walkFrame == 7 || walkFrame == 8 || walkFrame == 9 || walkFrame == 14 || walkFrame == 15 || walkFrame == 16)
        {
            frame.Y += 2; //walking bop
        }

        float rotation = drawPlayer.bodyRotation;
        SpriteEffects spriteEffects = drawInfo.playerEffect;

        DrawData drawData = new(texture, position, frame, color, rotation, origin, 1f, spriteEffects, 0);
        drawData.shader = drawInfo.cBody;
        drawInfo.DrawDataCache.Add(drawData);
    }
}
public class A_ShoulderGlowmask : PlayerDrawLayer
{ //Shoulder Drawing
	public override Position GetDefaultPosition()
	{
		return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.ArmOverItem);
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		if (drawInfo.drawPlayer.dead)
		{
			return;
		}

		Terraria.Item slot;

		if (drawPlayer.armor[11].type == ItemID.None && drawPlayer.armor[1].type != ItemID.None)
		{
			slot = drawPlayer.armor[1];
		}
		else if (drawPlayer.armor[11].type != ItemID.None)
		{
			slot = drawPlayer.armor[11];
		}
		else
		{
			return;
		}

		if (drawPlayer.body != slot.bodySlot)
		{
			return;
		}
		Color color = drawPlayer.GetImmuneAlphaPure(new Color(255, 255, 255, slot.GetGlobalItem<ArmorGlowmask>().glowAlpha) * drawPlayer.stealth, drawInfo.shadow);

		Texture2D texture = slot.GetGlobalItem<ArmorGlowmask>().glowTexture;

		float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
		float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
		Vector2 origin = drawInfo.bodyVect;
		Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

		Rectangle frame = drawInfo.compBackShoulderFrame;
		int walkFrame = drawPlayer.bodyFrame.Y / 56;
		if (walkFrame == 7 || walkFrame == 8 || walkFrame == 9 || walkFrame == 14 || walkFrame == 15 || walkFrame == 16)
		{
			frame.Y += 2; //walking bop
		}

		float rotation = drawPlayer.bodyRotation;
		SpriteEffects spriteEffects = drawInfo.playerEffect;

		DrawData drawData = new(texture, position, frame, color, rotation, origin, 1f, spriteEffects, 0);
		drawData.shader = drawInfo.cBody;
		drawInfo.DrawDataCache.Add(drawData);
	}
}

public class Z_ArmGlowmask : PlayerDrawLayer
{ //Arm Drawing
	public override Position GetDefaultPosition()
	{
		return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.ArmOverItem);
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		if (drawInfo.drawPlayer.dead)
		{
			return;
		}

		Terraria.Item slot;

		if (drawPlayer.armor[11].type == ItemID.None && drawPlayer.armor[1].type != ItemID.None)
		{
			slot = drawPlayer.armor[1];
		}
		else if (drawPlayer.armor[11].type != ItemID.None)
		{
			slot = drawPlayer.armor[11];
		}
		else
		{
			return;
		}

		if (drawPlayer.body != slot.bodySlot)
		{
			return;
		}
		Color color = drawPlayer.GetImmuneAlphaPure(new Color(255, 255, 255, slot.GetGlobalItem<ArmorGlowmask>().glowAlpha) * drawPlayer.stealth, drawInfo.shadow);

		Texture2D texture = slot.GetGlobalItem<ArmorGlowmask>().glowTexture;

		float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
		float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
		Vector2 origin = drawInfo.bodyVect;
		Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
		float rotation = drawPlayer.bodyRotation;
		SpriteEffects spriteEffects = drawInfo.playerEffect;
		if (drawPlayer.compositeFrontArm.enabled)
		{
			if (drawInfo.compFrontArmFrame.X / drawInfo.compFrontArmFrame.Width >= 7)
			{
				position += new Vector2((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : (-1), (!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically)) ? 1 : (-1));
			}
			Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawPlayer.bodyFrame.Y / drawPlayer.bodyFrame.Height];
			vector2.Y -= 2f;
			position += vector2 * -drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt();
			Vector2 offset = new Vector2(-5 * ((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : (-1)), 0f);
			origin += offset;
			position += offset + drawInfo.frontShoulderOffset;
			rotation = drawPlayer.bodyRotation + drawInfo.compositeFrontArmRotation;

			DrawData drawData = new(texture, position, drawInfo.compFrontArmFrame, color, rotation, origin, 1f, spriteEffects, 0);
			drawData.shader = drawInfo.cBody;
			drawInfo.DrawDataCache.Add(drawData);
		}
		else
		{
			Rectangle frame = drawInfo.compFrontArmFrame;
			int walkFrame = drawPlayer.bodyFrame.Y / 56;
			if (walkFrame == 7 || walkFrame == 8 || walkFrame == 9 || walkFrame == 14 || walkFrame == 15 || walkFrame == 16)
			{
				frame.Y += 2; //walking bop
			}
			DrawData drawData = new(texture, position, frame, color, rotation, origin, 1f, spriteEffects, 0);
			drawData.shader = drawInfo.cBody;
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}

public class Z_BackArmGlowmask : PlayerDrawLayer
{ //Back Arm Drawing
	public override Position GetDefaultPosition()
	{
		return new BeforeParent(Terraria.DataStructures.PlayerDrawLayers.OffhandAcc); //there's like, no other fitting layer lol
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		if (drawInfo.drawPlayer.dead)
		{
			return;
		}

		Terraria.Item slot;

		if (drawPlayer.armor[11].type == ItemID.None && drawPlayer.armor[1].type != ItemID.None)
		{
			slot = drawPlayer.armor[1];
		}
		else if (drawPlayer.armor[11].type != ItemID.None)
		{
			slot = drawPlayer.armor[11];
		}
		else
		{
			return;
		}

		if (drawPlayer.body != slot.bodySlot)
		{
			return;
		}
		Color color = drawPlayer.GetImmuneAlphaPure(new Color(255, 255, 255, slot.GetGlobalItem<ArmorGlowmask>().glowAlpha) * drawPlayer.stealth, drawInfo.shadow);

		Texture2D texture = slot.GetGlobalItem<ArmorGlowmask>().glowTexture;

		float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
		float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
		Vector2 origin = drawInfo.bodyVect;
		Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
		float rotation = drawPlayer.bodyRotation;
		SpriteEffects spriteEffects = drawInfo.playerEffect;
		if (drawPlayer.compositeBackArm.enabled)
		{
			Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawPlayer.bodyFrame.Y / drawPlayer.bodyFrame.Height];
			vector2.Y -= 2f;
			position += vector2 * -drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt();
			Vector2 offset = new Vector2(6 * ((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : (-1)), 2 * ((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically)) ? 1 : (-1)));
			origin += offset;
			position += offset + drawInfo.backShoulderOffset;
			rotation = drawPlayer.bodyRotation + drawInfo.compositeBackArmRotation;

			DrawData drawData = new(texture, position, drawInfo.compBackArmFrame, color, rotation, origin, 1f, spriteEffects, 0);
			drawData.shader = drawInfo.cBody;
			drawInfo.DrawDataCache.Add(drawData);
		}
		else
		{
			Rectangle frame = drawInfo.compBackArmFrame;
			int walkFrame = drawPlayer.bodyFrame.Y / 56;
			if (walkFrame == 7 || walkFrame == 8 || walkFrame == 9 || walkFrame == 14 || walkFrame == 15 || walkFrame == 16)
			{
				frame.Y += 2; //walking bop
			}
			DrawData drawData = new(texture, position, frame, color, rotation, origin, 1f, spriteEffects, 0);
			drawData.shader = drawInfo.cBody;
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}

public class LegsGlowmask : PlayerDrawLayer
{
    public override Position GetDefaultPosition()
    {
        return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Leggings);
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Player drawPlayer = drawInfo.drawPlayer;
        if (drawInfo.drawPlayer.dead)
        {
            return;
        }

        Terraria.Item slot;

        if (drawPlayer.armor[12].type == ItemID.None && drawPlayer.armor[2].type != ItemID.None)
        {
            slot = drawPlayer.armor[2];
        }
        else if (drawPlayer.armor[12].type != ItemID.None)
        {
            slot = drawPlayer.armor[12];
        }
        else
        {
            return;
        }

        if (drawPlayer.legs != slot.legSlot)
        {
            return;
        }
        Color color = drawPlayer.GetImmuneAlphaPure(new Color(255, 255, 255, slot.GetGlobalItem<ArmorGlowmask>().glowAlpha) * drawPlayer.stealth, drawInfo.shadow);

        Texture2D texture = slot.GetGlobalItem<ArmorGlowmask>().glowTexture;

        Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.legFrame.Width / 2, drawPlayer.height - drawPlayer.legFrame.Height + 4f) + drawPlayer.legPosition;
        Vector2 legsOffset = drawInfo.legsOffset;
        DrawData drawData = new DrawData(texture, drawPos.Floor() + legsOffset, drawPlayer.legFrame, color, drawPlayer.legRotation, legsOffset, 1f, drawInfo.playerEffect, 0)
        {
            shader = drawInfo.cLegs
        };
        drawInfo.DrawDataCache.Add(drawData);
    }
}
