using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AmasLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//ItemID.Sets.HidesBottomSkin[Item.legSlot] = true;
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.rare = 1;
		Item.vanity = true;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(362, 1);
		obj.AddIngredient(1115, 1);
		obj.AddRecipeGroup("IronBar", 1);
		obj.AddTile(16);
		obj.Register();
	}
}
