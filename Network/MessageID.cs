namespace Avalon.Network;

public enum MessageID
{
	ShadowTeleport = 0,
	//SyncMouse = 1, // deprecated, this shit flooded the network way too much, sync mouse pos manually where needed
	SyncTime = 2,
	SyncWiring = 3,
	StaminaHeal = 4,
	SyncLockUnlock = 5,
	SyncSkyBlessing = 6,
	SyncParticle = 7,
	SyncOnHit = 8,
	SyncLongbowArrowEffect = 9,
	SyncPhantasmSpawn = 10
}
