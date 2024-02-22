using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip]
public class FuriaLeggings : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 5, 0, 0);
		Item.rare = 8;
		Item.defense = 21;
	}

	public override void UpdateEquip(Player player)
	{
		StatModifier damage = player.GetDamage(DamageClass.Magic);
		damage *= 1.15f;
		player.moveSpeed *= 1.15f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3458, 15);
		obj.AddIngredient(1225, 12);
		obj.AddTile(412);
		obj.Register();
	}
}
