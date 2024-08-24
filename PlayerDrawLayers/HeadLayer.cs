using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers
{
	//Items manually register data which this layer is using
	public sealed class HeadLayer : PlayerDrawLayer
	{
		private static Dictionary<int, DrawLayerData> HeadLayerData { get; set; }

		/// <summary>
		/// Add data associated with the head equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
		/// <para>Don't forget the !Main.dedServ check!</para>
		/// </summary>
		/// <param name="headSlot">Head equip slot</param>
		/// <param name="data">Data</param>
		public static void RegisterData(int headSlot, DrawLayerData data)
		{
			if (!HeadLayerData.ContainsKey(headSlot))
			{
				HeadLayerData.Add(headSlot, data);
			}
		}

		public override void Load()
		{
			HeadLayerData = new Dictionary<int, DrawLayerData>();
		}

		public override void Unload()
		{
			HeadLayerData = null;
		}

		public override Position GetDefaultPosition()
		{
			return new AfterParent(Terraria.DataStructures.PlayerDrawLayers.Head);
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (drawPlayer.dead || drawPlayer.invis || drawPlayer.head == -1)
			{
				return false;
			}

			return true;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			if (!HeadLayerData.TryGetValue(drawPlayer.head, out DrawLayerData data))
			{
				return;
			}

			Color color = drawPlayer.GetImmuneAlphaPure(data.Color(drawInfo), drawInfo.shadow);

			Texture2D texture = data.Texture.Value;
			Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
			Vector2 headVect = drawInfo.headVect;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + headVect, drawPlayer.bodyFrame, color, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0)
			{
				shader = drawInfo.cHead
			};
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}
