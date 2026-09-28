using Avalon.Common.Interfaces;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Common;

public class AvalonGlobalNPCInstance : GlobalNPC
{
    public override bool InstancePerEntity => true;

	/// <summary>
	/// Use the NPC's ChangeSpeed() method instead of changing this manually
	/// </summary>
	public float Speed = 1;
	public float DebuffDuration = 1f;
	public float[] SpeedUpdateCount = new float[2];
	public bool AstigSpawned { get; set; }
    public int LacerateStacks { get; set; } = 1;
    public bool Lacerated { get; set; }
    public bool IsLaceratedHM { get; set; }
    public int BreathCd { get; set; } = 45;
    public bool DlBreath { get; set; }
    public bool Electrified { get; set; }
    public bool Frozen { get; set; }
    public bool InfernaSpawned { get; set; }
    public bool JugRunOnce { get; set; }
    public bool LavaWalk { get; set; }
    public bool Malaria { get; set; }
    public bool NecroticDrain { get; set; }
    public bool NoOneHitKill { get; set; }
    public int ORebirth { get; set; }
    public bool Silenced { get; set; }
    public int SlimeHitCounter { get; set; }
    public bool Slowed { get; set; }
    public int SpikeTimer { get; set; }
    public bool Virulent { get; set; }
    public bool Inferno { get; set; }
    public bool Pathogen { get; set; }
    public bool ShowStats { get; set; }
    public bool Wormed { get; set; }
    public bool BacterialInfection { get; set; }
    public bool CanDamageMobs { get; set; }
    public int DamageMobsTimer { get; set; }
	public bool SkellyBanana {  get; set; }
	public bool Dissolving {  get; set; }

	public override void Load()
	{
		On_NPC.AI += On_NPC_AI;
		On_NPC.FindFrame += On_NPC_FindFrame;
		On_NPC.UpdateCollision += On_NPC_UpdateCollision;
		On_NPC.UpdateNPC_BuffSetFlags += On_NPC_UpdateNPC_BuffSetFlags;
	}

	private void On_NPC_UpdateNPC_BuffSetFlags(On_NPC.orig_UpdateNPC_BuffSetFlags orig, NPC npc, bool lowerBuffTime)
	{
		for (int i = 0; i < npc.buffTime.Length; i++)
		{
			if (npc.buffTime[i] > 0 && npc.buffType[i] > 0)
			{
				ModBuff b = BuffLoader.GetBuff(npc.buffType[i]);
				if (b is IBuffThatNeedsToUpdateNPCEarly iB)
					iB.UpdateEarly(npc, ref i);
			}
		}
		orig(npc, lowerBuffTime);
	}

	private void On_NPC_UpdateCollision(On_NPC.orig_UpdateCollision orig, NPC self)
	{
		var gNPC = self.GetGlobalNPC<AvalonGlobalNPCInstance>();
		if (self.noGravity)
		{
			self.velocity *= gNPC.Speed;
			orig(self);
			self.velocity /= gNPC.Speed;
		}
		else
		{
			self.velocity.X *= gNPC.Speed;
			orig(self);
			self.velocity.X /= gNPC.Speed;
		}
	}
	private void On_NPC_FindFrame(On_NPC.orig_FindFrame orig, NPC self)
	{
		if (self.IsABestiaryIconDummy)
		{
			orig(self);
			return;
		}
		var gNPC = self.GetGlobalNPC<AvalonGlobalNPCInstance>();
		gNPC.SpeedUpdateCount[1] += gNPC.Speed;
		while (gNPC.SpeedUpdateCount[1] >= 1)
		{
			gNPC.SpeedUpdateCount[1]--;
			orig(self);
		}
	}

	private void On_NPC_AI(On_NPC.orig_AI orig, NPC self)
	{
		var gNPC = self.GetGlobalNPC<AvalonGlobalNPCInstance>();
		//self.GravityMultiplier *= gNPC.Speed;
		//self.MaxFallSpeedMultiplier *= gNPC.Speed;
		gNPC.SpeedUpdateCount[0] += gNPC.Speed;
		while (gNPC.SpeedUpdateCount[0] >= 1)
		{
			gNPC.SpeedUpdateCount[0]--;
			orig(self);
		}
		if (self.noTileCollide)
		{
			self.position += self.velocity * (gNPC.Speed - 1f);
		}
	}
	public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		for (int i = 0; i < SpeedUpdateCount.Length; i++)
		{
			binaryWriter.Write(SpeedUpdateCount[i]);
		}
	}
	public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
	{
		for (int i = 0; i < SpeedUpdateCount.Length; i++)
		{
			SpeedUpdateCount[i] = binaryReader.ReadSingle();
		}
	}
	public override void ResetEffects(NPC npc)
    {
		Speed = 1;
		DebuffDuration = 1f;
        NecroticDrain = false;
        Malaria = false;
        Electrified = false;
        Lacerated = false;
        Virulent = false;
        Inferno = false;
        Pathogen = false;
        Wormed = false;
		Dissolving = false;
        BacterialInfection = false;
        //BleedStacks = 1;
    }
    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (Pathogen)
        {
            drawColor.G = (byte)MathHelper.Clamp(drawColor.G - 76,0,255);
            drawColor.R = (byte)MathHelper.Clamp(drawColor.R - 25,0,255);
        }
		if (Dissolving)
		{
			drawColor.G = 255;
		}
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (npc.HasBuff(BuffID.Electrified))
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= 8;
            if (npc.velocity != Vector2.Zero)
                npc.lifeRegen -= 40;
            if (damage < 4)
            {
                damage = 4;
            }
        }
        if (BacterialInfection)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 24;
            if (damage < 5)
            {
                damage = 5;
            }
        }
		if (Dissolving)
		{
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}

			npc.lifeRegen -= 12;
			if (damage < 3)
			{
				damage = 3;
			}
		}
		if (Wormed)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 16;
            if (damage < 4)
            {
                damage = 4;
            }
        }
        if (Malaria)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 6;
            if (damage < 3)
            {
                damage = 3;
            }
        }
        if (Inferno)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 40;
            if (damage < 6)
            {
                damage = 6;
            }
        }
        if (NecroticDrain)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 90;
            if (damage < 3)
            {
                damage = 3;
            }
        }
        if (Electrified)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            if (npc.velocity != Vector2.Zero)
                npc.lifeRegen -= 30;
            npc.lifeRegen -= 40;
            if (damage < 6)
            {
                damage = 6;
            }
        }
        if (Lacerated)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            int mult = 4;
            if (IsLaceratedHM)
            {
                mult = 6;
            }
            npc.lifeRegen -= mult * LacerateStacks * 2;
            if (damage < LacerateStacks * 2)
            {
                damage = LacerateStacks * 2;
            }
        }
        if (Virulent)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 100;
            if (damage < 10)
            {
                damage = 10;
            }
        }
    }
}
