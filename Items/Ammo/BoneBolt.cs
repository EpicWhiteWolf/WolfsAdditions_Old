using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Ammo;

public class BoneBolt : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 10;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 14;
		Item.height = 32;
		Item.maxStack = 999;
		Item.consumable = true;
		Item.knockBack = 5f;
		Item.value = Item.sellPrice(0, 0, 0, 10);
		Item.rare = 1;
		Item.shoot = Mod.Find<ModProjectile>("BoneBoltProjectile").Type;
		Item.shootSpeed = 7f;
		Item.ammo = Mod.Find<ModItem>("BoneBolt").Type;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(25);
		obj.AddIngredient(3380, 1);
		obj.Register();
	}
}
