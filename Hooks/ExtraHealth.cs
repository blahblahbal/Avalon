using System;
using Avalon.Common;
using Avalon.Items.Accessories.Vanity;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class ExtraHealth : ModHook
{
    protected override void Apply()
    {
        //IL_ClassicPlayerResourcesDisplaySet.DrawLife += ILDrawLife;
        //IL_HorizontalBarsPlayerResourcesDisplaySet.LifeFillingDrawer += ILLifeFillingDrawer;
        //IL_FancyClassicPlayerResourcesDisplaySet.HeartFillingDrawer += ILHeartFillingDrawer;
    }

    private static void ILDrawLife(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdcR8(0.9))
            .GotoNext(i => i.MatchLdsfld(out _))
            .GotoNext(i => i.MatchCallvirt(out _))
            .Emit(OpCodes.Ldloc, 10);

        c.EmitDelegate<Func<Asset<Texture2D>, int, Asset<Texture2D>>>((sprite, index) =>
        {
			int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
			if (slot != -1)
			{
				if (Main.LocalPlayer.dye[slot].type != ItemID.None)
				{
					GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
					return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/DyeableHeart");
				}
			}

			int crystalFruitCount = (Main.LocalPlayer.statLifeMax - 500) / 5;
            if (index - 1 < crystalFruitCount)
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

        c.GotoNext(i => i.MatchLdarg(1))
            .GotoNext(i => i.MatchLdarg(0))
            .GotoNext(i => i.MatchLdfld(out hpSegmentsCountField))
            .GotoNext(i => i.MatchBlt(out _))
            .GotoNext(i => i.MatchStindRef())
            .Emit(OpCodes.Ldarg_1)
            .Emit(OpCodes.Ldarg_0)
            .Emit(OpCodes.Ldfld, hpSegmentsCountField);

        c.EmitDelegate<Func<Asset<Texture2D>, int, int, Asset<Texture2D>>>((sprite, elementIndex, hpSegmentsCount) =>
        {
			int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
			if (slot != -1)
			{
				if (Main.LocalPlayer.dye[slot].type != ItemID.None)
				{
					GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
					return ExxoAvalonOrigins.Mod.Assets.Request<Texture2D>($"{ExxoAvalonOrigins.TextureAssetsPath}/UI/Dyeable_HP_Fill");
				}
			}

			int crystalFruitSegments = (Main.LocalPlayer.statLifeMax - 500) / 5;
            if (elementIndex >= hpSegmentsCount - crystalFruitSegments)
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
