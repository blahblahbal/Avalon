using Avalon.Common.Templates;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public class SyncLongbowArrowEffect
{
	public static void SendPacket(int LongbowType, int BowOwner, int ProjectileIdentity, float Power, byte variant = 0)
	{
		if (Main.netMode == NetmodeID.SinglePlayer)
		{
			return; // no sending packets in singleplayer :D
		}
		ModPacket packet = MessageHandler.GetPacket(MessageID.SyncLongbowArrowEffect);
		packet.Write((byte)BowOwner);
		packet.Write((short)LongbowType);
		packet.Write((short)ProjectileIdentity);
		packet.Write(Power);
		packet.Write(variant);
		packet.Send(ignoreClient: BowOwner);
	}
	public static void HandlePacket(BinaryReader reader, int fromWho)
	{
		byte Owner = reader.ReadByte();
		short LongbowType = reader.ReadInt16();
		short projIdentity = reader.ReadInt16();
		float Power = reader.ReadSingle();
		byte Variant = reader.ReadByte();
		Projectile p = Main.projectile.FirstOrDefault(x => x.identity == projIdentity && x.owner == Owner);
		if (Main.myPlayer == Owner)
			return;
		if (Main.netMode == NetmodeID.Server)
		{
			SendPacket(LongbowType, Owner, projIdentity,Power, Variant);
		}
		else if (ContentSamples.ProjectilesByType[LongbowType].ModProjectile is LongbowTemplate bow)
		{
			bow.ApplyArrowEffect(p,Power,Variant);
		}
	}
}
