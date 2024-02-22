using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class FossilPlatemail : ModItem
{
	public override void SetStaticDefaults()
	{
		SetDefaults();
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 0, 50, 0);
		Item.rare = 1;
		Item.defense = 6;
	}

	public override void UpdateEquip(Player player)
	{
		StatModifier damage = player.GetDamage(DamageClass.Ranged);
		damage *= 1.05f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3380, 18);
		obj.AddIngredient(85, 12);
		obj.AddTile(16);
		obj.Register();
	}
}
