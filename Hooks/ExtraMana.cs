using System;
using System.Reflection;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class ExtraMana : ModHook {
    private const int LowManaTier = 2;
    private const int ManaPerCrystal = 20;
    private const int MaxManaCrystalsToDisplay = 10;
    private const int MaxManaToDisplay = MaxManaCrystalsToDisplay * ManaPerCrystal;
    private const int TopManaTier = 6;

    private static readonly Func<HorizontalBarsPlayerResourcesDisplaySet, int> GetMPSegmentsCount =
        Utilities.CreateInstancePropertyOrFieldReaderDelegate<HorizontalBarsPlayerResourcesDisplaySet, int>(
            "_mpSegmentsCount");

    protected override void Apply() {
        On_PlayerStatsSnapshot.ctor += OnPlayerStatsSnapshotCtor;
        On_FancyClassicPlayerResourcesDisplaySet.PrepareFields += OnFancyClassicPrepareFields;
        //IL_ClassicPlayerResourcesDisplaySet.DrawMana += ILClassicDrawMana;
        IL_FancyClassicPlayerResourcesDisplaySet.StarFillingDrawer +=
            ILStarFillingDrawer;
        On_HorizontalBarsPlayerResourcesDisplaySet.ManaFillingDrawer += OnManaFillingDrawer;
    }

    private static void OnPlayerStatsSnapshotCtor(
        On_PlayerStatsSnapshot.orig_ctor orig,
        ref PlayerStatsSnapshot self,
        Player player) {
        orig(ref self, player);

        if (self.ManaMax > MaxManaToDisplay) {
            FieldInfo? field =
                typeof(PlayerStatsSnapshot).GetField("numManaStars", BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(self, player.statManaMax2 / MaxManaCrystalsToDisplay);
        }
    }


    private static int RoundToNearest10(int num) {
        float original = num / 10f;
        if (original > 1f && original <= 1.5f) {
            original = 1f;
        }
        else if (original > 1.5f && original < 2f) {
            original = 2f;
        }

        return (int)(original * 10f);
    }


    private static void OnFancyClassicPrepareFields(On_FancyClassicPlayerResourcesDisplaySet.orig_PrepareFields orig,
                                                    FancyClassicPlayerResourcesDisplaySet self, Player player) {
        orig(self, player);
        FieldInfo? snapshot = typeof(FancyClassicPlayerResourcesDisplaySet).GetField("preparedSnapshot",
            BindingFlags.Instance | BindingFlags.NonPublic);
        if (player.statManaMax2 > MaxManaToDisplay) {
            var snap = (PlayerStatsSnapshot)snapshot.GetValue(self);
            FieldInfo? field =
                typeof(FancyClassicPlayerResourcesDisplaySet).GetField("_starCount",
                    BindingFlags.Instance | BindingFlags.NonPublic);
            int val = snap.ManaMax / (MaxManaCrystalsToDisplay * (snap.ManaMax / 100));
            if (val > 10) {
                val = RoundToNearest10(val);
            }

            field.SetValue(self, val);
            snapshot.SetValue(self, snap);
        }
    }

    private static void ILClassicDrawMana(ILContext il) {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdcI4(20))
            .GotoNext(i => i.MatchStfld(out _));

        c.EmitDelegate<Func<int, int>>(val => {
            if (Main.LocalPlayer.statManaMax2 > MaxManaToDisplay) {
                return Main.LocalPlayer.statManaMax2 / MaxManaCrystalsToDisplay;
            }

            return val;
        });

        c.GotoNext(i => i.MatchLdcR8(0.9))
            .GotoNext(i => i.MatchLdsfld(out _))
            .GotoNext(i => i.MatchCallvirt(out _))
            .Emit(OpCodes.Ldloc, 6);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, index) => {
            for (int i = TopManaTier; i >= LowManaTier; i--) {
                if (index - 1 < (Main.LocalPlayer.statManaMax2 - MaxManaToDisplay * (i - 1)) / ManaPerCrystal) {
                    return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
                        $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Mana{i}");
                }
            }

            return sprite;
        });
    }

    private static void ILStarFillingDrawer(ILContext il) {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdfld(out _))
            .GotoNext(i => i.MatchStindRef())
            .Emit(OpCodes.Ldarg, 1);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, index) => {
            for (int i = TopManaTier; i >= LowManaTier; i--) {
                if (index < (Main.LocalPlayer.statManaMax2 - MaxManaToDisplay * (i - 1)) / ManaPerCrystal) {
                    return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
                        $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/FancyMana{i}");
                }
            }

            return sprite;
        });
    }

    private static void OnManaFillingDrawer(On_HorizontalBarsPlayerResourcesDisplaySet.orig_ManaFillingDrawer orig,
                                            HorizontalBarsPlayerResourcesDisplaySet self, int elementIndex,
                                            int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite,
                                            out Vector2 offset, out float drawScale, out Rectangle? sourceRect) {
        orig(self, elementIndex, firstElementIndex, lastElementIndex, out sprite, out offset, out drawScale,
            out sourceRect);

        int mpSegmentsCount = GetMPSegmentsCount(self);
        int manaTier = 1 + mpSegmentsCount / MaxManaCrystalsToDisplay;

        if (manaTier > 1) {
            sprite = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
                $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/BarMana{manaTier}", AssetRequestMode.ImmediateLoad);
        }
    }
}
