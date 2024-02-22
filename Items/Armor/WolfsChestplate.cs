using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip]
public class WolfsChestplate : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
		//ItemID.Sets.HidesHands[Item.bodySlot] = false;
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 5, 0, 0);
		Item.rare = 8;
		Item.defense = 34;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += 40;
		player.aggro += 300;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient((Mod)null, "BalancedFragment", 20);
		obj.AddIngredient(1225, 16);
		obj.AddTile(412);
		obj.Register();
	}
}
