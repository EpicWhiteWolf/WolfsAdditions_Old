using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class WolfsHelmet : ModItem
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
		Item.defense = 24;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 20;
		player.aggro += 300;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("WolfsChestplate").Type)
		{
			return legs.type == Mod.Find<ModItem>("WolfsLeggings").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "Converts mana into a damage shield\nReduces mana star pickups,\nmana potions and magic cuffs!";
		player.buffImmune[176] = true;
		player.buffImmune[177] = true;
		player.buffImmune[178] = true;
		player.buffImmune[6] = true;
		player.GetModPlayer<WolfsPlayer>().wolfsArmorSet = true;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient((Mod)null, "BalancedFragment", 10);
		obj.AddIngredient(1225, 8);
		obj.AddTile(412);
		obj.Register();
	}
}
