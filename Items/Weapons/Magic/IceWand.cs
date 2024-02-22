using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Magic;

internal class IceWand : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 7;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 6;
		Item.width = 20;
		Item.height = 20;
		Item.useTime = 35;
		Item.useAnimation = 35;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 2f;
		Item.value = Item.sellPrice(0, 0, 5, 0);
		Item.rare = 1;
		Item.UseSound = SoundID.Item8;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("IceProjectile").Type;
		Item.shootSpeed = 12f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(75, 1);
		obj.AddRecipeGroup("Wood", 1);
		obj.AddIngredient(664, 1);
		obj.AddTile(18);
		obj.Register();
	}
}
