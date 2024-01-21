using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;

public class FleshArmorTendrilsBack : PlayerDrawLayer
{
    public override bool IsHeadLayer => false;

    public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.BackAcc);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Player p = drawInfo.drawPlayer;
        if (drawInfo.shadow != 0f || p.back > 0 || p.inventory[p.selectedItem].type is ItemID.HeatRay or ItemID.LeafBlower)
        {
            return;
        }
        if (p.body == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "FleshWrappings", EquipType.Body))
        {
            Vector2 value6 = new Vector2(0, 8);
            Vector2 vec4 = drawInfo.Position - Main.screenPosition + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.width / 2, drawInfo.drawPlayer.height - drawInfo.drawPlayer.bodyFrame.Height / 2) + new Vector2(0f, -4f) + value6;
            vec4 = vec4.Floor();
            DrawData item = new DrawData(Mod.Assets.Request<Texture2D>("Items/Armor/Hardmode/FleshWrappings_Tendril_Back").Value, vec4, drawInfo.drawPlayer.bodyFrame, drawInfo.colorArmorBody, drawInfo.drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
            item.shader = drawInfo.cBody;
            drawInfo.DrawDataCache.Add(item);
        }
    }
}

public class FleshArmorTendrilsFront : PlayerDrawLayer
{
    public override bool IsHeadLayer => false;

    public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.FrontAccFront);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Player p = drawInfo.drawPlayer;
        if (drawInfo.shadow != 0f || drawInfo.drawPlayer.front > 0 || p.inventory[p.selectedItem].type is ItemID.HeatRay or ItemID.LeafBlower)
        {
            return;
        }
        if (p.body == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "FleshWrappings", EquipType.Body))
        {
            Vector2 value6 = new Vector2(0, 8);
            Vector2 vec4 = drawInfo.Position - Main.screenPosition + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.width / 2, drawInfo.drawPlayer.height - drawInfo.drawPlayer.bodyFrame.Height / 2) + new Vector2(0f, -4f) + value6;
            vec4 = vec4.Floor();
            DrawData item = new DrawData(Mod.Assets.Request<Texture2D>("Items/Armor/Hardmode/FleshWrappings_Tendril_Front").Value, vec4, drawInfo.drawPlayer.bodyFrame, drawInfo.colorArmorBody, drawInfo.drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
            item.shader = drawInfo.cBody;
            drawInfo.DrawDataCache.Add(item);
        }
    }
}
