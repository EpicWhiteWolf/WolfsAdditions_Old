using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Magic;

internal class FireWand : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 3;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 6;
		Item.width = 20;
		Item.height = 20;
		Item.useAnimation = 29;
		Item.useTime = 7;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.sellPrice(0, 0, 5, 0);
		Item.rare = 1;
		Item.UseSound = SoundID.Item34;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("FireProjectile").Type;
		Item.shootSpeed = 6f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(75, 1);
		obj.AddRecipeGroup("Wood", 1);
		obj.AddIngredient(8, 1);
		obj.AddTile(18);
		obj.Register();
	}
}
