using Avalon.Common;
using Avalon.Items.Accessories.Vanity;
using Avalon.Items.Weapons.Melee.SolarSystem;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using System.Reflection;
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
		IL_ClassicPlayerResourcesDisplaySet.DrawLife += ILDrawLife;
		//IL_HorizontalBarsPlayerResourcesDisplaySet.LifeFillingDrawer += ILLifeFillingDrawer;
		//IL_FancyClassicPlayerResourcesDisplaySet.HeartFillingDrawer += ILHeartFillingDrawer;
	}

    private static void ILDrawLife(ILContext il)
    {
        var c = new ILCursor(il);

		int index_varNum = -1;
		int snapshot_varNum = -1;

		c.GotoNext(MoveType.After, i => i.MatchLdloc(out snapshot_varNum), i => i.MatchLdarg(0), i => i.MatchLdloc(out index_varNum), i => i.MatchLdcI4(1), i => i.MatchSub(), i => i.MatchLdloc(out _));
		c.EmitLdloc(index_varNum);
		c.EmitLdloc(snapshot_varNum);
		c.EmitDelegate((Asset<Texture2D> sprite, int i, PlayerStatsSnapshot snapshot) =>
		{
			int slot = Main.LocalPlayer.ReturnEquippedDyeInSlot(ModContent.ItemType<ResourceBarSkin>());
			if (slot != -1)
			{
				if (Main.LocalPlayer.dye[slot].type != ItemID.None)
				{
					GameShaders.Armor.GetSecondaryShader(Main.LocalPlayer.dye[slot].dye, Main.LocalPlayer).Apply();
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
