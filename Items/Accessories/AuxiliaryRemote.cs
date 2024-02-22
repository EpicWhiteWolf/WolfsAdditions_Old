using Terraria;
using Terraria.ModLoader;
using WolfsAdditions.Items.Crafting;

namespace WolfsAdditions.Items.Accessories;

internal class AuxiliaryRemote : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 30;
		Item.value = Item.sellPrice(0, 0, 5, 0);
		Item.rare = 4;
		Item.accessory = true;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(19, 1);
		obj.AddIngredient(530, 20);
		obj.AddIngredient<BlankCircuits>(1);
		obj.AddTile(114);
		obj.Register();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<WolfsPlayer>().auxRemote = true;
	}
}
