using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Items.Other;
using Avalon.Prefixes;
using Avalon.Systems;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class AvalonPlayer : ModPlayer
{
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
    public bool TrapImmune;
    public bool Lucky;
    public bool PulseCharm;
    public bool ShadowCharm;
    public bool CloudGlove;
    public float BonusKB = 1f;
    public bool AncientHeadphones;
    public bool EnchantedDie;

    public int FrameCount { get; private set; }
    public int ShadowCooldown { get; private set; }
    public int OldFallStart;
    public bool AdvancedBattle;
    public bool AdvancedCalming;
    public int TimeSlowCounter;

    public override void ResetEffects()
    {
        MagicCritDamage = 1f;
        MeleeCritDamage = 1f;
        RangedCritDamage = 1f;
        TrapImmune = false;
        AdvancedBattle = false;
        AdvancedCalming = false;
        Lucky = false;
        PulseCharm = false;
        ShadowCharm = false;
        CritDamageMult = 1f;
        BonusKB = 1f;
        AncientHeadphones = false;
        EnchantedDie = false;
        Player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 = Player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax;
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
            Player.statLife = Player.statLifeMax2 / 3;
            Player.AddBuff(ModContent.BuffType<ImmortalityCooldown>(), 60 * 60 * 3);
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
    }

    /// <inheritdoc />
    public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers) {
        // if (modifiers.CritDamage)
        // {
        //     damage += MultiplyMeleeCritDamage(damage);
        // }
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
    {
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

    /// <inheritdoc />
    public override void ModifyHurt(ref Player.HurtModifiers modifiers) {
        if (!modifiers.PvP) {
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
        //--== Biome Particles ==--
        if(ModContent.GetInstance<AvalonClientConfig>().BiomeParticlesEnabled)
            {
            if (Main.myPlayer == Player.whoAmI && Main.netMode != NetmodeID.Server)
            {

                if (Player.position.Y >= (Main.maxTilesY - 300) * 16) // Sparks
                {
                    int yea = (Player.position.Y >= (Main.maxTilesY - 200) * 16) ? 5 : 2;
                    for (int i = 0; i < Main.rand.Next(yea); i++)
                    {
                        Dust d = Dust.NewDustPerfect(Player.Center + new Vector2(Main.rand.Next(-1000, 1000), Main.rand.Next(-1000, 1000)), DustID.Torch, new Vector2(Main.rand.NextFloat(1f, 8f), Main.rand.NextFloat(-1f, -5f)), 0, default, Main.rand.NextFloat(0.1f, 1.5f));
                        d.noGravity = Main.rand.NextBool(3);
                        d.noLightEmittence = true;
                        if (Main.rand.NextBool(3))
                            d.type = DustID.InfernoFork;
                    }
                }
                if (Player.position.Y >= (Main.maxTilesY - 200) * 16) // Smoke
                {
                    for (int i = 0; i < Main.rand.Next(3); i++)
                    {
                        Dust d = Dust.NewDustPerfect(new Vector2(Player.Center.X, Main.rand.Next((Main.maxTilesY - 150) * 16, Main.maxTilesY * 16)) + new Vector2(Main.rand.Next(-1000, 1000), 0), DustID.Smoke, new Vector2(Main.rand.NextFloat(1f, 8f), Main.rand.NextFloat(-1f, -5f)), 200, Color.DarkGray, Main.rand.NextFloat(0.1f, 2f));
                        d.noGravity = Main.rand.NextBool(3);
                        d.noLightEmittence = true;
                    }
                    if (Player.position.X < 1100 * 16 || Player.position.X > (Main.maxTilesX - 1100) * 16) // Ashwood Biome
                    {
                        for (int ix = -60; ix < 60; ix++)
                        {
                            for (int iy = -60; iy < 60; iy++)
                            {
                                Vector2 coord = Player.Center + new Vector2(ix * 16, iy * 16);
                                int TileCoordsX = (int)(coord.X / 16);
                                int TileCoordsY = (int)(coord.Y / 16);
                                if (Main.tile[TileCoordsX, TileCoordsY + 1].LiquidType == LiquidID.Lava && Main.tile[TileCoordsX, TileCoordsY + 1].LiquidAmount > 0 && Main.tile[TileCoordsX, TileCoordsY - 1].LiquidAmount == 0)
                                {
                                    if (Main.rand.NextBool(2300))
                                    {
                                        Gore g = Gore.NewGorePerfect(Player.GetSource_FromThis(), coord, new Vector2(Main.rand.NextFloat(-1f, 3f), Main.rand.NextFloat(-3f, -4f)), Main.rand.Next(11, 13), 1);
                                        g.GetAlpha(Color.Lerp(new Color(128, 128, 128, 128), new Color(0, 0, 0, 128), Main.rand.NextFloat(0.3f, 0.8f)));
                                        g.rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
                                        g.alpha = Main.rand.Next(128, 180);
                                        g.scale = Main.rand.NextFloat(0.8f, 1.5f);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
