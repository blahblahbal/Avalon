using Avalon.Common;
using Avalon.Items.Accessories.Vanity;
using Avalon.Items.Weapons.Melee.SolarSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

//This is like, the gayest thing ever
internal class SpriteBatchSettingsSaver
{
	private SpriteSortMode sortMode;

	private BlendState blendState;

	private SamplerState samplerState;

	private DepthStencilState depth;

	private RasterizerState rasterizerState;

	private Effect effect;

	private Matrix matrix;

	internal SpriteBatchSettingsSaver(SpriteSortMode mode, BlendState state, SamplerState sample, DepthStencilState stencil, RasterizerState rasterizer, Effect effects, Matrix matricies)
	{
		sortMode = mode;
		blendState = state;
		samplerState = sample;
		depth = stencil;
		rasterizerState = rasterizer;
		effect = effects;
		matrix = matricies;
	}

	public void GetSettings(out SpriteSortMode mode, out BlendState state, out SamplerState sample, out DepthStencilState stencil, out RasterizerState rasterizer, out Effect effects, out Matrix matricies)
	{
		mode = sortMode;
		state = blendState;
		sample = samplerState;
		stencil = depth;
		rasterizer = rasterizerState;
		effects = effect;
		matricies = matrix;
	}
}

[Autoload(Side = ModSide.Client)]
public class ExtraHealth : ModHook
{
    protected override void Apply()
    {
		IL_ClassicPlayerResourcesDisplaySet.DrawLife += ILDrawLife;

		//IL_HorizontalBarsPlayerResourcesDisplaySet.Draw += HorizontalBarDyeing;
		//IL_HorizontalBarsPlayerResourcesDisplaySet.LifeFillingDrawer += ILLifeFillingDrawer;
		
		//IL_FancyClassicPlayerResourcesDisplaySet.HeartFillingDrawer += ILHeartFillingDrawer;
	}

	private void HorizontalBarDyeing(ILContext il)
	{
		ILCursor c = new(il);

		int indexTest = -1;
		c.GotoNext(MoveType.After, i => i.MatchLdsfld<Main>(nameof(Main.spriteBatch)), i => i.MatchStloc(out indexTest));

		//loops once for life, twice for mana
		for (int j = 0; j < 2; j++)
		{
			c.GotoNext(MoveType.Before, i => i.MatchLdloca(out _), i => i.MatchLdloc(out _), i => i.MatchLdloca(out _), i => i.MatchCall<ResourceDrawSettings>(nameof(ResourceDrawSettings.Draw)), i => i.MatchLdarg(0), i => i.MatchLdloc(out _));
			c.EmitLdarg(0);
			c.EmitLdloca(indexTest);
			c.EmitDelegate((HorizontalBarsPlayerResourcesDisplaySet self, ref SpriteBatch spriteBatch) =>
			{
				int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
				if (slot != -1)
				{
					if (Main.LocalPlayer.dye[slot].type != ItemID.None)
					{
						ArmorShaderData data = GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer);
						//data.Apply(Main.LocalPlayer, new Terraria.DataStructures.DrawData { texture = sprite.Value, effect = (SpriteEffects)0, rotation = 0f, position = position });
					}
				}

				int num2 = 18;
				int num3 = Main.screenWidth - 300 - 22 + 16;
				if (self._drawTextStyle == 2)
				{
					num2 += 2;
				}
				else if (self._drawTextStyle == 1)
				{
					num2 += 4;
				}
				if (j == 1)
				{
					Vector2 vector = new((float)num3, (float)num2);
					vector.X += (self._maxSegmentCount - self._hpSegmentsCount) * self._panelMiddleHP.Width();
					bool isHovered = false;
					ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
					resourceDrawSettings.ElementCount = self._hpSegmentsCount + 2;
					resourceDrawSettings.ElementIndexOffset = 0;
					resourceDrawSettings.TopLeftAnchor = vector;
					resourceDrawSettings.GetTextureMethod = ((int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect) =>
					{
						sourceRect = null;
						offset = Vector2.Zero;
						sprite = self._panelLeft;
						drawScale = 0f;
						if (elementIndex == lastElementIndex)
						{
							drawScale = 1f;
							sprite = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/HP_Panel_Right_Overlay");
							offset = new Vector2(-16f, -10f);
						}
						else if (elementIndex != firstElementIndex)
						{
							sprite = self._panelMiddleHP;
							int drawIndexOffset = lastElementIndex - (elementIndex - firstElementIndex) - elementIndex;
							offset.X = drawIndexOffset * self._panelMiddleHP.Width();
						}
					});
					resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
					resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
					resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
					resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
					resourceDrawSettings.StatsSnapshot = self.preparedSnapshot;
					resourceDrawSettings.DisplaySet = self;
					resourceDrawSettings.ResourceIndexOffset = -1;
					resourceDrawSettings.Draw(Main.spriteBatch, ref isHovered);
				}
				else
				{
					bool isHovered = false;
					Vector2 vector2 = new((float)(num3 - 10), (float)(num2 + 24));
					vector2.X += (self._maxSegmentCount - self._mpSegmentsCount) * self._panelMiddleMP.Width();
					ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
					resourceDrawSettings.ElementCount = self._mpSegmentsCount + 2;
					resourceDrawSettings.ElementIndexOffset = 0;
					resourceDrawSettings.TopLeftAnchor = vector2;
					resourceDrawSettings.GetTextureMethod = ((int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect) =>
					{
						sourceRect = null;
						offset = Vector2.Zero;
						sprite = self._panelLeft;
						drawScale = 0f;
						if (elementIndex == lastElementIndex)
						{
							drawScale = 1f;
							sprite = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/MP_Panel_Right_Overlay");
							offset = new Vector2(-16f, -6f);
						}
						else if (elementIndex != firstElementIndex)
						{
							sprite = self._panelMiddleMP;
							int drawIndexOffset = lastElementIndex - (elementIndex - firstElementIndex) - elementIndex;
							offset.X = drawIndexOffset * self._panelMiddleMP.Width();
						}
					});
					resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
					resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
					resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
					resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
					resourceDrawSettings.StatsSnapshot = self.preparedSnapshot;
					resourceDrawSettings.DisplaySet = self;
					resourceDrawSettings.ResourceIndexOffset = -1;
					resourceDrawSettings.Draw(Main.spriteBatch, ref isHovered);
				}
			});
		}
	}

	private void test(On_HorizontalBarsPlayerResourcesDisplaySet.orig_LifeFillingDrawer orig, HorizontalBarsPlayerResourcesDisplaySet self, int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Microsoft.Xna.Framework.Vector2 offset, out float drawScale, out Microsoft.Xna.Framework.Rectangle? sourceRect)
	{
		orig.Invoke(self, elementIndex, firstElementIndex, lastElementIndex, out sprite, out offset, out drawScale, out sourceRect);

		int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
		if (slot != -1)
		{
			//if (Main.LocalPlayer.dye[slot].type != ItemID.None)
			//{
			//	GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
			//}
		}

		int crystalFruitSegments = (Main.LocalPlayer.statLifeMax - 500) / 5; //needs to have the crystal rewritten to have its own use field 
		if (elementIndex >= /*hpSegmentsCount - */crystalFruitSegments)
		{
			sprite = ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/HP_Fill_Crystal");
		}
	}

	private static void ILDrawLife(ILContext il)
    {
        var c = new ILCursor(il);

		int index_varNum = -1;
		int snapshot_varNum = -1;
		int position_varNum = -1;

		c.GotoNext(MoveType.After, i => i.MatchLdloca(out position_varNum), i => i.MatchLdloc(out snapshot_varNum), i => i.MatchLdarg(0), i => i.MatchLdloc(out index_varNum), i => i.MatchLdcI4(1), i => i.MatchSub(), i => i.MatchLdloc(out _));
		c.EmitLdloc(index_varNum);
		c.EmitLdloc(snapshot_varNum);
		c.EmitLdloc(position_varNum);
		c.EmitDelegate((Asset<Texture2D> sprite, int i, PlayerStatsSnapshot snapshot, Vector2 position) =>
		{
			int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
			if (slot != -1)
			{
				if (Main.LocalPlayer.dye[slot].type != ItemID.None)
				{
					ArmorShaderData data = GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer);
					data.Apply(Main.LocalPlayer, new Terraria.DataStructures.DrawData { texture = sprite.Value, effect = (SpriteEffects)0, rotation = 0f, position = position });
				}
			}

			int crystalFruitCount = (Main.LocalPlayer.statLifeMax - 500) / 5;
			if (i - 1 < crystalFruitCount)
			{
				return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Heart3");
			}

			return sprite;
		});
    }

	private static void ILLifeFillingDrawer(ILContext il)
    {
        var c = new ILCursor(il);

        FieldReference? hpSegmentsCountField = null;

		c.GotoNext(i => i.MatchLdarg(1));
		c.GotoNext(i => i.MatchLdarg(0));
		c.GotoNext(i => i.MatchLdfld(out hpSegmentsCountField));
		c.GotoNext(i => i.MatchBlt(out _));
		c.GotoNext(i => i.MatchStindRef());
		c.Emit(OpCodes.Ldarg_1);
		c.Emit(OpCodes.Ldarg_0);
        c.Emit(OpCodes.Ldfld, hpSegmentsCountField);

        c.EmitDelegate((Asset<Texture2D> sprite, int elementIndex, int hpSegmentsCount) =>
        {
			int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
			if (slot != -1)
			{
				if (Main.LocalPlayer.dye[slot].type != ItemID.None)
				{
					GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
				}
			}

			int crystalFruitSegments = (Main.LocalPlayer.statLifeMax - 500) / 5; //needs to have the crystal rewritten to have its own use field 
            if (elementIndex >= /*hpSegmentsCount - */crystalFruitSegments)
            {
                return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
                    $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/HP_Fill_Crystal");
            }

            return sprite;
        });
    }

    private static void ILHeartFillingDrawer(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchBge(out _))
            .GotoNext(i => i.MatchStindRef())
            .Emit(OpCodes.Ldarg_1);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, elementIndex) =>
        {
			int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
			if (slot != -1)
			{
				if (Main.LocalPlayer.dye[slot].type != ItemID.None)
				{
					GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
					return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/DyeableFancyHeart");
				}
			}

			int crystalFruitCount = (Main.LocalPlayer.statLifeMax - 500) / 5;
            if (elementIndex < crystalFruitCount)
            {
                return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>(
                    $"{ExxoAvalonOrigins.TextureAssetsPath}/UI/FancyBlueHeart");
            }

            return sprite;
        });
    }
}
