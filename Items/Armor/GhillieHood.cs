using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class GhillieHood : ModItem
{
	public override void SetStaticDefaults()
	{
		SetDefaults();
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 0, 14, 0);
		Item.rare = 1;
		Item.defense = 1;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("GhillieJacket").Type)
		{
			return legs.type == Mod.Find<ModItem>("GhilliePants").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "2% increased crit chance";
		player.GetCritChance(DamageClass.Ranged) += 2f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(225, 7);
		obj.AddIngredient(3584, 8);
		obj.AddTile(304);
		obj.Register();
	}
}
