using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip]
public class MoltenJacket : ModItem
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
		Item.defense = 7;
	}

	public override void UpdateEquip(Player player)
	{
		StatModifier damage = player.GetDamage(DamageClass.Ranged);
		damage *= 1.15f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(175, 20);
		obj.AddTile(16);
		obj.Register();
	}
}
