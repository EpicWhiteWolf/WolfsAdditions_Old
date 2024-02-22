using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class MoltenHelm : ModItem
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

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("MoltenJacket").Type)
		{
			return legs.type == Mod.Find<ModItem>("MoltenLeggings").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "10% increased crit chance\nImmunity to fire";
		player.GetCritChance(DamageClass.Ranged) += 10f;
		player.buffImmune[24] = true;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(175, 10);
		obj.AddTile(16);
		obj.Register();
	}
}
