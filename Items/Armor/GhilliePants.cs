using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class GhilliePants : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 0, 16, 0);
		Item.rare = 1;
		Item.defense = 1;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed *= 1.05f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(225, 8);
		obj.AddIngredient(3584, 8);
		obj.AddTile(304);
		obj.Register();
	}
}
