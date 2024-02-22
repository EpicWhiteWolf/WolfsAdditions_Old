using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip]
public class GhillieJacket : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 0, 18, 0);
		Item.rare = 1;
		Item.defense = 2;
	}

	public override void UpdateEquip(Player player)
	{
		StatModifier damage = player.GetDamage(DamageClass.Ranged);
		damage *= 1.05f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(225, 9);
		obj.AddIngredient(3584, 8);
		obj.AddTile(304);
		obj.Register();
	}
}
