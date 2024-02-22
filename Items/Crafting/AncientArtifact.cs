using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Crafting;

internal class AncientArtifact : ModItem
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
		obj.AddIngredient(3271, 10);
		obj.AddIngredient(3380, 15);
		obj.AddTile(18);
		obj.Register();
	}
}
