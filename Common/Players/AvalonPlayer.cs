using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Other;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Tools.Superhardmode;
using Avalon.NPCs.Bosses.PreHardmode;
using Avalon.Prefixes;
using Avalon.Projectiles;
using Avalon.Projectiles.Tools;
using Avalon.Systems;
using Avalon.Tiles.Ores;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Common.Players;

public class AvalonPlayer : ModPlayer
{
    #region solar system vars
    public LinkedList<int>[] Planets { get; } = new LinkedList<int>[9]
    {
        new(),
        new(),
        new(),
        new(),
        new(),
        new(),
        new(),
        new(),
        new()
    };
    public LinkedListNode<int> HandlePlanets(int index)
    {
        return Planets[index].AddLast(Planets[index].Count);
    }
    public float[] PlanetRotation { get; set; } = new float[9];

    public enum Planet
    {
        Mercury = 0,
        Venus = 1,
        Earth = 2,
        Mars = 3,
        Jupiter = 4,
        Saturn = 5,
        Uranus = 6,
        Neptune = 7
    }
    #endregion

    #region reflector
    public LinkedList<int> Reflectors { get; } = new();
    public float ReflectorStaffRotation { get; set; }
    public LinkedListNode<int> HandleReflectorSummon() => Reflectors.AddLast(Reflectors.Count);
    public void RemoveReflectorSummon(LinkedListNode<int> linkedListNode)
    {
        LinkedListNode<int> nextNode = linkedListNode.Next;
        while (nextNode != null)
        {
            nextNode.Value--;
            nextNode = nextNode.Next;
        }

        Reflectors.Remove(linkedListNode);
    }
    public LinkedListNode<int> ObtainExistingReflectorSummon(int index)
    {
        int diff = index + 1 - Reflectors.Count;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                Reflectors.AddLast(Reflectors.Count);
            }

            return Reflectors.Last;
        }

        return Reflectors.Find(index);
    }
    #endregion

    public bool PotionSicknessSoundPlayed;

    public byte FartTimer = 0;
    public int RocketDustTimer = 0;
    public bool DarkMatterMonolith { get; set; }
    public int DarkMatterTimeOut = 20;

    public bool InBossFight;

    public Vector2 MousePosition;
    public float MagicCritDamage = 1f;
    public float MeleeCritDamage = 1f;
    public float RangedCritDamage = 1f;
    public float CritDamageMult = 1f;

    public bool AdjShimmer;
    public bool oldAdjShimmer;
    private int gemCount;
    public bool[] OwnedLargeGems = new bool[10];

    public bool shadowTele;
    public bool TeleportV;
    public bool tpStam = true;
    public int tpCD;
    public bool teleportVWasTriggered;
    public bool BadgeOfBacteria = false;
    public bool BacterialEndurance;

    public bool DisplayStats;

    /// <summary>
    /// Magic critical strike chance cap. Subtract from it to use.
    /// </summary>
    public int MaxMagicCrit;
    /// <summary>
    /// Melee critical strike chance cap. Subtract from it to use.
    /// </summary>
    public int MaxMeleeCrit;
    /// <summary>
    /// Ranged critical strike chance cap. Subtract from it to use.
    /// </summary>
    public int MaxRangedCrit;

    public List<int> revertHeads = new List<int>();

    #region armor sets
    public bool SkyBlessing;
    public int SkyStacks = 1;
    public bool OreDupe;
    public bool HookBonus;
    public bool AuraThorns;
    public bool DefDebuff;
    public bool HyperMelee;
    public bool HyperMagic;
    public bool HyperRanged;
    public int HyperBar;
    public bool AmmoCost85;
    public bool FleshArmor;
    public bool ZombieArmor;

    public bool AncientLessCost;
    public bool AncientGunslinger;
    public bool AncientMinionGuide;
    public bool AncientSandVortex;
    public bool AncientRangedBonus;
    public bool AncientRangedBonusActive;

    public bool OblivionKill;
    public bool BlahArmor;
    public bool DoubleDamage;
    public bool GoBerserk;
    public bool SpectrumSpeed;
    public bool RoseMagic;
    public int RoseMagicCooldown;
    public bool SplitProj;
    #endregion

    public bool CougherMask;
    private int[] doubleTapTimer = new int[3];

    #region accessories
    public bool PulseCharm;
    public bool ShadowCharm;
    public bool CloudGlove;
    public float BonusKB = 1f;
    public bool AncientHeadphones;
    public bool SixSidedDie;
    public bool LoadedDie;
    public bool CrystalEdge;
    public bool EyeoftheGods;
    public bool TrapImmune;
    public bool BenevolentWard;
    public int WardCD;
    public bool HeartGolem;
    public bool EtherealHeart;
    public bool BloodyWhetstone;
    public bool FrostGauntlet;
    public bool SlimeBand;
    public bool NoSticky;
    public bool VampireTeeth;
    public bool ObsidianGlove;
    public bool RiftGoggles;
    public bool InertiaBoots;
    public bool HideVarefolk;
    public const string LavaMermanName = "LavaMerman";
    public bool AccLavaMerman;
    public bool lavaMerman;
    public bool ForceVarefolk;
    public bool ThePill;
    public bool PocketBench;
    public bool ChaosCharm;
    public bool Reflex;
    public bool ReflexShield;
    public bool UndeadImmune;
    public bool CobShield;
    public bool PallShield;
    public bool DuraShield;
    public bool CobOmegaShield;
    public bool PallOmegaShield;
    public bool DuraOmegaShield;
    public bool DesertGamblerVisible;
    public bool ShadowRing;
    public bool BlahWings;
    public int bubbleCD;
    public bool BubbleBoost;
    public bool CrystalSkull;
    public bool DesertGambler;
    public bool ForceGambler;
    public bool OilBottle;
    public int OilBottleTimer;
    public bool EarthInsig;
    public bool GoblinToolbelt;
    public bool GoblinAK;
    public bool BuilderBelt;
    #endregion

    #region buffs and debuffs
    public bool BrokenWeaponry;
    public bool Unloaded;
    public bool Lucky;
    public bool Heartsick;
    public bool HeartsickElixir;
    public bool AdvancedBattle;
    public bool AdvancedCalming;
    public int TimeSlowCounter;
    public bool NinjaElixir;
    public bool NinjaPotion;
    public bool Ward;
    public int WardCurseDOT;
    public bool CaesiumPoison;
    public bool PathogenImbue;
    public bool Pathogen;
    public bool BloodCasting;
    public bool Vision;
    public float ForceFieldRotation;
    public bool CoughCooldown;
    public int DeliriumCount;
    public bool Berserk;
    public bool SanguineSacrifice;

    public bool HungryMinion;
    public bool GastroMinion;

    public bool SnotOrb;
    #endregion

    #region prefixes
    public bool GreedyPrefix;
    public bool HoardingPrefix;
    public int EfficiencyPrefix;
    public float FluidicModifier;
    #endregion
    public int FrameCount { get; set; }
    public int ShadowCooldown { get; private set; }
    public int OldFallStart;

    public int DesertBeakSpawnTimer;
    public override void Load()
    {
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/Costumes/LavaMerman_Head", EquipType.Head,
            null, LavaMermanName);
        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/Costumes/LavaMerman_Female_Head", EquipType.Head,
            null, LavaMermanName + "_Female");
        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/Costumes/LavaMerman_Body", EquipType.Body,
            null, LavaMermanName);
        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/Costumes/LavaMerman_Legs", EquipType.Legs,
            null, LavaMermanName);
    }
    public override void ResetEffects()
    {
        EfficiencyPrefix = 0;
        if (DesertBeakSpawnTimer < -1)
        {
            DesertBeakSpawnTimer = -1;
        }

        CoughCooldown = false;
        if (DarkMatterTimeOut-- < 0)
        {
            DarkMatterMonolith = false;
        }
        MagicCritDamage = 0f;
        MeleeCritDamage = 0f;
        RangedCritDamage = 0f;

        // buffs
        AdvancedBattle = false;
        AdvancedCalming = false;
        Lucky = false;
        Heartsick = false;
        NinjaElixir = false;
        NinjaPotion = false;
        HeartsickElixir = false;
        CaesiumPoison = false;
        Pathogen = false;
        PathogenImbue = false;
        HungryMinion = false;
        GastroMinion = false;
        BloodCasting = false;
        Vision = false;
        Berserk = false;
        SanguineSacrifice = false;

        // accessories
        TrapImmune = false;
        PulseCharm = false;
        ShadowCharm = false;
        CritDamageMult = 1f;
        BonusKB = 1f;
        AncientHeadphones = false;
        SixSidedDie = false;
        CrystalEdge = false;
        LoadedDie = false;
        BenevolentWard = false;
        Ward = false;
        HeartGolem = false;
        EtherealHeart = false;
        BloodyWhetstone = false;
        CloudGlove = false;
        BadgeOfBacteria = false;
        BacterialEndurance = false;
        FrostGauntlet = false;
        SlimeBand = false;
        NoSticky = false;
        VampireTeeth = false;
        ObsidianGlove = false;
        RiftGoggles = false;
        InertiaBoots = false;
        HideVarefolk = false;
        AccLavaMerman = false;
        ForceVarefolk = false;
        ThePill = false;
        PocketBench = false;
        ChaosCharm = false;
        Reflex = false;
        ReflexShield = false;
        lavaMerman = false;
        UndeadImmune = false;
        CobShield = false;
        PallShield = false;
        DuraShield = false;
        CobOmegaShield = false;
        PallOmegaShield = false;
        DuraOmegaShield = false;
        ShadowRing = false;
        BlahWings = false;
        BubbleBoost = false;
        CrystalSkull = false;
        DesertGambler = false;
        ForceGambler = false;
        OilBottle = false;
        EarthInsig = false;
        GoblinToolbelt = false;
        GoblinAK = false;
        BuilderBelt = false;

        // armor sets
        SkyBlessing = false;
        OreDupe = false;
        HookBonus = false;
        AuraThorns = false;
        DefDebuff = false;
        HyperMagic = false;
        HyperMelee = false;
        HyperRanged = false;
        AmmoCost85 = false;
        FleshArmor = false;
        ZombieArmor = false;
        AncientLessCost = false;
        AncientGunslinger = false;
        AncientMinionGuide = false;
        AncientSandVortex = false;
        AncientRangedBonus = false;
        OblivionKill = false;
        DoubleDamage = false;
        SpectrumSpeed = false;
        RoseMagic = false;
        SplitProj = false;
        TeleportV = false;

        // prefixes
        GreedyPrefix = false;
        HoardingPrefix = false;
        FluidicModifier = 0f;


        CougherMask = false;

        SnotOrb = false;

        MaxMagicCrit = 100;
        MaxMeleeCrit = 100;
        MaxRangedCrit = 100;

        Player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 = Player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax;

        for (int m = 0; m < 200; m++)
        {
            if (Main.npc[m].active && (Main.npc[m].boss || Main.npc[m].type == NPCID.EaterofWorldsHead || Main.npc[m].type == NPCID.EaterofWorldsBody || Main.npc[m].type == NPCID.EaterofWorldsTail) && Math.Abs(Player.Center.X - Main.npc[m].Center.X) + Math.Abs(Player.Center.Y - Main.npc[m].Center.Y) < 4000f)
            {
                InBossFight = true;
                break;
            }
            else { InBossFight = false; }
        }
    }
    public override void PreUpdateBuffs()
    {
        FrameCount++; // aura potion
        PlanetRotation[0] = (PlanetRotation[0] % MathHelper.TwoPi) + 0.08f;
        PlanetRotation[1] = (PlanetRotation[1] % MathHelper.TwoPi) + 0.09f;
        PlanetRotation[2] = (PlanetRotation[2] % MathHelper.TwoPi) + 0.06f;
        PlanetRotation[3] = (PlanetRotation[3] % MathHelper.TwoPi) + 0.07f;
        PlanetRotation[4] = (PlanetRotation[4] % MathHelper.TwoPi) + 0.05f;
        PlanetRotation[5] = (PlanetRotation[5] % MathHelper.TwoPi) + 0.03f;
        PlanetRotation[6] = (PlanetRotation[6] % MathHelper.TwoPi) + 0.042f;
        PlanetRotation[7] = (PlanetRotation[7] % MathHelper.TwoPi) + 0.05f;
        PlanetRotation[8] = (PlanetRotation[8] % MathHelper.TwoPi) + 0.035f;

        ReflectorStaffRotation = (ReflectorStaffRotation % MathHelper.TwoPi) + 0.01f;
    }
    public override void PostUpdateBuffs()
    {
        if (Player.lifeRegen < 0 && Pathogen)
        {
            Player.lifeRegen = (int)(Player.lifeRegen * 1.5f);
        }
        if(Pathogen && Player.ichor)
        {
            Player.statDefense -= 7;
        }
        if (Pathogen && Player.tipsy)
        {
            Player.statDefense -= 2;
        }
        if(Pathogen && Player.HasBuff(BuffID.Weak))
        {
            Player.statDefense -= 2;
        }
        if (Pathogen && Player.brokenArmor)
        {
            Player.statDefense *= 0.75f;
        }
        if (Pathogen && Player.witheredArmor)
        {
            Player.statDefense *= 0.75f;
        }
        if (Pathogen && Player.slow)
        {
            Player.moveSpeed /= 1.5f;
        }

        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            int PSickness = 0;
            for (int i = 0; i < Player.buffType.Length; i++)
            {
                PSickness = i;
                if (Player.buffType[i] == BuffID.PotionSickness)
                    break;
            }
            if (Player.buffTime[PSickness] == 1 && PotionSicknessSoundPlayed != true)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
                for (int i = 0; i < 5; i++)
                {
                    int num2 = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<LifeRegeneration>(), 0f, 0f, 0, default(Color), Main.rand.Next(20, 26) * 0.1f);
                    Main.dust[num2].noLight = true;
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 0.5f;
                }
                PotionSicknessSoundPlayed = true;
            }
            if (Player.buffTime[PSickness] == 0)
                PotionSicknessSoundPlayed = false;
        }
    }

    public override void PreUpdate()
    {
        if (Player.GetModPlayer<AvalonBiomePlayer>().ZoneSkyFortress)
        {
            float num39 = Main.maxTilesX / 4200;
            num39 *= num39;
            float gravity = (float)((double)(Player.position.Y / 16f - (60f + 10f * num39)) / (Main.worldSurface / 6.0));
            if ((double)gravity < 0.25)
            {
                gravity = 0.25f;
            }
            if (gravity > 1f)
            {
                gravity = 1f;
            }
            Player.gravity /= gravity;
        }

        #region mouseposition
        if (Player.whoAmI == Main.myPlayer)
        {
            MousePosition = Main.MouseWorld;
        }
        #endregion

        tpStam = !TeleportV;
        if (TeleportV)
        {
            TeleportV = false;
            teleportVWasTriggered = true;
        }

        if (Player.whoAmI == Main.myPlayer)
        {
            if (Player.trashItem.type == ModContent.ItemType<LargeZircon>() ||
                Player.trashItem.type == ModContent.ItemType<LargeTourmaline>() ||
                Player.trashItem.type == ModContent.ItemType<LargePeridot>())
            {
                Player.trashItem.SetDefaults();
            }
        }
    }
    public override void PostUpdate()
    {
        OilBottleTimer--;
        if (OilBottleTimer < 0) OilBottleTimer = 0;
        
        if (Player.HasItem(ItemID.LargeRuby) && Player.HasItem(ItemID.LargeAmber) && Player.HasItem(ItemID.LargeTopaz) && Player.HasItem(ModContent.ItemType<LargePeridot>()) &&
            Player.HasItem(ItemID.LargeEmerald) && Player.HasItem(ModContent.ItemType<LargeTourmaline>()) && Player.HasItem(ItemID.LargeSapphire) && Player.HasItem(ItemID.LargeAmethyst) &&
            Player.HasItem(ItemID.LargeDiamond) && Player.HasItem(ModContent.ItemType<LargeZircon>()))
        {
            ExxoAvalonOrigins.Achievements?.Call("Event", "HaveAllLargeGems");
        }
        if (Player.position.Y / 16 - Player.fallStart >= Main.maxTilesY / 3 * 2)
        {
            ExxoAvalonOrigins.Achievements?.Call("Event", "Hellevator");
        }
        DesertBeakSpawnTimer--;
        if (DesertBeakSpawnTimer == 0)
        {
            NPC.SpawnOnPlayer(Player.whoAmI, ModContent.NPCType<DesertBeak>());

            //NPC.NewNPC(Player.GetSource_FromThis(), (int)Player.position.X, (int)Player.position.Y, ModContent.NPCType<DesertBeak>());
            //int db = ClassExtensions.FindATypeOfNPC(ModContent.NPCType<DesertBeak>());
            //if (db > -1)
            //{
            //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(db.ToString()), Color.White);
            //    ModContent.GetInstance<DesertBeak>().leftWing = NPC.NewNPC(Main.npc[db].GetSource_FromThis(), (int)Main.npc[db].position.X, (int)Main.npc[db].position.Y, ModContent.NPCType<DesertBeakWingNPC>(), Start: Player.whoAmI, ai1: db, ai2: 1);
            //    ModContent.GetInstance<DesertBeak>().rightWing = NPC.NewNPC(Main.npc[db].GetSource_FromThis(), (int)Main.npc[db].position.X, (int)Main.npc[db].position.Y, ModContent.NPCType<DesertBeakWingNPC>(), Start: Player.whoAmI, ai1: db, ai2: 2);
            //}
            SoundEngine.PlaySound(SoundID.Roar, Player.position);
        }
        if (AncientRangedBonusActive)
        {
            Player.GetDamage(DamageClass.Ranged) += 0.15f;
            Player.GetCritChance(DamageClass.Ranged) += 10;
            RangedCritDamage += 0.6f;
            MaxRangedCrit -= 15;
            Player.endurance -= 0.075f;
            Player.statDefense /= 2;
        }
        if (InertiaBoots)
        {
            if (Player.velocity.X is > 6f or < -6f)
            {
                int dustType = DustID.Cloud;
                bool superSonic = false;

                if ((Player.dye[0].type == ItemID.YellowDye || Player.dye[0].type == ItemID.BrightYellowDye) && Player.head == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "SonicHat", EquipType.Head) &&
                    (Player.dye[1].type == ItemID.YellowDye || Player.dye[1].type == ItemID.BrightYellowDye) && Player.body == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "SonicShirt", EquipType.Body) &&
                    (Player.dye[2].type == ItemID.YellowDye || Player.dye[2].type == ItemID.BrightYellowDye) && Player.legs == EquipLoader.GetEquipSlot(ExxoAvalonOrigins.Mod, "SonicShoes", EquipType.Legs))
                {
                    superSonic = true;
                    dustType = DustID.HallowedWeapons;
                }
                var newColor = default(Color);
                var num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, dustType, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 50, newColor, 1.55f);
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                Main.dust[num].noGravity = true;
                if (superSonic)
                {
                    var newColor2 = default(Color);
                    var num2 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.GolfPaticle, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 100, newColor2, 0.7f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= Main.rand.NextFloat() * 2;
                }
            }
        }

        if (RocketDustTimer > 0)
        {
            if (Player.velocity.Y < 0)
            {
                for (int x = 0; x < 5; x++)
                {
                    int d = Dust.NewDust(new Vector2(Player.Center.X - 10, Player.position.Y + Player.height), 10, 10,
                        DustID.Smoke);
                    Main.dust[d].position.X += Main.rand.NextFloat();
                    Main.dust[d].velocity.X *= 0.1f;
                    Main.dust[d].velocity.Y += 0.5f;
                }
            }
            RocketDustTimer--;
        }

        if (Player.GetJumpState<FartInAJarJump>().Active)
        {
            FartTimer++;
            if (FartTimer == 1)
            {
                Item fart = Player.HasItemInArmorFindIt(ItemID.FartinaJar);
                if (fart != null)
                {
                    if (fart.prefix == PrefixID.Violent)
                    {
                        if (Player.Male) SoundEngine.PlaySound(in SoundID.PlayerHit, Player.position);
                        else SoundEngine.PlaySound(in SoundID.FemaleHit, Player.position);
                    }
                }
            }
        }
        else FartTimer = 0;

        if (SkyBlessing)
        {
            if (SkyStacks < 10)
            {
                Player.GetDamage(DamageClass.Summon) += 0.04f * SkyStacks;
            }
            else
            {
                Player.GetDamage(DamageClass.Summon) += 0.45f;
            }
        }

        WardCD--;
        if (WardCD < 0) WardCD = 0;

        // Large gem inventory checking
        Player.gemCount = 0;
        gemCount++;
        if (gemCount >= 10)
        {
            Player.gem = -1;
            OwnedLargeGems = new bool[10];
            gemCount = 0;
            for (int num27 = 0; num27 <= 58; num27++)
            {
                if (Player.inventory[num27].type == ItemID.None || Player.inventory[num27].stack == 0)
                {
                    Player.inventory[num27].TurnToAir();
                }

                // Vanilla gems
                if (Player.inventory[num27].type >= ItemID.LargeAmethyst &&
                    Player.inventory[num27].type <= ItemID.LargeDiamond)
                {
                    Player.gem = Player.inventory[num27].type - 1522;
                    OwnedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ItemID.LargeAmber)
                {
                    Player.gem = 6;
                    OwnedLargeGems[Player.gem] = true;
                }
                // Modded gems
                else if (Player.inventory[num27].type == ModContent.ItemType<LargeZircon>())
                {
                    Player.gem = 7;
                    OwnedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ModContent.ItemType<LargeTourmaline>())
                {
                    Player.gem = 8;
                    OwnedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ModContent.ItemType<LargePeridot>())
                {
                    Player.gem = 9;
                    OwnedLargeGems[Player.gem] = true;
                }
            }
        }
        if (!AncientRangedBonus)
        {
            AncientRangedBonusActive = false;
        }
    }
    public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
    {
        if (Player.HasItem(ModContent.ItemType<Items.Potions.Buff.ImmortalityPotion>()) && !Player.HasBuff(ModContent.BuffType<ImmortalityCooldown>()))
        {
            Player.statLife = Player.statLifeMax2 / 4;
            Player.AddBuff(ModContent.BuffType<ImmortalityCooldown>(), 60 * 60 * 5);
            int i = Player.FindItem(ModContent.ItemType<Items.Potions.Buff.ImmortalityPotion>());
            Player.inventory[i].stack--;
            SoundEngine.PlaySound(SoundID.Item3, Player.position);
            if (Player.inventory[i].stack <= 0)
            {
                Player.inventory[i].SetDefaults();
            }
            return false;
        }
        return true;
    }
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        if (Main.myPlayer == Player.whoAmI)
        {
            Player.trashItem.SetDefaults();
            if (Player.difficulty == 0)
            {
                for (int i = 0; i < 59; i++)
                {
                    if (Player.inventory[i].stack > 0 &&
                        (Player.inventory[i].type == ModContent.ItemType<LargeZircon>() ||
                         Player.inventory[i].type == ModContent.ItemType<LargeTourmaline>() ||
                         Player.inventory[i].type == ModContent.ItemType<LargePeridot>()))
                    {
                        int num = Item.NewItem(Player.GetSource_Death(), (int)Player.position.X, (int)Player.position.Y,
                            Player.width, Player.height, Player.inventory[i].type);
                        Main.item[num].netDefaults(Player.inventory[i].netID);
                        Main.item[num].Prefix(Player.inventory[i].prefix);
                        Main.item[num].stack = Player.inventory[i].stack;
                        Main.item[num].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[num].velocity.X = Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[num].noGrabDelay = 100;
                        Main.item[num].favorited = false;
                        Main.item[num].newAndShiny = false;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, num);
                        }

                        Player.inventory[i].SetDefaults();
                    }
                }
            }
        }
        if (!InBossFight && ModContent.GetInstance<AvalonConfig>().ReducedRespawnTimer)
        {
            Player.respawnTimer = (int)(Player.respawnTimer = 60 * 5);
        }
    }

    public override void OnConsumeMana(Item item, int manaConsumed)
    {
        if (BloodCasting)
        {
            int hp = manaConsumed / 3;
            Player.statLife -= hp;
            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, Player.width, Player.height), CombatText.DamagedFriendly, hp);
            if (Player.statLife <= 0)
            {
                Player.KillMe(PlayerDeathReason.ByCustomReason($"{Player.name} had their heart in the wrong place."), 1, 0);
            }
        }
    }
    public override void FrameEffects()
    {
        if ((DesertGamblerVisible && DesertGambler || ForceGambler) && Player.head <= 0)
        {
            Player.head = EquipLoader.GetEquipSlot(Mod, "DesertGambler", EquipType.Head);
            ArmorIDs.Head.Sets.DrawHatHair[Player.head] = true;
            ArmorIDs.Head.Sets.IsTallHat[Player.head] = true;
        }
    }
    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        
        if (lavaMerman || ForceVarefolk)
        {
            if (ForceVarefolk) Player.forceMerman = true;
            if (Player.head > 0 && ArmorIDs.Head.Sets.DrawHatHair[Player.head])
            {
                revertHeads.Add(Player.head);
            }
            ArmorIDs.Head.Sets.DrawHatHair[Player.head] = false;
            Player.head = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Head);
            Player.body = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Legs);
            ArmorIDs.Head.Sets.DrawHatHair[Player.head] = false;
            ArmorIDs.Head.Sets.DrawFullHair[Player.head] = false;
            ArmorIDs.Head.Sets.DrawHead[Player.head] = false;
            ArmorIDs.Body.Sets.HidesTopSkin[Player.body] = true;
            ArmorIDs.Body.Sets.HidesArms[Player.body] = true;
            ArmorIDs.Legs.Sets.HidesBottomSkin[Player.legs] = true;
            if (!Player.Male)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, LavaMermanName + "_Female", EquipType.Head);
            }
        }
        else
        {
            if (revertHeads.Count > 0)
            {
                foreach (int i in revertHeads)
                {
                    ArmorIDs.Head.Sets.DrawHatHair[i] = true;
                }
                revertHeads.Clear();
            }
        }
        if (Player.HasBuff(ModContent.BuffType<Shockwave>()) || Player.HasBuff(ModContent.BuffType<AdvShockwave>()))
        {
            r *= 0.7372f;
            g *= 0.5176f;
            b *= 0.3686f;
        }
        if(Pathogen)
        {
            g -= 0.3f;
            r -= 0.1f;
        }
    }
    public override void GetDyeTraderReward(List<int> rewardPool)
    {
        rewardPool.Add(ModContent.ItemType<Items.Dyes.HighVisDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.EbonstoneDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.CrimstoneDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.ChunkstoneDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.StoneDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.AquaDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.LavaDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.PhantomWispDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.ReflectiveRhodiumDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.ReflectiveOsmiumDye>());
        rewardPool.Add(ModContent.ItemType<Items.Dyes.ReflectiveIridiumDye>());
    }
    public override void PostHurt(Player.HurtInfo info)
    {
        if (Player.miscEquips[Player.miscSlotHook].type == ModContent.ItemType<Items.Tools.PreHardmode.EruptionHook>())
        {
            int proj = -1;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].type == ModContent.ProjectileType<Projectiles.Tools.EruptionHook>() && Main.projectile[i].owner == Player.whoAmI)
                {
                    proj = i;
                    break;
                }
            }
            if (proj != -1)
            {
                PlayerDeathReason damageSource = info.DamageSource;
                IEntitySource spawnSource = Player.GetSource_FromThis();
                Entity entity = null;
                if (damageSource.TryGetCausingEntity(out entity))
                {
                    spawnSource = Player.GetSource_OnHurt(entity);
                }
                int num4 = Projectile.NewProjectile(spawnSource, Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<EruptionHookBoom>(), (int)Player.GetTotalDamage(DamageClass.Generic).ApplyTo(35f), 15f, Main.myPlayer);
                Main.projectile[num4].netUpdate = true;
                Main.projectile[num4].Kill();
            }
        }
        if (Player.whoAmI == Main.myPlayer && CobShield)
        {
            int time = 300;
            if (CobOmegaShield)
            {
                time = 600;
            }

            Player.AddBuff(BuffID.Ironskin, time);
        }
        if (Player.whoAmI == Main.myPlayer && PallShield)
        {
            int hpHealed = 5;
            if (PallOmegaShield)
            {
                hpHealed = 10;
            }

            Player.statLife += hpHealed;
            Player.HealEffect(hpHealed);
        }
    }
    public override void UpdateBadLifeRegen()
    {
        if (DuraShield)
        {
            if (Player.poisoned)
            {
                int add = 1;
                if (DuraOmegaShield) add = 2;
                Player.lifeRegen += add;
            }
            if (Player.onFire)
            {
                int add = 2;
                if (DuraOmegaShield) add = 4;
                Player.lifeRegen += add;
            }
            if (Player.venom)
            {
                int add = 7;
                if (DuraOmegaShield) add = 15;
                Player.lifeRegen += add;
            }
            if (Player.onFire3)
            {
                int add = 2;
                if (DuraOmegaShield) add = 4;
                Player.lifeRegen += add;
            }
            if (Player.onFrostBurn)
            {
                int add = 4;
                if (DuraOmegaShield) add = 8;
                Player.lifeRegen += add;
            }
            if (Player.onFrostBurn2)
            {
                int add = 4;
                if (DuraOmegaShield) add = 8;
                Player.lifeRegen += add;
            }
            if (Player.onFire2)
            {
                int add = 6;
                if (DuraOmegaShield) add = 12;
                Player.lifeRegen += add;
            }
            if (Player.burned)
            {
                int add = 15;
                if (DuraOmegaShield) add = 30;
                Player.lifeRegen += add;
            }
            if (Player.electrified)
            {
                int add = 2;
                if (DuraOmegaShield) add = 4;
                Player.lifeRegen += add;
                if (Player.controlLeft || Player.controlRight)
                {
                    add = 8;
                    if (DuraOmegaShield) add = 16;
                    Player.lifeRegen += add;
                }
            }
        }
    }
    public LinkedListNode<int> ObtainExistingPlanet(int index, int planetNum)
    {
        int diff = index + 1 - Planets[planetNum].Count;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                Planets[planetNum].AddLast(Planets[planetNum].Count);
            }

            return Planets[planetNum].Last;
        }

        return Planets[planetNum].Find(index);
    }
    public override void SaveData(TagCompound tag)
    {
        tag["Avalon:StatStam"] = Player.GetModPlayer<AvalonStaminaPlayer>().StatStam;
    }
    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("Avalon:StatStam"))
        {
            Player.GetModPlayer<AvalonStaminaPlayer>().StatStam = tag.Get<int>("Avalon:StatStam");
        }
    }
    public override void PostUpdateEquips()
    {
        if (Player.HeldItem.type == ModContent.ItemType<RhodiumPickaxe>() || Player.HeldItem.type == ModContent.ItemType<OsmiumPickaxe>() || Player.HeldItem.type == ModContent.ItemType<IridiumPickaxe>())
        {
            Player.pickSpeed -= 0.5f;
        }
        else if (Player.HeldItem.type == ModContent.ItemType<BlahsPicksawTierII>())
        {
            Player.pickSpeed -= 0.75f;
        }

        #region double tap keys
        for (int m = 0; m < 3; m++)
        {
            doubleTapTimer[m]--;
            if (doubleTapTimer[m] < 0)
            {
                doubleTapTimer[m] = 0;
            }
        }
        for (int m = 0; m < 3; m++)
        {
            bool keyPressedAndReleased = false;
            switch (m)
            {
                case 0:
                    keyPressedAndReleased = Player.controlDown && Player.releaseDown;
                    break;
                case 1:
                    keyPressedAndReleased = Player.controlUp && Player.releaseUp;
                    break;
                case 2:
                    keyPressedAndReleased = Player.controlJump && Player.releaseJump;
                    break;
            }
            if (keyPressedAndReleased)
            {
                if (doubleTapTimer[m] > 0)
                {
                    KeyDoubleTap(m);
                }
                else
                {
                    doubleTapTimer[m] = 15;
                }
            }
        }
        #endregion

        #region chaos charm
        if (ChaosCharm)
        {
            int modCrit = 2 * (int)Math.Floor((Player.statLifeMax2 - (double)Player.statLife) /
                Player.statLifeMax2 * 10.0);
            Player.GetCritChance(DamageClass.Generic) += modCrit;
        }
        #endregion chaos charm

        #region bubble boost
        if (BubbleBoost && !Player.IsOnGround() && !Player.releaseJump)// &&
            //!NPC.AnyNPCs(ModContent.NPCType<Bosses.Superhardmode.ArmageddonSlime>()))
        {
            #region bubble timer and spawn bubble gores/sound
            bubbleCD++;
            if (bubbleCD == 20)
            {
                for (int i = 0; i < 3; i++)
                {
                    int g1 = Gore.NewGore(Player.GetSource_FromThis(),
                        Player.Center + new Vector2(Main.rand.Next(-32, 33), Main.rand.Next(-32, 33)), Player.velocity,
                        Mod.Find<ModGore>("Bubble").Type);
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Bubbles"),
                        Player.position);
                }
            }

            if (bubbleCD == 30)
            {
                for (int i = 0; i < 2; i++)
                {
                    int g1 = Gore.NewGore(Player.GetSource_FromThis(),
                        Player.Center + new Vector2(Main.rand.Next(-32, 33), Main.rand.Next(-32, 33)), Player.velocity,
                        Mod.Find<ModGore>("LargeBubble").Type);
                }
            }

            if (bubbleCD == 40)
            {
                for (int i = 0; i < 4; i++)
                {
                    int g1 = Gore.NewGore(Player.GetSource_FromThis(),
                        Player.Center + new Vector2(Main.rand.Next(-32, 33), Main.rand.Next(-32, 33)), Player.velocity,
                        Mod.Find<ModGore>("SmallBubble").Type);
                }

                bubbleCD = 0;
            }
            #endregion bubble timer and spawn bubble gores/sound

            #region down
            if (Player.controlDown && Player.controlJump)
            {
                Player.wingsLogic = 0;
                Player.rocketBoots = 0;
                if (Player.controlLeft)
                {
                    Player.velocity.X = -15f;
                }
                else if (Player.controlRight)
                {
                    Player.velocity.X = 15f;
                }
                else
                {
                    Player.velocity.X = 0f;
                }

                Player.velocity.Y = 15f;
                //bubbleBoostActive = true;
            }
            #endregion down
            #region up
            else if (Player.controlUp && Player.controlJump)
            {
                Player.wingsLogic = 0;
                Player.rocketBoots = 0;
                if (Player.controlLeft)
                {
                    Player.velocity.X = -15f;
                }
                else if (Player.controlRight)
                {
                    Player.velocity.X = 15f;
                }
                else
                {
                    Player.velocity.X = 0f;
                }

                Player.velocity.Y = -15f;
                //bubbleBoostActive = true;
            }
            #endregion up
            #region left
            else if (Player.controlLeft && Player.controlJump)
            {
                Player.velocity.X = -15f;
                Player.wingsLogic = 0;
                Player.rocketBoots = 0;
                if (Player.gravDir == 1f && Player.velocity.Y > -Player.gravity)
                {
                    Player.velocity.Y = -(Player.gravity + 1E-06f);
                }
                else if (Player.gravDir == -1f && Player.velocity.Y < Player.gravity)
                {
                    Player.velocity.Y = Player.gravity + 1E-06f;
                }
                //bubbleBoostActive = true;
            }
            #endregion left
            #region right
            else if (Player.controlRight && Player.controlJump)
            {
                Player.velocity.X = 15f;
                Player.wingsLogic = 0;
                Player.rocketBoots = 0;
                if (Player.gravDir == 1f && Player.velocity.Y > -Player.gravity)
                {
                    Player.velocity.Y = -(Player.gravity + 1E-06f);
                }
                else if (Player.gravDir == -1f && Player.velocity.Y < Player.gravity)
                {
                    Player.velocity.Y = Player.gravity + 1E-06f;
                }

                //bubbleBoostActive = true;
            }
            #endregion right

            StayInBounds(Player.position);
        }
        #endregion bubble boost

        if (AccLavaMerman && !HideVarefolk && Collision.LavaCollision(Player.position, Player.width, Player.height))
        {
            lavaMerman = true;
            Player.merman = true;
        }

        for (int i = 0; i <= 9; i++)
        {
            Item item = Player.armor[i];
            (PrefixLoader.GetPrefix(item.prefix) as ExxoPrefix)?.UpdateOwnerPlayer(Player);
        }

        if (TeleportV || tpStam)
        {
            if (tpCD > 300)
            {
                tpCD = 300;
            }

            tpCD++;
        }

        Player.GetModPlayer<AvalonStaminaPlayer>().FlightRestoreCooldown++;
        if (Player.GetModPlayer<AvalonStaminaPlayer>().FlightRestoreCooldown > 3600)
        {
            Player.GetModPlayer<AvalonStaminaPlayer>().FlightRestoreCooldown = 3600;
        }
    }
    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        if (Player.HasBuff<AdvAmmoReservation>() && Main.rand.NextFloat() < AdvAmmoReservation.Chance)
        {
            return false;
        }
        if (AmmoCost85 && Main.rand.Next(100) < 15)
        {
            return false;
        }
        if (HoardingPrefix)
        {
            int amtOfAcc = 0;
            for (int slot = 3; slot <= 9; slot++)
            {
                if (Player.armor[slot].prefix == ModContent.PrefixType<Hoarding>())
                {
                    amtOfAcc++;
                }
            }
            if (Main.rand.NextFloat() < amtOfAcc * 0.05f)
            {
                return false;
            }
        }

        return base.CanConsumeAmmo(weapon, ammo);
    }
    public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
    {
        int bait = attempt.playerFishingConditions.BaitItemType;
        int power = attempt.playerFishingConditions.BaitPower + attempt.playerFishingConditions.PolePower;
        int questFish = attempt.questFish;
        int poolSize = attempt.waterTilesCount;
        bool water = !attempt.inHoney && !attempt.inLava;
        bool lava = attempt.inLava;

        Point point = Player.Center.ToTileCoordinates();
        if ((Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion || Player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundContagion) &&
            attempt.uncommon && questFish == ModContent.ItemType<Items.Fish.Quest.Snotpiranha>())
        {
            itemDrop = ModContent.ItemType<Items.Fish.Quest.Snotpiranha>();
            return;
        }
        if (water)
        {
            if (attempt.crate && (Player.InModBiome<Biomes.Contagion>() || Player.InModBiome<Biomes.UndergroundContagion>()))
            {
                if (!attempt.veryrare && !attempt.legendary && attempt.rare && Main.rand.NextBool())
                {
                    if (Main.hardMode)
                    {
                        itemDrop = ModContent.ItemType<Items.Consumables.PlagueCrate>();
                        return;
                    }
                    else
                    {
                        itemDrop = ModContent.ItemType<Items.Consumables.ContagionCrate>();
                        return;
                    }
                }
            }
            if (attempt.legendary && Main.hardMode && (Player.InModBiome<Biomes.Contagion>() || Player.InModBiome<Biomes.UndergroundContagion>()) && Main.rand.NextBool(2))
            {
                itemDrop = ModContent.ItemType<Items.Weapons.Summon.Whips.AnchorWhipworm>();
                return;
            }
            if (attempt.uncommon && (Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion || Player.GetModPlayer<AvalonBiomePlayer>().ZoneUndergroundContagion))
            {
                int r = Main.rand.Next(3);
                if (r == 0)
                {
                    itemDrop = ModContent.ItemType<Items.Fish.Ickfish>();
                    return;
                }
                else if (r == 1)
                {
                    itemDrop = ModContent.ItemType<Items.Fish.NauSeaFish>();
                    return;
                }
                else if (r == 2)
                {
                    itemDrop = ModContent.ItemType<Items.Fish.SicklyTrout>();
                    return;
                }
            }
        }
    }
    public override void OnHitAnything(float x, float y, Entity victim)
    {
        if (victim is NPC && Player.HasBuff(ModContent.BuffType<Blah>()))
        {
            NPC n = (NPC)victim;
            n.AddBuff(BuffID.Midas, 60 * 3);
        }
    }
    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Berserk)
        {
            MeleeCritDamage += 1.5f;
            MaxMeleeCrit -= 20;
        }

        if (OblivionKill && Main.rand.NextBool(35) && !target.boss && target.type != ModContent.NPCType<DesertBeakWingNPC>())
        {
            target.life = 0;
            target.active = false;
            target.NPCLoot();
        }
        if (AncientSandVortex && Main.rand.NextBool(10) && item.DamageType == DamageClass.Melee)
        {
            Player.immuneTime = 120;
        }
        if (VampireTeeth && item.DamageType == DamageClass.Melee)
        {
            if (target.boss)
            {
                Player.VampireHeal(hit.Damage / 2, target.Center);
            }
            else
            {
                Player.VampireHeal(hit.Damage, target.Center);
            }
        }
        if (BloodyWhetstone)
        {
            if (!target.HasBuff<Lacerated>())
            {
                target.GetGlobalNPC<AvalonGlobalNPCInstance>().LacerateStacks = 1;
            }

            target.AddBuff(ModContent.BuffType<Lacerated>(), 120);
        }
        if (FrostGauntlet)
        {
            target.AddBuff(BuffID.Frostburn2, 60 * 4);
        }
        if (PathogenImbue)
        {
            target.AddBuff(ModContent.BuffType<Pathogen>(), 60 * Main.rand.Next(3, 7));
        }
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (RoseMagic && proj.DamageType == DamageClass.Magic && Main.rand.NextBool(8) && RoseMagicCooldown <= 0 && target.lifeMax > 5 && target.type != NPCID.TargetDummy)
        {
            int num36 = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X,
                (int)target.position.Y, target.width, target.height, ModContent.ItemType<Rosebud>());
            Main.item[num36].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
            Main.item[num36].velocity.X = Main.rand.Next(10, 31) * 0.2f * Player.direction;
            RoseMagicCooldown = 20;
        }

        if (target.life <= 0 && AncientRangedBonusActive && proj.owner == Main.myPlayer && proj.DamageType == DamageClass.Ranged)
        {
            Projectile.NewProjectile(Player.GetSource_OnHit(target), target.position, Vector2.Zero,
                ModContent.ProjectileType<SandyExplosion>(), hit.Damage * 2, hit.Knockback);
        }
        if (hit.DamageType == DamageClass.Summon && SkyBlessing)
        {
            if (Main.rand.NextBool(5))
            {
                int it = Item.NewItem(Player.GetSource_DropAsItem(), proj.Hitbox, ModContent.ItemType<SkyInsignia>());
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, it);
                }
            }
        }
    }
    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
        if (DoubleDamage && !Player.immune && !npc.dontTakeDamage)
        {
            int HitDir = -1;
            if (npc.position.X > Player.position.X)
            {
                HitDir = 1;
            }
            npc.StrikeNPC(new NPC.HitInfo
            {
                Damage = npc.damage * 2,
                Knockback = 0,
                HitDirection = HitDir
            });
        }

        //if (!npc.friendly && npc.aiStyle == 9)
        //{
        //    if (Main.rand.NextBool(4) || Player.HasItemInArmor(ModContent.ItemType<ReflexShield>()))
        //    {
        //        hurtInfo.Damage = 1;
        //        npc.friendly = true;
        //        npc.velocity *= -1;
        //    }
        //}

        if (Player.whoAmI == Main.myPlayer && BadgeOfBacteria)
        {
            Player.AddBuff(ModContent.BuffType<BacterialEndurance>(), 6 * 60);
            npc.AddBuff(ModContent.BuffType<BacteriaInfection>(), 6 * 60);
        }

        if (AuraThorns && !Player.immune && !npc.dontTakeDamage)
        {
            int x = (int)Player.position.X;
            int y = (int)Player.position.Y;
            foreach (NPC N2 in Main.npc)
            {
                if (N2.position.X >= x - 620 && N2.position.X <= x + 620 && N2.position.Y >= y - 620 &&
                    N2.position.Y <= y + 620)
                {
                    if (!N2.active || N2.dontTakeDamage || N2.townNPC || N2.life < 1 || N2.boss ||
                        N2.realLife >= 0) //|| N2.type == ModContent.NPCType<NPCs.Juggernaut>())
                    {
                        continue;
                    }
                    NPC.HitInfo dmg = new NPC.HitInfo();
                    dmg.Damage = hurtInfo.Damage;
                    dmg.Knockback = 5f;
                    dmg.HitDirection = 1;
                    N2.StrikeNPC(dmg);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.DamageNPC, -1, -1, NetworkText.Empty, N2.whoAmI,
                            hurtInfo.Damage);
                    }
                }
            }
        }
    }
    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
        if (Player.whoAmI == Main.myPlayer && BadgeOfBacteria)
        {
            Player.AddBuff(ModContent.BuffType<BacterialEndurance>(), 6 * 60);
        }
    }
    public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
        if (UndeadImmune)
        {
            int dmgPlaceholder = npc.damage;
            if (Data.Sets.NPC.Undead[npc.type])
            {
                modifiers.SetMaxDamage(dmgPlaceholder - ((Player.statDefense / 2) + 10));
            }
        }
    }
    /// <inheritdoc />
    public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (EarthInsig)
        {
            if (Data.Sets.Item.EarthRelatedItems[item.type])
            {
                modifiers.FinalDamage *= 1.2f;
            }
        }


        if (ZombieArmor && Vector2.Distance(Player.Center, target.Center) < 15 * 16)
        {
            modifiers.FlatBonusDamage += 5;
        }
        //if (HyperMelee)
        //{
        //    HyperBar++;
        //    if (HyperBar > 15 && HyperBar <= 25)
        //    {
        //        modifiers.SetCrit();
        //        if (HyperBar == 25)
        //        {
        //            HyperBar = 0;
        //        }
        //    }
        //}
        if (HyperMelee)
        {
            HyperBar++;
            float Start = 15 * (1 + ((100 - MaxMeleeCrit) / 100));
            float Mult = MaxMeleeCrit / 100;
            float End = Start + 10 * Mult;
            Start = (float)Math.Round(Start);
            End = (float)Math.Round(End);
            int Difference = (int)(End - Start);
            if (Difference < 5)
            {
                Difference = 5;
            }
            if (HyperBar > Start && HyperBar <= Start + Difference)
            {
                modifiers.SetCrit();
                if (HyperBar == Start + Difference)
                {
                    HyperBar = 0;
                }
            }
        }
        if (CrystalEdge)
        {
            modifiers.FlatBonusDamage += 15;
        }
        if (BacterialEndurance)
        {
            modifiers.FlatBonusDamage += 8;
        }
        if (item.DamageType == DamageClass.Melee)
        {
            modifiers.CritDamage += MeleeCritDamage;
        }
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (EarthInsig)
        {
            if (Data.Sets.Projectile.EarthRelatedItems[proj.type])
            {
                modifiers.FinalDamage *= 1.2f;
            }
        }

        //if (ZombieArmor && Vector2.Distance(Player.Center, target.Center) < 15 * 16)
        //{
        //    modifiers.FlatBonusDamage += 5;
        //}
        if (Berserk && proj.DamageType == DamageClass.Melee)
        {
            modifiers.SourceDamage *= 0.6f;
        }

        #region Troxinium armor Hyper Damage
        if (HyperMelee)
        {
            HyperBar++;
            float Start = 15 * (1 + ((100 - MaxMeleeCrit) / 100));
            float Mult = MaxMeleeCrit / 100;
            float End = Start + 10 * Mult;
            Start = (float)Math.Round(Start);
            End = (float)Math.Round(End);
            int Difference = (int)(End - Start);
            if (Difference < 5)
            {
                Difference = 5;
            }
            if (HyperBar > Start && HyperBar <= Start + Difference)
            {
                modifiers.SetCrit();
                if (HyperBar == Start + Difference)
                {
                    HyperBar = 0;
                }
            }
        }
        if (HyperRanged)
        {
            HyperBar++;
            float Start = 15 * (1 + ((100 - MaxRangedCrit) / 100));
            float Mult = MaxRangedCrit / 100;
            float End = Start + 10 * Mult;
            Start = (float)Math.Round(Start);
            End = (float)Math.Round(End);
            int Difference = (int)(End - Start);
            if (Difference < 5)
            {
                Difference = 5;
            }
            if (HyperBar > Start && HyperBar <= Start + Difference)
            {
                modifiers.SetCrit();
                if (HyperBar == Start + Difference)
                {
                    HyperBar = 0;
                }
            }
        }
        if (HyperMagic)
        {
            HyperBar++;
            float Start = 15 * (1 + ((100 - MaxMagicCrit) / 100));
            float Mult = MaxMagicCrit / 100;
            float End = Start + 10 * Mult;
            Start = (float)Math.Round(Start);
            End = (float)Math.Round(End);
            int Difference = (int)(End - Start);
            if (Difference < 5)
            {
                Difference = 5;
            }
            if (HyperBar > Start && HyperBar <= Start + Difference)
            {
                modifiers.SetCrit();
                if (HyperBar == Start + Difference)
                {
                    HyperBar = 0;
                }
            }
        }
        #endregion

        if (CrystalEdge)
        {
            modifiers.FlatBonusDamage += 15;
        }
        if (BacterialEndurance)
        {
            modifiers.FlatBonusDamage += 8;
        }
        if (proj.DamageType == DamageClass.Melee)
        {
            modifiers.CritDamage += MeleeCritDamage;
        }
        if (proj.DamageType == DamageClass.Magic)
        {
            modifiers.CritDamage += MagicCritDamage;
        }
        if (proj.DamageType == DamageClass.Ranged)
        {
            modifiers.CritDamage += RangedCritDamage;
        }
    }
    public override void MeleeEffects(Item item, Rectangle hitbox)
    {
        if (item.DamageType == DamageClass.Melee && !item.noMelee && !item.noUseGraphic)
        {
            if (PathogenImbue)
            {
                if (Main.rand.NextBool(3))
                {
                    int num21 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<PathogenDust>(), Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 128);
                    Main.dust[num21].noGravity = true;
                    Main.dust[num21].fadeIn = 1.5f;
                    Main.dust[num21].velocity *= 0.25f;
                }
            }
            if (FrostGauntlet)
            {
                if (Main.rand.NextBool(3))
                {
                    int num21 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceTorch, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100);
                    Main.dust[num21].noGravity = true;
                    Main.dust[num21].fadeIn = 1.5f;
                    Main.dust[num21].velocity *= 0.25f;
                    Main.dust[num21].velocity *= 0.7f;
                    Main.dust[num21].velocity.Y -= 0.5f;
                }
            }
        }
    }
    public override void OnHurt(Player.HurtInfo info)
    {
        if (BenevolentWard && Main.rand.NextBool(100) && !Player.HasBuff(ModContent.BuffType<Buffs.BenevolentWard>()) &&
            !Player.HasBuff(ModContent.BuffType<WardCurse>()))
        {
            Player.AddBuff(ModContent.BuffType<Buffs.BenevolentWard>(), 8 * 60);
        }
        if (Ward)
        {
            WardCurseDOT += info.Damage;
            info.Damage = 1;
        }
        if (Player.whoAmI == Main.myPlayer && NinjaPotion && Main.rand.NextBool(10))
        {
            Player.NinjaDodge();
            info.Damage = 0;
        }
        if (Player.whoAmI == Main.myPlayer && NinjaElixir && Main.rand.NextBool(15))
        {
            Player.NinjaDodge();
            info.Damage = 0;
        }

        if (CaesiumPoison)
        {
            info.Damage = (int)(info.Damage * 1.15f);
        }
    }
    public override bool ImmuneTo(PlayerDeathReason damageSource, int cooldownCounter, bool dodgeable)
    {
        if (UndeadImmune)
        {
            NPC N = Main.npc[damageSource.SourceNPCIndex];
            if (Data.Sets.NPC.Undead[N.type])
            {
                if (N.damage - ((Player.statDefense / 2) + 10) <= 0)
                {
                    return true;
                }
            }
        }
        if (Reflex)
        {
            if (damageSource.SourceProjectileLocalIndex > -1)
            {
                Projectile proj = Main.projectile[damageSource.SourceProjectileLocalIndex];
                if (!proj.friendly && !proj.bobber && !Data.Sets.Projectile.DontReflect[proj.type])
                {
                    if (Main.rand.NextBool(4))
                    {
                        proj.hostile = false;
                        proj.friendly = true;
                        proj.velocity *= -1;
                        return true;
                    }
                }
            }
            if (damageSource.SourceNPCIndex > -1)
            {
                NPC npc = Main.npc[damageSource.SourceNPCIndex];
                if (!npc.friendly && npc.aiStyle == 9)
                {
                    if (Main.rand.NextBool(4))
                    {
                        npc.friendly = true;
                        npc.velocity *= -1;
                        return true;
                    }
                }
            }
        }
        if (ReflexShield)
        {
            if (damageSource.SourceProjectileLocalIndex > -1)
            {
                Projectile proj = Main.projectile[damageSource.SourceProjectileLocalIndex];
                if (!proj.friendly && !proj.bobber && !Data.Sets.Projectile.DontReflect[proj.type])
                {
                    if (Main.rand.NextBool(2))
                    {
                        proj.hostile = false;
                        proj.friendly = true;
                        proj.velocity *= -1;
                        return true;
                    }
                }
            }
            if (damageSource.SourceNPCIndex > -1)
            {
                NPC npc = Main.npc[damageSource.SourceNPCIndex];
                if (!npc.friendly && npc.aiStyle == 9)
                {
                    if (Main.rand.NextBool(2))
                    {
                        npc.friendly = true;
                        npc.velocity *= -1;
                        return true;
                    }
                }
            }
        }
        return base.ImmuneTo(damageSource, cooldownCounter, dodgeable);
    }
    /// <inheritdoc />
    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
        if (Player.HasBuff<SpectrumBlur>() && Player.whoAmI == Main.myPlayer && Main.rand.NextBool(10))
        {
            SpectrumDodge();
            return;
        }
        if (!modifiers.PvP)
        {
            return;
        }

        // if (crit)
        // {
        //     damage += MultiplyMeleeCritDamage(damage);
        // }

        // if (modifiers.DamageSource.SourceProjectileType >= ProjectileID.None && crit)
        // {
        //     if (proj.DamageType == DamageClass.Magic)
        //     {
        //         damage += MultiplyMagicCritDamage(damage);
        //     }
        //     if (proj.DamageType == DamageClass.Melee)
        //     {
        //         damage += MultiplyMeleeCritDamage(damage);
        //     }
        //     if (proj.DamageType == DamageClass.Ranged)
        //     {
        //         damage += MultiplyRangedCritDamage(damage);
        //     }
        // }
    }
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (KeybindSystem.ShadowHotkey.JustPressed && tpStam && tpCD >= 300 &&
            Player.GetModPlayer<AvalonStaminaPlayer>().TeleportUnlocked)
        {
            int amt = 90;
            if (Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrain)
            {
                amt *= (int)(Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks *
                             Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainMult);
            }

            if (Player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
            {
                Player.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
                tpCD = 0;
                if (Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                        (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType !=
                    ModContent.WallType<ImperviousBrickWallUnsafe>() &&
                    Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                        (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType != WallID.LihzahrdBrickUnsafe)
                {
                    for (int m = 0; m < 70; m++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror,
                            Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, default, 1.1f);
                    }

                    Player.position.X = Main.mouseX + Main.screenPosition.X;
                    Player.position.Y = Main.mouseY + Main.screenPosition.Y;
                    for (int n = 0; n < 70; n++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150,
                            default, 1.1f);
                    }
                }
            }
            else if (Player.GetModPlayer<AvalonStaminaPlayer>().StamFlower)
            {
                Player.GetModPlayer<AvalonStaminaPlayer>().QuickStamina(amt);
                if (Player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
                {
                    Player.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
                    tpCD = 0;
                    if (Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                            (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType !=
                        ModContent.WallType<ImperviousBrickWallUnsafe>() &&
                        Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                            (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType != WallID.LihzahrdBrickUnsafe &&
                        !Main.wallDungeon[
                            Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                                (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType])
                    {
                        for (int m = 0; m < 70; m++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror,
                                Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, default, 1.1f);
                        }

                        Player.position.X = Main.mouseX + Main.screenPosition.X;
                        Player.position.Y = Main.mouseY + Main.screenPosition.Y;
                        for (int n = 0; n < 70; n++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150,
                                default, 1.1f);
                        }
                    }
                }
            }
        }
        else if (KeybindSystem.ShadowHotkey.JustPressed && (TeleportV || teleportVWasTriggered) && tpCD >= 300)
        {
            teleportVWasTriggered = false;
            tpCD = 0;
            if (Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                    (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType !=
                ModContent.WallType<ImperviousBrickWallUnsafe>() &&
                Main.tile[(int)(Main.mouseX + Main.screenPosition.X) / 16,
                    (int)(Main.mouseY + Main.screenPosition.Y) / 16].WallType != WallID.LihzahrdBrickUnsafe)
            {
                for (int m = 0; m < 70; m++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror,
                        Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, default, 1.1f);
                }

                Player.position.X = Main.mouseX + Main.screenPosition.X;
                Player.position.Y = Main.mouseY + Main.screenPosition.Y;
                for (int n = 0; n < 70; n++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150, default,
                        1.1f);
                }
            }
        }
    }
    public override void PostUpdateMiscEffects()
    {
        #region Biome Particles
        ////--== Biome Particles ==--
        //if(ModContent.GetInstance<AvalonClientConfig>().BiomeParticlesEnabled)
        //    {
        //    if (Main.myPlayer == Player.whoAmI && Main.netMode != NetmodeID.Server)
        //    {

        //        if (Player.position.Y >= (Main.maxTilesY - 300) * 16) // Sparks
        //        {
        //            int yea = (Player.position.Y >= (Main.maxTilesY - 200) * 16) ? 5 : 2;
        //            for (int i = 0; i < Main.rand.Next(yea); i++)
        //            {
        //                Dust d = Dust.NewDustPerfect(Player.Center + new Vector2(Main.rand.Next(-1000, 1000), Main.rand.Next(-1000, 1000)), DustID.Torch, new Vector2(Main.rand.NextFloat(1f, 8f), Main.rand.NextFloat(-1f, -5f)), 0, default, Main.rand.NextFloat(0.1f, 1.5f));
        //                d.noGravity = Main.rand.NextBool(3);
        //                d.noLightEmittence = true;
        //                if (Main.rand.NextBool(3))
        //                    d.type = DustID.InfernoFork;
        //            }
        //        }
        //        if (Player.position.Y >= (Main.maxTilesY - 200) * 16) // Smoke
        //        {
        //            for (int i = 0; i < Main.rand.Next(3); i++)
        //            {
        //                Dust d = Dust.NewDustPerfect(new Vector2(Player.Center.X, Main.rand.Next((Main.maxTilesY - 150) * 16, Main.maxTilesY * 16)) + new Vector2(Main.rand.Next(-1000, 1000), 0), DustID.Smoke, new Vector2(Main.rand.NextFloat(1f, 8f), Main.rand.NextFloat(-1f, -5f)), 200, Color.DarkGray, Main.rand.NextFloat(0.1f, 2f));
        //                d.noGravity = Main.rand.NextBool(3);
        //                d.noLightEmittence = true;
        //            }
        //            if (Player.position.X < 1100 * 16 || Player.position.X > (Main.maxTilesX - 1100) * 16) // Ashwood Biome
        //            {
        //                for (int ix = -60; ix < 60; ix++)
        //                {
        //                    for (int iy = -60; iy < 60; iy++)
        //                    {
        //                        Vector2 coord = Player.Center + new Vector2(ix * 16, iy * 16);
        //                        int TileCoordsX = (int)(coord.X / 16);
        //                        int TileCoordsY = (int)(coord.Y / 16);
        //                        if (Main.tile[TileCoordsX, TileCoordsY + 1].LiquidType == LiquidID.Lava && Main.tile[TileCoordsX, TileCoordsY + 1].LiquidAmount > 0 && Main.tile[TileCoordsX, TileCoordsY - 1].LiquidAmount == 0)
        //                        {
        //                            if (Main.rand.NextBool(2300))
        //                            {
        //                                Gore g = Gore.NewGorePerfect(Player.GetSource_FromThis(), coord, new Vector2(Main.rand.NextFloat(-1f, 3f), Main.rand.NextFloat(-3f, -4f)), Main.rand.Next(11, 13), 1);
        //                                g.GetAlpha(Color.Lerp(new Color(128, 128, 128, 128), new Color(0, 0, 0, 128), Main.rand.NextFloat(0.3f, 0.8f)));
        //                                g.rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
        //                                g.alpha = Main.rand.Next(128, 180);
        //                                g.scale = Main.rand.NextFloat(0.8f, 1.5f);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion

        if (RoseMagic)
        {
            if (RoseMagicCooldown > 0)
            {
                RoseMagicCooldown--;
            }
            else
            {
                RoseMagicCooldown = 0;
            }
        }

        if (SpectrumSpeed)
        {
            float damagePercent;
            float maxSpeed;

            if (NoSticky)
            {
                maxSpeed = 5f;
            }
            else
            {
                maxSpeed = Player.maxRunSpeed;
            }

            damagePercent = (-25f * (Math.Abs(Player.velocity.X) / maxSpeed)) + 25f;

            if (damagePercent < 0)
            {
                damagePercent = 0;
            }

            if (Math.Abs(Player.velocity.X) >= maxSpeed || Player.velocity.Y < 0f)
            {
                Player.AddBuff(ModContent.BuffType<SpectrumBlur>(), 5);
            }

            Player.GetDamage(DamageClass.Ranged) += damagePercent / 100f;
        }

        if (Player.whoAmI == Main.myPlayer && Main.netMode == NetmodeID.MultiplayerClient)
        {
            SyncMouseCursor(server: false);
        }
    }
    public override void PostUpdateRunSpeeds()
    {
        FloorVisualsAvalon(Player.velocity.Y > Player.gravity);
    }
    public void FloorVisualsAvalon(bool falling)
    {
        int num = (int)((Player.position.X + (Player.width / 2)) / 16f);
        int num2 = (int)((Player.position.Y + Player.height) / 16f);
        Tile? floorTile = Player.GetFloorTile(num, num2);
        int num3 = -1;
        if (floorTile.HasValue)
        {
            num3 = floorTile.Value.TileType;
        }
        if (num3 <= -1)
        {
            Player.ResetFloorFlags();
            return;
        }

        if (num3 > -1)
        {
            if (SlimeBand) // || Player.GetModPlayer<ExxoBiomePlayer>().ZoneIceSoul)
            {
                Player.slippy = true;
                Player.slippy2 = true;
            }
            else
            {
                Player.slippy = false;
                Player.slippy2 = false;
            }
        }
    }
    public void KeyDoubleTap(int keyDir)
    {
        int num = 0;
        if (Main.ReversedUpDownArmorSetBonuses)
        {
            num = 1;
        }
        if (keyDir == 2)
        {
            goto doubleTapJump;
        }
        if (keyDir != num)
        {
            return;
        }
        //if (FrenzyStance && !Player.mount.Active)
        //{
        //    FrenzyStanceActive = !FrenzyStanceActive;
        //}
        //if (CaesiumBoost && !Player.mount.Active)
        //{
        //    CaesiumBoostActive = !CaesiumBoostActive;
        //}
        if (AncientRangedBonusActive && !Player.mount.Active)
        {
            AncientRangedBonusActive = !AncientRangedBonusActive;
        }
        doubleTapJump:
        if (keyDir == 2)
        {
            if (Player.wingsLogic > 0 && Player.wingTime == 0 &&
                Player.GetModPlayer<AvalonStaminaPlayer>().FlightRestoreUnlocked &&
                Player.GetModPlayer<AvalonStaminaPlayer>().FlightRestoreCooldown >= 60 * 60)
            {
                int amt = 150;
                if (Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrain)
                {
                    amt *= (int)(Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks * Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainMult);
                }
                if (Player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
                {
                    Player.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
                    Player.GetModPlayer<AvalonStaminaPlayer>().FlightRestoreCooldown = 0;
                    Player.wingTime = Player.wingTimeMax;
                }
                else if (Player.GetModPlayer<AvalonStaminaPlayer>().StamFlower)
                {
                    Player.GetModPlayer<AvalonStaminaPlayer>().QuickStamina(amt);
                    if (Player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
                    {
                        Player.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
                        Player.GetModPlayer<AvalonStaminaPlayer>().FlightRestoreCooldown = 0;
                        Player.wingTime = Player.wingTimeMax;
                    }
                }
            }
        }
    }
    private void SpectrumDodge()
    {
        Player.immune = true;
        if (Player.longInvince)
        {
            Player.immuneTime = 60;
        }
        else
        {
            Player.immuneTime = 30;
        }

        SoundEngine.PlaySound(new SoundStyle("Avalon/Sounds/Item/SpectrumDodge"), Player.position);
        for (int i = 0; i < Player.hurtCooldowns.Length; i++)
        {
            Player.hurtCooldowns[i] = Player.immuneTime;
        }

        if (Player.whoAmI == Main.myPlayer)
        {
            NetMessage.SendData(Terraria.ID.MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f);
        }
    }
    public static void StayInBounds(Vector2 pos)
    {
        if (pos.X > Main.maxTilesX - 100)
        {
            pos.X = Main.maxTilesX - 100;
        }

        if (pos.X < 100f)
        {
            pos.X = 100f;
        }

        if (pos.Y > Main.maxTilesY)
        {
            pos.Y = Main.maxTilesY;
        }

        if (pos.Y < 100f)
        {
            pos.Y = 100f;
        }
    }

    #region crit dmg stuff
    public int MultiplyCritDamage(int dmg, float mult = 0f) // dmg = damage before crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * ((mult == 0f ? CritDamageMult : mult) + 1f) / 2);
        return bonusDmg;
    }

    public int MultiplyMagicCritDamage(int dmg, float mult = 0f) // dmg = damage before crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * ((mult == 0f ? MagicCritDamage : mult) + 1f) / 2);
        return bonusDmg;
    }

    public int MultiplyMeleeCritDamage(int dmg, float mult = 0f) // dmg = damage before crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * ((mult == 0f ? MeleeCritDamage : mult) + 1f) / 2);
        return bonusDmg;
    }
    public int MultiplyRangedCritDamage(int dmg, float mult = 0f) // dmg = damage before crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * ((mult == 0f ? RangedCritDamage : mult) + 1f) / 2);
        return bonusDmg;
    }
    public void AllCritDamage(float value)
    {
        MagicCritDamage += value;
        MeleeCritDamage += value;
        RangedCritDamage += value;
        CritDamageMult += value;
    }
    public void AllMaxCrit(int value)
    {
        MaxMagicCrit -= value;
        MaxMeleeCrit -= value;
        MaxRangedCrit -= value;
    }
    #endregion crit dmg stuff

    internal void HandleMouseCursor(BinaryReader reader)
    {
        MousePosition = reader.ReadVector2();
        if (Main.netMode == NetmodeID.Server)
            SyncMouseCursor(server: true);
    }
    public void SyncMouseCursor(bool server)
    {
        ModPacket packet = Mod.GetPacket();
        packet.Write((int)Network.MessageID.CursorPosition);
        packet.Write(Player.whoAmI);
        packet.WriteVector2(MousePosition);
        Player.SendPacket(packet, server);
    }
    public void LevelUpSkyBlessing()
    {
        Player.AddBuff(ModContent.BuffType<SkyBlessing>(), 60 * 7);
    }
    public void ResetShadowCooldown() => ShadowCooldown = 0;
}
