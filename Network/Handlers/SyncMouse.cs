using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network.Handlers;
public class SyncMouse
{
	public static void SendPacket(Vector2 mousePosition, int playerIndex, int toClient = -1, int ignoreClient = -1)
	{
		ModPacket message = MessageHandler.GetPacket(MessageID.SyncMouse);
		message.WriteVector2(mousePosition);
		message.Write(playerIndex);
		message.Send(toClient, ignoreClient);
	}

	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		Vector2 position = reader.ReadVector2();
		int whoAmI = reader.ReadInt32();
		// If server recieved a message, forward this to all clients, ignoring the sender
		if (Main.netMode == NetmodeID.Server)
		{
			SendPacket(position, whoAmI, -1, fromWho);
		}
		else
		{
			Player player = Main.player[whoAmI];
			AvalonPlayer modPlayer = player.GetModPlayer<AvalonPlayer>();
			modPlayer.MousePosition = position;
		}
	}
}

public class SyncMouseDetour : ModHook
{
	protected override void Apply()
	{
		On_PlayerInput.SetZoom_MouseInWorld += On_PlayerInput_SetZoom_MouseInWorld;
	}

	private void On_PlayerInput_SetZoom_MouseInWorld(On_PlayerInput.orig_SetZoom_MouseInWorld orig)
	{
		orig();
		if (Main.LocalPlayer.active)
		{
			Vector2 pos = Main.MouseWorld;
			Main.LocalPlayer.GetModPlayer<AvalonPlayer>().MousePosition = pos;
			SyncMouse.SendPacket(pos, Main.myPlayer, ignoreClient: Main.myPlayer);
		}
	}
}
