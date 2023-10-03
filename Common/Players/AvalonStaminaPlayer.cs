using Avalon.Buffs;
using Avalon.Dusts;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
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
    public bool RocketJumpUnlocked = false;
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
    public byte StaminaCooldown;
    public byte StaminaSprintCooldown;


    public bool ActivateSprint;
    public bool ActivateSwim;
    public bool ActivateSlide;
    public bool ActivateRocketJump;
    public bool StamDashKey;

    public bool SwimmingUnlocked;
    public bool TeleportUnlocked;
    public bool RollDodgeUnlocked;

    public override void ResetEffects()
    {
        StaminaDrain = false;
        StamFlower = false;
        if (EnergyCrystal) { StaminaRegen = 800; StaminaRegenCost = 800; }
        else { StaminaRegen = 1000; StaminaRegenCost = 1000; }
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
        // swimming
        if (Player.wet && Player.velocity != Vector2.Zero && !Player.accMerman && ActivateSwim &&
            SwimmingUnlocked)
        {
            bool flag15 = true;
            StaminaCooldown++;
            StaminaRegenCount = 0;
            if (StaminaCooldown >= 10)
            {
                Player.ConsumeStamina(1);
                //int amt = 1;
                //if (StaminaDrain)
                //{
                //    amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
                //}

                //if (StatStam >= amt)
                //{
                //    StatStam -= amt;
                //}
                //else if (StamFlower)
                //{
                //    QuickStamina();
                //    if (StatStam >= amt)
                //    {
                //        StatStam -= amt;
                //    }
                //}

                if (StatStam <= 0)
                {
                    StatStam = 0;
                    flag15 = false;
                }

                StaminaCooldown = 0;
            }

            if (flag15)
            {
                Player.accFlipper = true;
            }
        }
        // sprinting
        /*if (ActivateSprint)
        {
            if ((Player.controlRight || Player.controlLeft) && Player.velocity.X != 0f)
            {
                bool flag17 = true;
                StaminaSprintCooldown++;
                Player.GetModPlayer<AvalonStaminaPlayer>().StaminaRegenCount = 0;
                if (StaminaSprintCooldown >= 30)
                {
                    int amt = 10;
                    if (Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrain)
                    {
                        amt *= (int)(Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks *
                                     Player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainMult);
                    }

                    if (Player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
                    {
                        Player.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
                    }
                    else if (Player.GetModPlayer<AvalonStaminaPlayer>().StamFlower)
                    {
                        Player.GetModPlayer<AvalonStaminaPlayer>().QuickStamina();
                        if (Player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= amt)
                        {
                            Player.GetModPlayer<AvalonStaminaPlayer>().StatStam -= amt;
                        }
                    }

                    if (Player.GetModPlayer<AvalonStaminaPlayer>().StatStam <= 0)
                    {
                        Player.GetModPlayer<AvalonStaminaPlayer>().StatStam = 0;
                        flag17 = false;
                    }

                    StaminaSprintCooldown = 0;
                }

                if (flag17)
                {
                    if (!Player.HasItemInArmor(ItemID.HermesBoots) && !Player.HasItemInArmor(ItemID.FlurryBoots) &&
                        !Player.HasItemInArmor(ItemID.SpectreBoots) &&
                        !Player.HasItemInArmor(ItemID.LightningBoots) &&
                        !Player.HasItemInArmor(ItemID.FrostsparkBoots) &&
                        !Player.HasItemInArmor(ItemID.SailfishBoots) &&
                        !Player.GetModPlayer<ExxoEquipEffectPlayer>().InertiaBoots && !Player.GetModPlayer<ExxoEquipEffectPlayer>().BlahWings)
                    {
                        Player.accRunSpeed = 6f;
                    }
                    else if (!Player.HasItemInArmor(ItemID.LightningBoots) &&
                             !Player.HasItemInArmor(ItemID.FrostsparkBoots) &&
                             !Player.GetModPlayer<ExxoEquipEffectPlayer>().InertiaBoots && !Player.GetModPlayer<ExxoEquipEffectPlayer>().BlahWings)
                    {
                        Player.accRunSpeed = 6.75f;
                    }
                    else if (!Player.GetModPlayer<ExxoEquipEffectPlayer>().InertiaBoots && !Player.GetModPlayer<ExxoEquipEffectPlayer>().BlahWings)
                    {
                        Player.accRunSpeed = 10.29f;
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
                    else
                    {
                        Player.accRunSpeed = 14.29f;
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
        }*/
    }
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (KeybindSystem.QuickStaminaHotkey.JustPressed)
        {
            QuickStamina();
        }
        if (KeybindSystem.RocketJumpHotkey.JustPressed && RocketJumpUnlocked)
        {
            ActivateRocketJump = !ActivateRocketJump;
            Main.NewText(!ActivateRocketJump ? "Rocket Jump Off" : "Rocket Jump On");
        }

        if (KeybindSystem.SprintHotkey.JustPressed && SprintUnlocked)
        {
            ActivateSprint = !ActivateSprint;
            Main.NewText(!ActivateSprint ? "Sprinting Off" : "Sprinting On");
        }

        if (KeybindSystem.DashHotkey.JustPressed)
        {
            StamDashKey = !StamDashKey;
            Main.NewText(!StamDashKey ? "Dashing Off" : "Dashing On");
        }

        if (KeybindSystem.FlightTimeRestoreHotkey.JustPressed && Player.wingsLogic > 0 && Player.wingTime == 0 && FlightRestoreUnlocked && FlightRestoreCooldown >= 60 * 60)
        {
            int amt = 150;
            if (StaminaDrain)
            {
                amt *= (int)(StaminaDrainStacks * StaminaDrainMult);
            }
            if (StatStam >= amt)
            {
                StatStam -= amt;
                FlightRestoreCooldown = 0;
                Player.wingTime = Player.wingTimeMax;
            }
            else if (StamFlower)
            {
                QuickStamina(amt);
                if (StatStam >= amt)
                {
                    StatStam -= amt;
                    FlightRestoreCooldown = 0;
                    Player.wingTime = Player.wingTimeMax;
                }
            }
        }
    }
    public override void UpdateLifeRegen()
    {
        UpdateStaminaRegen();
    }
    public override void SaveData(TagCompound tag)
    {
        tag["Avalon:Stamina"] = StatStamMax;
        tag["Avalon:RocketJumpUnlocked"] = RocketJumpUnlocked;
        tag["Avalon:TeleportUnlocked"] = TeleportUnlocked;
        tag["Avalon:SwimmingUnlocked"] = SwimmingUnlocked;
        tag["Avalon:SprintUnlocked"] = SprintUnlocked;
        tag["Avalon:FlightRestoreUnlocked"] = FlightRestoreUnlocked;
        tag["Avalon:EnergyCrystal"] = EnergyCrystal;
    }
    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("Avalon:Stamina"))
        {
            StatStamMax = tag.GetAsInt("Avalon:Stamina");
        }
        if (tag.ContainsKey("Avalon:RocketJumpUnlocked"))
        {
            RocketJumpUnlocked = tag.Get<bool>("Avalon:RocketJumpUnlocked");
        }
        if (tag.ContainsKey("Avalon:TeleportUnlocked"))
        {
            TeleportUnlocked = tag.Get<bool>("Avalon:TeleportUnlocked");
        }
        if (tag.ContainsKey("Avalon:SwimmingUnlocked"))
        {
            SwimmingUnlocked = tag.Get<bool>("Avalon:SwimmingUnlocked");
        }
        if (tag.ContainsKey("Avalon:SprintUnlocked"))
        {
            SprintUnlocked = tag.Get<bool>("Avalon:SprintUnlocked");
        }
        if (tag.ContainsKey("Avalon:FlightRestoreUnlocked"))
        {
            FlightRestoreUnlocked = tag.Get<bool>("Avalon:FlightRestoreUnlocked");
        }
        if (tag.ContainsKey("Avalon:EnergyCrystal"))
        {
            EnergyCrystal = tag.Get<bool>("Avalon:EnergyCrystal");
        }
    }
    public void StaminaHealEffect(int healAmount, bool broadcast = true)
    {
        CombatText.NewText(Player.getRect(), new Color(5, 200, 255, 255), string.Concat(healAmount), false, false);
        if (broadcast && Main.netMode == 1 && Player.whoAmI == Main.myPlayer)
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
