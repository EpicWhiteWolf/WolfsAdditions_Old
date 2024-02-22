using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip]
public class FuriaCoat : ModItem
{
	public override void SetStaticDefaults()
	{
		//ItemID.Sets.HidesHands[Item.bodySlot] = false;
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 5, 0, 0);
		Item.rare = 8;
		Item.defense = 25;
	}

	public override void UpdateEquip(Player player)
	{
		StatModifier damage = player.GetDamage(DamageClass.Magic);
		damage *= 1.15f;
		player.GetCritChance(DamageClass.Magic) += 15f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3458, 20);
		obj.AddIngredient(1225, 16);
		obj.AddTile(412);
		obj.Register();
	}
}
