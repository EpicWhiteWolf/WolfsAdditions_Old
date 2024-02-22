using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Crafting;

internal class BlankCircuits : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.width = 40;
		Item.height = 40;
		Item.rare = 1;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(762, 1);
		obj.AddTile(17);
		obj.Register();
	}
}
