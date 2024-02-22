using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WolfsAdditions;

internal class WolfsRecipes : ModSystem
{
	public static RecipeGroup GoldWatches;

	public override void Unload()
	{
		GoldWatches = null;
	}

	public override void AddRecipeGroups()
	{
		GoldWatches = new RecipeGroup((Func<string>)(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(17)), new int[2] { 17, 709 });
		RecipeGroup.RegisterGroup("GoldWatch", GoldWatches);
	}

	public override void AddRecipes()
	{
		Recipe obj = Recipe.Create(857, 1);
		obj.AddIngredient(53, 1);
		obj.AddIngredient(Mod, "AncientArtifact", 1);
		obj.AddTile(114);
		obj.Register();
		Recipe obj2 = Recipe.Create(3368, 1);
		obj2.AddIngredient(989, 1);
		obj2.AddIngredient(211, 1);
		obj2.AddTile(16);
		obj2.Register();
		Recipe obj3 = Recipe.Create(410, 1);
		obj3.AddIngredient(225, 15);
		obj3.AddRecipeGroup("IronBar", 1);
		obj3.AddTile(86);
		obj3.Register();
		Recipe obj4 = Recipe.Create(411, 1);
		obj4.AddIngredient(225, 12);
		obj4.AddRecipeGroup("IronBar", 1);
		obj4.AddTile(86);
		obj4.Register();
		Recipe obj5 = Recipe.Create(934, 1);
		obj5.AddIngredient(225, 20);
		obj5.AddIngredient(19, 2);
		obj5.AddTile(114);
		obj5.Register();
		Recipe obj6 = Recipe.Create(1253, 1);
		obj6.AddIngredient(1328, 1);
		obj6.AddIngredient(2161, 1);
		obj6.AddTile(114);
		obj6.Register();
		Recipe obj7 = Recipe.Create(1309, 1);
		obj7.AddIngredient(762, 1);
		obj7.AddRecipeGroup("Wood", 4);
		obj7.AddTile(18);
		obj7.Register();
	}
}
