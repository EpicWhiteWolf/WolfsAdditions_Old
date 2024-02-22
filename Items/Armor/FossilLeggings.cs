using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class FossilLeggings : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 0, 40, 0);
		Item.rare = 1;
		Item.defense = 4;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed *= 1.05f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3380, 15);
		obj.AddIngredient(85, 10);
		obj.AddTile(16);
		obj.Register();
	}
}
