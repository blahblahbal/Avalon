using System;
using Avalon.Common;
using Avalon.Items.Accessories.Vanity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.NPCs;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class ExtraMana : ModHook {
    private const int ManaPerCrystal = 20;
    private const int MaxManaTier = 6;

    protected override void Apply() {
        On_PlayerStatsSnapshot.ctor += OnPlayerStatsSnapshotCtor;
        On_FancyClassicPlayerResourcesDisplaySet.StarFillingDrawer += OnStarFillingDrawer;
        On_HorizontalBarsPlayerResourcesDisplaySet.ManaFillingDrawer += OnManaFillingDrawer;
        IL_ClassicPlayerResourcesDisplaySet.DrawMana += ILClassicDrawMana;

		On_HorizontalBarsPlayerResourcesDisplaySet.Draw += On_HorizontalBarsPlayerResourcesDisplaySet_Draw;
		On_FancyClassicPlayerResourcesDisplaySet.Draw += On_FancyClassicPlayerResourcesDisplaySet_Draw;
		On_ClassicPlayerResourcesDisplaySet.Draw += On_ClassicPlayerResourcesDisplaySet_Draw;
    }

	private void On_ClassicPlayerResourcesDisplaySet_Draw(On_ClassicPlayerResourcesDisplaySet.orig_Draw orig, ClassicPlayerResourcesDisplaySet self)
	{
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
		orig.Invoke(self);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
	}

	private void On_FancyClassicPlayerResourcesDisplaySet_Draw(On_FancyClassicPlayerResourcesDisplaySet.orig_Draw orig, FancyClassicPlayerResourcesDisplaySet self)
	{
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
		orig.Invoke(self);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
	}

	private void On_HorizontalBarsPlayerResourcesDisplaySet_Draw(On_HorizontalBarsPlayerResourcesDisplaySet.orig_Draw orig, HorizontalBarsPlayerResourcesDisplaySet self)
	{
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
		orig.Invoke(self);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
	}

	private void OnPlayerStatsSnapshotCtor(On_PlayerStatsSnapshot.orig_ctor orig, ref PlayerStatsSnapshot self,
                                           Player player) {
        orig(ref self, player);
        self.AmountOfManaStars = Math.Min(self.AmountOfManaStars, 10);
    }

    private static int GetManaTier(int lastElementIndex, int elementIndex) {
        int maxManaCrystals = lastElementIndex + 1;
        int baseTier = Main.LocalPlayer.statManaMax2 / (maxManaCrystals * ManaPerCrystal);
        int manaAboveTier = Main.LocalPlayer.statManaMax2 % (maxManaCrystals * ManaPerCrystal);
        int manaTier = baseTier + Convert.ToInt32(manaAboveTier - elementIndex * ManaPerCrystal > 0);
        return Math.Min(manaTier, MaxManaTier);
    }

    private static void ILClassicDrawMana(ILContext il) {
        var cursor = new ILCursor(il);

        cursor.GotoNext(instruction => instruction.MatchLdsfld(typeof(TextureAssets), nameof(TextureAssets.Mana)));
        cursor.GotoNext(instruction => instruction.MatchCall<ResourceOverlayDrawContext>(".ctor"));

        // i
        cursor.Emit(OpCodes.Ldloc, 8);

        // this.UIDisplay_ManaPerStar
        cursor.Emit(OpCodes.Ldarg, 0);
        cursor.Emit<ClassicPlayerResourcesDisplaySet>(OpCodes.Ldfld, "UIDisplay_ManaPerStar");

        cursor.EmitDelegate((Asset<Texture2D> origManaTexture, int elementIndex, float manaPerStar) => {
            elementIndex--;

            int lastElementIndex = Main.LocalPlayer.statManaMax2 / (int)manaPerStar - 1;

            int manaTier = GetManaTier(lastElementIndex, elementIndex);

			int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
			if (slot != -1)
			{
				if (Main.LocalPlayer.dye[slot].type != ItemID.None)
				{
					GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
					return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
						$"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Mana{manaTier}", AssetRequestMode.ImmediateLoad);
				}
			}

			if (manaTier > 1) {
                return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
                    $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Mana{manaTier}", AssetRequestMode.ImmediateLoad);
            }

            return origManaTexture;
        });
    }

    private static void OnStarFillingDrawer(On_FancyClassicPlayerResourcesDisplaySet.orig_StarFillingDrawer orig,
                                            FancyClassicPlayerResourcesDisplaySet self, int elementIndex,
                                            int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite,
                                            out Vector2 offset, out float drawScale, out Rectangle? sourceRect) {
        orig(self, elementIndex, firstElementIndex, lastElementIndex, out sprite, out offset, out drawScale,
            out sourceRect);

        int manaTier = GetManaTier(lastElementIndex, elementIndex);
		int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
		if (slot != -1)
		{
			if (Main.LocalPlayer.dye[slot].type != ItemID.None)
			{
				GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
			}
		}

		if (manaTier > 1) {
            sprite = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
                $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/FancyMana{manaTier}", AssetRequestMode.ImmediateLoad);
        }

		
	}

    private static void OnManaFillingDrawer(On_HorizontalBarsPlayerResourcesDisplaySet.orig_ManaFillingDrawer orig,
                                            HorizontalBarsPlayerResourcesDisplaySet self, int elementIndex,
                                            int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite,
                                            out Vector2 offset, out float drawScale, out Rectangle? sourceRect) {
        orig(self, elementIndex, firstElementIndex, lastElementIndex, out sprite, out offset, out drawScale,
            out sourceRect);

		int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
		if (slot != -1)
		{
			if (Main.LocalPlayer.dye[slot].type != ItemID.None)
			{
				GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
			}
		}

		int manaTier = GetManaTier(lastElementIndex, elementIndex);
		if (manaTier > 1) {
            sprite = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
                $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/BarMana{manaTier}", AssetRequestMode.ImmediateLoad);
        }
	}
}
