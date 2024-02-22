using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class WolfsLeggings : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 5, 0, 0);
		Item.rare = 8;
		Item.defense = 20;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 40;
		player.aggro += 300;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient((Mod)null, "BalancedFragment", 14);
		obj.AddIngredient(1225, 12);
		obj.AddTile(412);
		obj.Register();
	}
}
