using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WolfsAdditions.Projectiles;

namespace WolfsAdditions.Items.Weapons;

public class WallCharge : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.useStyle = 1;
		Item.shootSpeed = 12f;
		Item.shoot = ModContent.ProjectileType<Projectiles.WallCharge>();
		Item.width = 18;
		Item.height = 18;
		Item.maxStack = 99;
		Item.consumable = true;
		Item.UseSound = SoundID.Item1;
		Item.useAnimation = 40;
		Item.useTime = 40;
		Item.noUseGraphic = true;
		Item.noMelee = true;
		Item.value = Item.buyPrice(0, 0, 20, 0);
		Item.rare = 1;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(167, 1);
		obj.AddIngredient(762, 1);
		obj.AddTile(18);
		obj.Register();
	}
}
