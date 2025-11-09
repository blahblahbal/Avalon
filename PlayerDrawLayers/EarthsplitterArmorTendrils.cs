using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers;

public class EarthsplitterArmorTendrils : PlayerDrawLayer
{
	public override bool IsHeadLayer => true;

	public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.BackAcc);

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		if (drawInfo.shadow != 0f)
		{
			return;
		}
		Player p = drawInfo.drawPlayer;
		if (p.body == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "EarthsplitterChestpiece", EquipType.Body))
		{
			Vector2 value6 = new(-4 * p.direction, 8);
			Vector2 vec4 = drawInfo.Position - Main.screenPosition + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.width / 2, drawInfo.drawPlayer.height - drawInfo.drawPlayer.bodyFrame.Height / 2) + new Vector2(0f, -4f) + value6;
			vec4 = vec4.Floor();
			DrawData item = new(Mod.Assets.Request<Texture2D>("Items/Armor/Hardmode/EarthsplitterChestpiece_Back").Value, vec4, drawInfo.drawPlayer.bodyFrame, drawInfo.colorArmorBody, drawInfo.drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0)
			{
				shader = drawInfo.cBody
			};
			drawInfo.DrawDataCache.Add(item);
		}
	}
}
