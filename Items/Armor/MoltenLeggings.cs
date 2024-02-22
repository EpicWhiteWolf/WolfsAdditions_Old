using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class MoltenLeggings : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 5, 0, 0);
		Item.rare = 3;
		Item.defense = 6;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed *= 1.1f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(175, 15);
		obj.AddTile(16);
		obj.Register();
	}
}
