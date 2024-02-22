using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class FossilMask : ModItem
{
	public override void SetStaticDefaults()
	{
		SetDefaults();
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 0, 30, 0);
		Item.rare = 1;
		Item.defense = 3;
	}

	public override void UpdateEquip(Player player)
	{
		StatModifier damage = player.GetDamage(DamageClass.Ranged);
		damage *= 1.05f;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("FossilPlatemail").Type)
		{
			return legs.type == Mod.Find<ModItem>("FossilLeggings").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "5% increased ranged crit chance";
		player.GetCritChance(DamageClass.Ranged) += 5f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3380, 12);
		obj.AddIngredient(85, 8);
		obj.AddTile(16);
		obj.Register();
	}
}
