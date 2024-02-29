using Avalon.Buffs;
using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Avalon.Items.Armor.Hardmode;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Common.Players;

public class AvalonStaminaPlayer : ModPlayer
{
    public static int StaminaDrainTime = 10 * 60;

    public bool EnergyCrystal;
    public int FlightRestoreCooldown = 0;
    public bool FlightRestoreUnlocked = false;
    public bool ReleaseQuickStamina;
    public bool SprintUnlocked = false;
    public bool StamFlower = false;
    public bool StaminaDrain = false;
    public float StaminaDrainMult = 1.2f;
    public int StaminaDrainStacks = 1;
    public int StaminaRegen;
    public int StaminaRegenCost = 1000;
    public int StaminaRegenCount;
    public int StamRegenDelay;
    public int StatStam = 100;
    public int StatStamMax = 30;
    public int StatStamMax2;
    public byte StaminaSwimCooldown;
    public byte StaminaSprintCooldown;
    public byte StaminaMiningCD;
    public byte StaminaSlidingCD;


    public bool ActivateSprint;
    public bool ActivateSwim;
    public bool ActivateSlide;
    public bool StamDashKey;

    public bool SwimmingUnlocked;
    public bool TeleportUnlocked;
    public bool RollDodgeUnlocked;
    public bool MiningSpeedUnlocked;
    public bool WallSlidingUnlocked;

    public override void ResetEffects()
    {
        StaminaDrain = false;
        StamFlower = false;
        if (EnergyCrystal) { StaminaRegen = 800; StaminaRegenCost = 800; }
        else { StaminaRegen = 1000; StaminaRegenCost = 1000; }

        FlightRestoreUnlocked = false;
        TeleportUnlocked = false;
        SprintUnlocked = false;
        SwimmingUnlocked = false;
        MiningSpeedUnlocked = false;
        WallSlidingUnlocked = false;
    }
    public void QuickStamina(int stamNeeded = 0) // todo: make stamina flower not allow you to consume stam pots that wouldn't allow you to continue using stamina
    {
        if (Player.noItems)
        {
            return;
        }
        if (StatStam == StatStamMax2)
        {
            return;
        }
        int num = StatStamMax2 - StatStam;
        Item potionToBeUsed = null;
        int num2 = -StatStamMax2;
        for (int i = 0; i < 58; i++)
        {
            Item potionChecked = Player.inventory[i];
            if (potionChecked.stack > 0 && potionChecked.type > 0 && potionChecked.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina > 0)
            {
                int num3 = potionChecked.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina - num;
                if (num2 < 0)
                {
                    if (num3 > num2)
                    {
                        potionToBeUsed = potionChecked;
                        num2 = num3;
                    }
                }
                else if (num3 < num2 && num3 >= 0)
                {
                    potionToBeUsed = potionChecked;
                    num2 = num3;
                }
            }
        }
        if (potionToBeUsed == null)
        {
            return;
        }
        if (potionToBeUsed.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina < stamNeeded && stamNeeded != 0)
        {
            return;
        }
        SoundEngine.PlaySound(SoundID.Item3, Player.position);
        StatStam += potionToBeUsed.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina;
        if (StatStam > StatStamMax2)
        {
            StatStam = StatStamMax2;
        }
        if (potionToBeUsed.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina > 0 && Main.myPlayer == Player.whoAmI)
        {
            Player.AddBuff(ModContent.BuffType<StaminaDrain>(), 8 * 60);
            StaminaHealEffect(potionToBeUsed.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina, true);
        }
        potionToBeUsed.stack--;
        if (potionToBeUsed.stack <= 0)
        {
            potionToBeUsed.type = 0;
        }
    }
    public void DashMovement()
    {
        if (Player.dashDelay > 0)
        {
            Player.dashDelay--;
            return;
        }

        int amt = 60;
        if (StaminaDrain)
        {
            amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
        }

        if (StamDashKey && Player.dash == 0 && Player.dashDelay >= 0)
        {
            if (StatStam > amt)
            {
                int num2 = 0;
                bool flag = false;
                if (Player.dashTime > 0)
                {
                    Player.dashTime--;
                }

                if (Player.dashTime < 0)
                {
                    Player.dashTime++;
                }

                if (Player.controlRight && Player.releaseRight)
                {
                    if (Player.dashTime > 0)
                    {
                        num2 = 1;
                        flag = true;
                        Player.dashTime = 0;
                        StatStam -= amt;
                    }
                    else
                    {
                        Player.dashTime = 15;
                    }
                }
                else if (Player.controlLeft && Player.releaseLeft)
                {
                    if (Player.dashTime < 0)
                    {
                        num2 = -1;
                        flag = true;
                        Player.dashTime = 0;
                        StatStam -= amt;
                    }
                    else
                    {
                        Player.dashTime = -15;
                    }
                }

                if (flag)
                {
                    Player.velocity.X = 15.9f * num2;
                    Player.dashDelay = -1;
                    for (int j = 0; j < 20; j++)
                    {
                        int num3 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width,
                            Player.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                        Dust expr_336_cp_0 = Main.dust[num3];
                        expr_336_cp_0.position.X += Main.rand.Next(-5, 6);
                        Dust expr_35D_cp_0 = Main.dust[num3];
                        expr_35D_cp_0.position.Y += Main.rand.Next(-5, 6);
                        Main.dust[num3].velocity *= 0.2f;
                        Main.dust[num3].scale *= 1f + (Main.rand.Next(20) * 0.01f);
                    }

                    int num4 = Gore.NewGore(Player.GetSource_FromThis(),
                        new Vector2(Player.position.X + (Player.width / 2) - 24f,
                            Player.position.Y + (Player.height / 2) - 34f), default, Main.rand.Next(61, 64));
                    Main.gore[num4].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num4].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num4].velocity *= 0.4f;
                    num4 = Gore.NewGore(Player.GetSource_FromThis(),
                        new Vector2(Player.position.X + (Player.width / 2) - 24f,
                            Player.position.Y + (Player.height / 2) - 14f), default, Main.rand.Next(61, 64));
                    Main.gore[num4].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num4].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[num4].velocity *= 0.4f;
                }
            }
            else if (StamFlower)
            {
                QuickStamina(amt);
                if (StatStam > amt)
                {
                    int num2 = 0;
                    bool flag = false;
                    if (Player.dashTime > 0)
                    {
                        Player.dashTime--;
                    }

                    if (Player.dashTime < 0)
                    {
                        Player.dashTime++;
                    }

                    if (Player.controlRight && Player.releaseRight)
                    {
                        if (Player.dashTime > 0)
                        {
                            num2 = 1;
                            flag = true;
                            Player.dashTime = 0;
                            StatStam -= amt;
                        }
                        else
                        {
                            Player.dashTime = 15;
                        }
                    }
                    else if (Player.controlLeft && Player.releaseLeft)
                    {
                        if (Player.dashTime < 0)
                        {
                            num2 = -1;
                            flag = true;
                            Player.dashTime = 0;
                            StatStam -= amt;
                        }
                        else
                        {
                            Player.dashTime = -15;
                        }
                    }

                    if (flag)
                    {
                        Player.velocity.X = 15.9f * num2;
                        Player.dashDelay = -1;
                        for (int j = 0; j < 20; j++)
                        {
                            int num3 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width,
                                Player.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                            Dust expr_336_cp_0 = Main.dust[num3];
                            expr_336_cp_0.position.X = expr_336_cp_0.position.X + Main.rand.Next(-5, 6);
                            Dust expr_35D_cp_0 = Main.dust[num3];
                            expr_35D_cp_0.position.Y += Main.rand.Next(-5, 6);
                            Main.dust[num3].velocity *= 0.2f;
                            Main.dust[num3].scale *= 1f + (Main.rand.Next(20) * 0.01f);
                        }

                        int num4 = Gore.NewGore(Player.GetSource_FromThis(),
                            new Vector2(Player.position.X + (Player.width / 2) - 24f,
                                Player.position.Y + (Player.height / 2) - 34f), default, Main.rand.Next(61, 64));
                        Main.gore[num4].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num4].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num4].velocity *= 0.4f;
                        num4 = Gore.NewGore(Player.GetSource_FromThis(),
                            new Vector2(Player.position.X + (Player.width / 2) - 24f,
                                Player.position.Y + (Player.height / 2) - 14f), default, Main.rand.Next(61, 64));
                        Main.gore[num4].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num4].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num4].velocity *= 0.4f;
                    }
                }
            }
        }
    }
    public override void PostUpdateMiscEffects()
    {
        DashMovement();
    }
    public override void PostUpdateRunSpeeds()
    {
        #region swimming (6 stamina per second)
        if (Player.wet && Player.velocity != Vector2.Zero && !Player.accMerman &&
            SwimmingUnlocked)
        {
            bool doSwim = false;
            StaminaSwimCooldown++;
            StaminaRegenCount = 0;

            int amt = 1;
            if (StaminaDrain)
            {
                amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
            }
            if (StatStam >= amt)
            {
                doSwim = true;
            }

            if (StaminaSwimCooldown >= 10 && doSwim)
            {
                StatStam -= amt;
                if (StamFlower && StatStam < amt)
                {
                    QuickStamina();
                }

                if (StatStam <= 0)
                {
                    StatStam = 0;
                    doSwim = false;
                }

                StaminaSwimCooldown = 0;
            }

            if (doSwim)
            {
                Player.accFlipper = true;
            }
        }
        #endregion

        #region sprinting (8 stamina per second)
        if (SprintUnlocked)
        {
            if ((Player.controlRight || Player.controlLeft) && Player.velocity.X != 0f)
            {
                bool doSprint = false;
                StaminaSprintCooldown++;
                StaminaRegenCount = 0;

                int amt = 2;
                if (StaminaDrain)
                {
                    amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
                }
                if (StatStam >= amt)
                {
                    doSprint = true;
                }

                if (StaminaSprintCooldown >= 15 && doSprint)
                {
                    StatStam -= amt;
                    if (StamFlower && StatStam < amt)
                    {
                        QuickStamina();
                    }

                    if (StatStam <= 0)
                    {
                        StatStam = 0;
                        doSprint = false;
                    }

                    StaminaSprintCooldown = 0;
                }

                if (doSprint)
                {
                    if (!Player.HasItemInFunctionalAccessories(ItemID.HermesBoots) && !Player.HasItemInArmor(ItemID.FlurryBoots) &&
                        !Player.HasItemInFunctionalAccessories(ItemID.SpectreBoots) &&
                        !Player.HasItemInFunctionalAccessories(ItemID.LightningBoots) &&
                        !Player.HasItemInFunctionalAccessories(ItemID.FrostsparkBoots) &&
                        !Player.HasItemInFunctionalAccessories(ItemID.SailfishBoots) &&
                        !Player.HasItemInFunctionalAccessories(ItemID.TerrasparkBoots) &&
                        !Player.GetModPlayer<AvalonPlayer>().InertiaBoots && !Player.GetModPlayer<AvalonPlayer>().BlahWings)
                    {
                        Player.accRunSpeed = 6f;
                    }
                    else if (!Player.HasItemInFunctionalAccessories(ItemID.LightningBoots) &&
                             !Player.HasItemInFunctionalAccessories(ItemID.FrostsparkBoots) &&
                             !Player.HasItemInFunctionalAccessories(ItemID.TerrasparkBoots) &&
                             !Player.GetModPlayer<AvalonPlayer>().InertiaBoots && !Player.GetModPlayer<AvalonPlayer>().BlahWings)
                    {
                        Player.accRunSpeed = 6.75f;
                    }
                    else if (!Player.GetModPlayer<AvalonPlayer>().InertiaBoots && !Player.GetModPlayer<AvalonPlayer>().BlahWings)
                    {
                        if (!Player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
                        {
                            Player.accRunSpeed = 10.29f;
                        }
                        else
                        {
                            Player.accRunSpeed = 4.5f;
                        }
                        if (!Player.vortexStealthActive && !Player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
                        {
                            if ((Player.velocity.X < 4f && Player.controlRight) ||
                                (Player.velocity.X > -4f && Player.controlLeft))
                            {
                                Player.velocity.X = Player.velocity.X + (Player.controlRight ? 0.31f : -0.31f);
                            }
                            else if ((Player.velocity.X < 8f && Player.controlRight) ||
                                     (Player.velocity.X > -8f && Player.controlLeft))
                            {
                                Player.velocity.X = Player.velocity.X + (Player.controlRight ? 0.29f : -0.29f);
                            }
                        }
                    }
                    else
                    {
                        if (!Player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
                        {
                            Player.accRunSpeed = 14.29f;
                        }
                        else
                        {
                            Player.accRunSpeed = 6f;
                        }
                        if (!Player.vortexStealthActive && !Player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
                        {
                            if ((Player.velocity.X < 5f && Player.controlRight) ||
                                (Player.velocity.X > -5f && Player.controlLeft))
                            {
                                Player.velocity.X = Player.velocity.X + (Player.controlRight ? 0.41f : -0.41f);
                            }
                            else if ((Player.velocity.X < 14f && Player.controlRight) ||
                                     (Player.velocity.X > -14f && Player.controlLeft))
                            {
                                Player.velocity.X = Player.velocity.X + (Player.controlRight ? 0.39f : -0.39f);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (KeybindSystem.QuickStaminaHotkey.JustPressed)
        {
            QuickStamina();
        }

        //if (KeybindSystem.FlightTimeRestoreHotkey.JustPressed && Player.wingsLogic > 0 && Player.wingTime == 0 && FlightRestoreUnlocked && FlightRestoreCooldown >= 60 * 60)
        //{
        //    int amt = 150;
        //    if (StaminaDrain)
        //    {
        //        amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
        //    }
        //    if (StatStam >= amt)
        //    {
        //        StatStam -= amt;
        //        FlightRestoreCooldown = 0;
        //        Player.wingTime = Player.wingTimeMax;
        //    }
        //    else if (StamFlower)
        //    {
        //        QuickStamina(amt);
        //        if (StatStam >= amt)
        //        {
        //            StatStam -= amt;
        //            FlightRestoreCooldown = 0;
        //            Player.wingTime = Player.wingTimeMax;
        //        }
        //    }
        //}
    }
    public void WallslideMovement()
    {
        Player.sliding = false;
        if (Player.slideDir == 0 || Player.spikedBoots <= 0 || Player.mount.Active || ((!Player.controlLeft || Player.slideDir != -1) && (!Player.controlRight || Player.slideDir != 1)))
        {
            return;
        }
        if (Player.spikedBoots >= 2) return;
        bool doSliding = true;
        StaminaSlidingCD++;
        StaminaRegenCount = 0;
        if (StaminaSlidingCD >= (Player.HasItemInArmor(ItemID.ClimbingClaws) || Player.HasItemInArmor(ItemID.ShoeSpikes) ? 12 : 6))
        {
            int amt = 1;
            if (StaminaDrain)
            {
                amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
            }

            if (StatStam >= amt)
            {
                StatStam -= amt;
            }
            else if (StamFlower)
            {
                QuickStamina();
                if (StatStam >= amt)
                {
                    StatStam -= amt;
                }
            }

            if (StatStam <= 0)
            {
                StatStam = 0;
                doSliding = false;
            }

            StaminaSlidingCD = 0;
        }
        bool flag = false;
        float num = Player.position.X;
        if (Player.slideDir == 1)
        {
            num += Player.width;
        }
        num += Player.slideDir;
        float num2 = Player.position.Y + Player.height + 1f;
        if (Player.gravDir < 0f)
        {
            num2 = Player.position.Y - 1f;
        }
        num /= 16f;
        num2 /= 16f;
        if (WorldGen.SolidTile((int)num, (int)num2) && WorldGen.SolidTile((int)num, (int)num2 - 1))
        {
            flag = true;
        }
        // both
        if (Player.spikedBoots >= 2)
        {
            if (!flag || ((!(Player.velocity.Y > 0f) || Player.gravDir != 1f) && (!(Player.velocity.Y < Player.gravity) || Player.gravDir != -1f)))
            {
                return;
            }
            float num3 = Player.gravity;
            if (Player.slowFall)
            {
                num3 = ((!Player.TryingToHoverUp) ? (Player.gravity / 3f * Player.gravDir) : (Player.gravity / 10f * Player.gravDir));
            }
            Player.fallStart = (int)(Player.position.Y / 16f);
            if ((Player.controlDown && Player.gravDir == 1f) || (Player.controlUp && Player.gravDir == -1f))
            {
                Player.velocity.Y = 4f * Player.gravDir;
                int num4 = Dust.NewDust(new Vector2(Player.position.X + Player.width / 2 + (Player.width / 2 - 4) * Player.slideDir, Player.position.Y + Player.height / 2 + (Player.height / 2 - 4) * Player.gravDir), 8, 8, 31);
                if (Player.slideDir < 0)
                {
                    Main.dust[num4].position.X -= 10f;
                }
                if (Player.gravDir < 0f)
                {
                    Main.dust[num4].position.Y -= 12f;
                }
                Dust obj = Main.dust[num4];
                obj.velocity *= 0.1f;
                Main.dust[num4].scale *= 1.2f;
                Main.dust[num4].noGravity = true;
                Main.dust[num4].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
            }
            else if (Player.gravDir == -1f)
            {
                Player.velocity.Y = (0f - num3 + 1E-05f) * Player.gravDir;
            }
            else
            {
                Player.velocity.Y = (0f - num3 + 1E-05f) * Player.gravDir;
            }
            Player.sliding = true;
        }
        else if ((flag && Player.velocity.Y > 0.5 && Player.gravDir == 1f) || (Player.velocity.Y < -0.5 && Player.gravDir == -1f))
        {
            Player.fallStart = (int)(Player.position.Y / 16f);
            if (Player.controlDown)
            {
                Player.velocity.Y = 4f * Player.gravDir;
            }
            else
            {
                Player.velocity.Y = 0.5f * Player.gravDir;
            }
            Player.sliding = true;
            int num5 = Dust.NewDust(new Vector2(Player.position.X + Player.width / 2 + (Player.width / 2 - 4) * Player.slideDir, Player.position.Y + Player.height / 2 + (Player.height / 2 - 4) * Player.gravDir), 8, 8, 31);
            if (Player.slideDir < 0)
            {
                Main.dust[num5].position.X -= 10f;
            }
            if (Player.gravDir < 0f)
            {
                Main.dust[num5].position.Y -= 12f;
            }
            Dust obj2 = Main.dust[num5];
            obj2.velocity *= 0.1f;
            Main.dust[num5].scale *= 1.2f;
            Main.dust[num5].noGravity = true;
            Main.dust[num5].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
        }
    }
    public override void PostUpdateEquips()
    {
        bool flag = false;
        float num = Player.position.X;
        if (Player.slideDir == 1)
        {
            num += Player.width;
        }
        num += Player.slideDir;
        float num2 = Player.position.Y + Player.height + 1f;
        if (Player.gravDir < 0f)
        {
            num2 = Player.position.Y - 1f;
        }
        num /= 16f;
        num2 /= 16f;
        if (WorldGen.SolidTile((int)num, (int)num2) && WorldGen.SolidTile((int)num, (int)num2 - 1) && Player.velocity.Y > 0)
        {
            flag = true;
        }

        #region wall sliding (3 or 6 stamina per second)
        if (WallSlidingUnlocked && Player.spikedBoots <= 2 && flag)
        {
            bool doSliding = false;
            StaminaSlidingCD++;
            StaminaRegenCount = 0;

            // set the normal amount
            int amt = 1;

            if (StaminaDrain)
            {
                // multiply the amount relative to your stamina drain stacks
                amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
            }

            // if your current stamina is above the amount required, set the flag to true
            if (StatStam >= amt)
            {
                doSliding = true;
            }

            if (StaminaSlidingCD >= (Player.spikedBoots == 1 ? 20 : 10) && doSliding)
            {
                StatStam -= amt;
                if (StamFlower && StatStam < amt)
                {
                    QuickStamina();
                }

                if (StatStam <= 0)
                {
                    StatStam = 0;
                    doSliding = false;
                }

                StaminaSlidingCD = 0;
            }

            if (doSliding)
            {
                Player.spikedBoots++;
            }
        }
        #endregion

        #region mining speed (stamina consumed depends on current mining speed)
        int amt2 = 5 - (int)(1 - Player.pickSpeed * 5);
        if (MiningSpeedUnlocked && Player.HeldItem.pick > 0 && Player.ItemAnimationActive && Player.pickSpeed > 0.4f && StatStam > amt2)
        {
            bool doMining = false;
            StaminaMiningCD++;
            StaminaRegenCount = 0;

            int amt = 5 - (int)(1 - Player.pickSpeed * 5);
            if (StaminaDrain)
            {
                amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
            }
            if (StatStam >= amt)
            {
                doMining = true;
            }

            if (StaminaMiningCD >= 30 && doMining)
            {
                StatStam -= amt;
                if (StamFlower && StatStam < amt)
                {
                    QuickStamina();
                }

                if (StatStam <= 0)
                {
                    StatStam = 0;
                    doMining = false;
                }

                StaminaMiningCD = 0;
            }

            if (doMining)
            {
                Player.pickSpeed -= Player.pickSpeed * 0.15f;
                if (Player.pickSpeed < 0.4f)
                {
                    Player.pickSpeed = 0.4f;
                }
            }
        }
        #endregion
    }
    public override void UpdateLifeRegen()
    {
        UpdateStaminaRegen();
    }
    public override void SaveData(TagCompound tag)
    {
        tag["Avalon:Stamina"] = StatStamMax;
        tag["Avalon:EnergyCrystal"] = EnergyCrystal;
    }
    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("Avalon:Stamina"))
        {
            StatStamMax = tag.GetAsInt("Avalon:Stamina");
        }
        if (tag.ContainsKey("Avalon:EnergyCrystal"))
        {
            EnergyCrystal = tag.Get<bool>("Avalon:EnergyCrystal");
        }
    }
    public void StaminaHealEffect(int healAmount, bool broadcast = true)
    {
        CombatText.NewText(Player.getRect(), new Color(5, 200, 255, 255), string.Concat(healAmount), false, false);
        if (broadcast && Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
        {
            ModPacket packet = Network.MessageHandler.GetPacket(Network.MessageID.StaminaHeal);
            packet.Write(Player.whoAmI);
            packet.Write(healAmount);
            packet.Send();
        }
    }
    public void UpdateStaminaRegen()
    {
        if (StamRegenDelay > 0)
        {
            StamRegenDelay--;
            if ((Player.velocity.X == 0f && Player.velocity.Y == 0f) || Player.grappling[0] >= 0)
            {
                StamRegenDelay--;
            }
        }
        if (StamRegenDelay <= 0)
        {
            StamRegenDelay = 0;
            StaminaRegen = StatStamMax2 / 7 + 1;
            if ((Player.velocity.X == 0f && Player.velocity.Y == 0f) || Player.grappling[0] >= 0)
            {
                StaminaRegen += StatStamMax2 / 2;
            }
            float num = StatStam / (float)StatStamMax2 * 0.8f + 0.2f;
            StaminaRegen = (int)(StaminaRegen * num * 1.15);
        }
        else
        {
            StaminaRegen = 0;
        }
        StaminaRegenCount += StaminaRegen;

        while (StaminaRegenCount >= StaminaRegenCost)
        {
            bool flag = false;
            StaminaRegenCount -= StaminaRegenCost;
            if (StatStam < StatStamMax2)
            {
                StatStam++;
                flag = true;
            }
            if (StatStam >= StatStamMax2)
            {
                if (Player.whoAmI == Main.myPlayer && flag)
                {
                    SoundEngine.PlaySound(SoundID.MaxMana);
                    for (int i = 0; i < 5; i++)
                    {
                        int num2 = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<StaminaRegeneration>(), 0f, 0f, 255, default(Color), Main.rand.Next(20, 26) * 0.1f);
                        Main.dust[num2].noLight = true;
                        Main.dust[num2].noGravity = true;
                        Main.dust[num2].velocity *= 0.5f;
                    }
                }
                StatStam = StatStamMax2;
            }
        }
    }
}
