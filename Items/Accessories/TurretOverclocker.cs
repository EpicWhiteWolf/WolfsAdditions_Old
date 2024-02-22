using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Accessories;

internal class TurretOverclocker : ModItem
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
		obj.AddIngredient(324, 1);
		obj.AddRecipeGroup(WolfsRecipes.GoldWatches, 1);
		obj.AddTile(114);
		obj.Register();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<WolfsPlayer>().overClock = true;
	}
}
