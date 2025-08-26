using ModLiquidLib.ModLoader;
using Terraria;
using Terraria.Localization;

namespace Avalon.ModSupport.MLL;
public static class MLLSystems
{
	public static Recipe AddLiquid(this Recipe recipe, int liquidID)
	{
		recipe.Conditions.Add(new Condition(Language.GetOrRegister($"Mods.Avalon.Liquids.{LiquidLoader.GetLiquid(liquidID).Name}.RecipeEntry"), LiquidLoader.NearLiquid(liquidID).Predicate));

		return recipe;
	}
	public static Recipe AddLiquid<T>(this Recipe recipe) where T : ModLiquid
	{
		recipe.Conditions.Add(new Condition(Language.GetOrRegister($"Mods.Avalon.Liquids.{typeof(T).Name}.RecipeEntry"), LiquidLoader.NearLiquid(LiquidLoader.LiquidType<T>()).Predicate));

		return recipe;
	}
}