using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Other;
using Avalon.Prefixes;
using Avalon.Systems;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
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
    public bool teleportV;
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

    #region accessories
    public bool PulseCharm;
    public bool ShadowCharm;
    public bool CloudGlove;
    public float BonusKB = 1f;
    public bool AncientHeadphones;
    public bool SixSidedDie;
    public bool LoadedDie;
    public bool CrystalEdge;
    public bool MutatedStocking;
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

    public bool SnotOrb;
    #endregion

    public int FrameCount { get; private set; }
    public int ShadowCooldown { get; private set; }
    public int OldFallStart;

    public override void ResetEffects()
    {
        MagicCritDamage = 1f;
        MeleeCritDamage = 1f;
        RangedCritDamage = 1f;

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

        // accessories
        TrapImmune = false;
        PulseCharm = false;
        ShadowCharm = false;
        CritDamageMult = 1f;
        BonusKB = 1f;
        AncientHeadphones = false;
        SixSidedDie = false;
        MutatedStocking = false;
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

        SnotOrb = false;

        MaxMagicCrit = 100;
        MaxMeleeCrit = 100;
        MaxRangedCrit = 100;

        Player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 = Player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax;
        if (Player.whoAmI == Main.myPlayer)
        {
            MousePosition = Main.MouseWorld;
        }

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

    public bool PotionSicknessSoundPlayed;
    public override void PostUpdateBuffs()
    {
        if (Player.lifeRegen < 0 && Pathogen)
        {
            Player.lifeRegen = (int)(Player.lifeRegen * 1.5f);
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
    public override void PreUpdateBuffs()
    {
        PlanetRotation[0] = (PlanetRotation[0] % MathHelper.TwoPi) + 0.08f;
        PlanetRotation[1] = (PlanetRotation[1] % MathHelper.TwoPi) + 0.09f;
        PlanetRotation[2] = (PlanetRotation[2] % MathHelper.TwoPi) + 0.06f;
        PlanetRotation[3] = (PlanetRotation[3] % MathHelper.TwoPi) + 0.07f;
        PlanetRotation[4] = (PlanetRotation[4] % MathHelper.TwoPi) + 0.05f;
        PlanetRotation[5] = (PlanetRotation[5] % MathHelper.TwoPi) + 0.03f;
        PlanetRotation[6] = (PlanetRotation[6] % MathHelper.TwoPi) + 0.042f;
        PlanetRotation[7] = (PlanetRotation[7] % MathHelper.TwoPi) + 0.05f;
        PlanetRotation[8] = (PlanetRotation[8] % MathHelper.TwoPi) + 0.035f;
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
        for (int i = 0; i <= 9; i++)
        {
            Item item = Player.armor[i];
            (PrefixLoader.GetPrefix(item.prefix) as ExxoPrefix)?.UpdateOwnerPlayer(Player);
        }

        if (teleportV || tpStam)
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
        if (MutatedStocking)
        {
            Player.maxRunSpeed += 0.3f; // Buggy :)
            Player.gravity += 0.5f;
            Player.jumpSpeedBoost += 4f;
            Player.jumpHeight -= 6;
            if (!Player.IsOnGround())
            {
                Player.runAcceleration += 1f;
                Player.maxRunSpeed += 0.3f;
            }
            if (Player.wingTime > 0 && Player.controlJump)
            {
                Player.velocity.Y += -0.5f * Player.gravDir;
            }
        }
    }
    public void ResetShadowCooldown() => ShadowCooldown = 0;
    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        if (Player.HasBuff<AdvAmmoReservation>() && Main.rand.NextFloat() < AdvAmmoReservation.Chance)
        {
            return false;
        }

        return base.CanConsumeAmmo(weapon, ammo);
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
    public override void PostUpdate()
    {
        #region crit cap
        for (int i = 0; i < Player.inventory.Length; i++)
        {
            Item curItem = Player.inventory[i];

            Item storedItem = new Item();
            storedItem.SetDefaults(curItem.type);

            float cMagic = 0f;
            float cMelee = 0f;
            float cRanged = 0f;
            if (storedItem.DamageType == DamageClass.Magic)
            {
                cMagic = storedItem.crit + Player.GetTotalCritChance(DamageClass.Magic);
            }
            if (storedItem.DamageType == DamageClass.Melee)
            {
                cMelee = storedItem.crit + Player.GetTotalCritChance(DamageClass.Melee);
            }
            if (storedItem.DamageType == DamageClass.Ranged)
            {
                cRanged = storedItem.crit + Player.GetTotalCritChance(DamageClass.Ranged);
            }

            // magic
            if (MaxMagicCrit < 100)
            {
                if (Player.inventory[Player.selectedItem].DamageType == DamageClass.Magic)
                {
                    Player.inventory[Player.selectedItem].crit = MaxMagicCrit - (int)Player.GetCritChance(DamageClass.Magic);
                }
            }
            else
            {
                if (Player.inventory[Player.selectedItem].DamageType == DamageClass.Magic)
                {
                    Player.inventory[Player.selectedItem].crit = (int)cMagic;
                }
            }
            // melee
            if (MaxMeleeCrit < 100)
            {
                if (Player.inventory[Player.selectedItem].DamageType == DamageClass.Melee)
                {
                    Player.inventory[Player.selectedItem].crit = MaxMeleeCrit - (int)Player.GetCritChance(DamageClass.Melee);
                }
            }
            else
            {
                if (Player.inventory[Player.selectedItem].DamageType == DamageClass.Melee)
                {
                    Player.inventory[Player.selectedItem].crit = (int)cMelee;
                }
            }
            // ranged
            if (MaxRangedCrit < 100)
            {
                if (Player.inventory[Player.selectedItem].DamageType == DamageClass.Ranged)
                {
                    Player.inventory[Player.selectedItem].crit = MaxRangedCrit - (int)Player.GetCritChance(DamageClass.Ranged);
                }
            }
            else
            {
                if (Player.inventory[Player.selectedItem].DamageType == DamageClass.Ranged)
                {
                    Player.inventory[Player.selectedItem].crit = (int)cRanged;
                }
            }
        }
        #endregion

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
    }

    public override void PreUpdate()
    {
        tpStam = !teleportV;
        if (teleportV)
        {
            teleportV = false;
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
        if (!InBossFight)
        {
            Player.respawnTimer = (int)(Player.respawnTimer = 60 * 5);
        }
    }
    /// <inheritdoc />
    public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (CrystalEdge)
        {
            modifiers.FlatBonusDamage += 15;
        }
        if (BacterialEndurance)
        {
            modifiers.FlatBonusDamage += 8;
        }
        // if (modifiers.CritDamage)
        // {
        //     damage += MultiplyMeleeCritDamage(damage);
        // }
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (CrystalEdge)
        {
            modifiers.FlatBonusDamage += 15;
        }
        if (BacterialEndurance)
        {
            modifiers.FlatBonusDamage += 8;
        }
        // if (crit)
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
    public override void OnHitNPC(NPC npc, NPC.HitInfo hit, int damage)
    {
        if (hit.DamageType == DamageClass.Melee && BloodyWhetstone)
        {
            if (!npc.HasBuff<Bleeding>())
            {
                npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks = 1;
            }

            npc.AddBuff(ModContent.BuffType<Bleeding>(), 120);
        }
        if (FrostGauntlet && hit.DamageType == DamageClass.Melee)
        {
            npc.AddBuff(BuffID.Frostburn2, 60 * 4);
        }
        if (PathogenImbue && hit.DamageType == DamageClass.Melee)
        {
            npc.AddBuff(ModContent.BuffType<Pathogen>(), 60 * Main.rand.Next(3, 7));
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
    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {

        if (Player.whoAmI == Main.myPlayer && BadgeOfBacteria)
        {
            Player.AddBuff(ModContent.BuffType<BacterialEndurance>(), 6 * 60);
        }
    }
    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
        if (Player.whoAmI == Main.myPlayer && BadgeOfBacteria)
        {
            Player.AddBuff(ModContent.BuffType<BacterialEndurance>(), 6 * 60);
            npc.AddBuff(ModContent.BuffType<BacteriaInfection>(), 6 * 60);
        }
    }

    /// <inheritdoc />
    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
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
        else if (KeybindSystem.ShadowHotkey.JustPressed && (teleportV || teleportVWasTriggered) && tpCD >= 300)
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
    #endregion crit dmg stuff
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
            if (num3 == 229 && !NoSticky)
            {
                Player.sticky = true;
            }
            else
            {
                Player.sticky = false;
            }

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
}
