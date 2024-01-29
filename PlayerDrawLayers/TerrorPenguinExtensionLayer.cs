using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;

internal class TerrorPenguinExtensionLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new BeforeParent(Terraria.DataStructures.PlayerDrawLayers.ArmorLongCoat);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0f)
        {
            return;
        }

        Player p = drawInfo.drawPlayer;
        if (p.body == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "TerrorPenguinsOnepiece", EquipType.Body) &&
            p.legs == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "TerrorPenguinsHeels", EquipType.Legs))
        {
            Texture2D tex = Mod.Assets.Request<Texture2D>("Items/Vanity/TerrorPenguinsOnepiece_Extension_Legs").Value;
            if (drawInfo.isSitting)
            {
                DrawSittingLongCoat(ref drawInfo, tex, drawInfo.colorArmorBody, drawInfo.cBody);
                return;
            }
            DrawData item = new DrawData(tex,
                new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (drawInfo.drawPlayer.legFrame.Width / 2) + drawInfo.drawPlayer.width / 2),
                (int)(drawInfo.Position.Y - Main.screenPosition.Y + drawInfo.drawPlayer.height - drawInfo.drawPlayer.legFrame.Height + 4f)) +
                    drawInfo.drawPlayer.legPosition + drawInfo.legVect, drawInfo.drawPlayer.legFrame, drawInfo.colorArmorBody,
                drawInfo.drawPlayer.legRotation, drawInfo.legVect, 1f, drawInfo.playerEffect);
            item.shader = drawInfo.cBody;
            drawInfo.DrawDataCache.Add(item);
        }
    }
    private static void DrawSittingLongCoat(ref PlayerDrawSet drawinfo, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0)
    {
        Vector2 legsOffset = drawinfo.legsOffset;
        Vector2 position = new Vector2((int)(drawinfo.Position.X - Main.screenPosition.X - drawinfo.drawPlayer.legFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (int)(drawinfo.Position.Y - Main.screenPosition.Y + drawinfo.drawPlayer.height - drawinfo.drawPlayer.legFrame.Height + 4f)) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
        Rectangle legFrame = drawinfo.drawPlayer.legFrame;
        position += legsOffset;
        position.X += 2 * drawinfo.drawPlayer.direction;
        legFrame.Y = legFrame.Height * 5;
        DrawData item = new DrawData(textureToDraw, position, legFrame, matchingColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect);
        item.shader = shaderIndex;
        drawinfo.DrawDataCache.Add(item);
    }
}
