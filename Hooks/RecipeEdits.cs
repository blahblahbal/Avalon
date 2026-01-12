using Avalon.Common;
using Avalon.Items.Material.Ores;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;
public class RecipeEdits : ModHook
{
	protected override void Apply()
	{
		IL_Recipe.CollectGuideRecipes += RemoveRescipeVisually;
		IL_Main.DrawGuideCraftText += DontShowItemConditions;
	}

	private void DontShowItemConditions(ILContext il)
	{
		ILCursor c = new(il);
		ILLabel IL_0000 = c.DefineLabel();
		int recipe_numvar = -1;

		c.GotoNext(MoveType.After, i => i.MatchCallvirt<List<string>>(nameof(List<string>.AddRange)));
		c.MarkLabel(IL_0000);
		c.GotoPrev(MoveType.Before, i => i.MatchLdloc(out recipe_numvar), i => i.MatchLdfld<Recipe>(nameof(Recipe.Conditions)));
		c.EmitLdloc(recipe_numvar);
		c.EmitDelegate((List<string> _requiredObjecsForCraftingText, Recipe recipe) =>
		{
			return (recipe.HasResult(ModContent.ItemType<Heartstone>()) && recipe.HasIngredient(ItemID.LifeCrystal)) || (recipe.HasResult(ItemID.LifeCrystal) && recipe.HasIngredient(ModContent.ItemType<Heartstone>())); //Disbaled by recipe not recipe group since this removes ALL group conditions for the recipe
		});
		c.EmitBrtrue(IL_0000);
		c.EmitLdsfld(typeof(Main).GetField("_requiredObjecsForCraftingText", BindingFlags.NonPublic | BindingFlags.Static));
	}

	private void RemoveRescipeVisually(ILContext il)
	{
		ILCursor c = new(il);
		ILLabel IL_00f3 = null;
		int recipe_numVar = -1;

		c.GotoNext(MoveType.After, i => i.MatchLdsfld<Main>(nameof(Main.recipe)), i => i.MatchLdloc(out _), i => i.MatchLdelemRef(), i => i.MatchStloc(out recipe_numVar));
		c.GotoNext(MoveType.After, i => i.MatchLdloc(out _), i => i.MatchLdfld<Item>("type"), i => i.MatchBrfalse(out IL_00f3));
		c.EmitLdloc(recipe_numVar);
		c.EmitDelegate((Recipe recipe) =>
		{
			return (recipe.HasCondition(Heartstone.RetroWorld) && !AvalonWorld.retroWorld) || (recipe.HasCondition(Heartstone.NotRetroWorld) && AvalonWorld.retroWorld);
		});
		c.EmitBrtrue(IL_00f3);
	}
}
