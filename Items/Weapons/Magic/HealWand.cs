using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Magic;

internal class HealWand : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 1;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 2;
		Item.width = 20;
		Item.height = 20;
		Item.useTime = 10;
		Item.useAnimation = 10;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 3f;
		Item.value = Item.sellPrice(0, 0, 5, 0);
		Item.rare = 1;
		Item.UseSound = SoundID.Item8;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("HealProjectile").Type;
		Item.shootSpeed = 6f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(75, 1);
		obj.AddIngredient(3069, 1);
		obj.AddIngredient(188, 1);
		obj.AddTile(18);
		obj.Register();
	}
}
