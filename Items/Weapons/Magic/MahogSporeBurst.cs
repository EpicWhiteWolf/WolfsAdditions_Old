using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Magic;

internal class MahogSporeBurst : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 5;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 60;
		Item.width = 20;
		Item.height = 20;
		Item.useAnimation = 60;
		Item.useTime = 60;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.sellPrice(0, 0, 5, 0);
		Item.rare = 1;
		Item.UseSound = SoundID.Item61;
		Item.autoReuse = false;
		Item.shoot = Mod.Find<ModProjectile>("MahogSporeBomb").Type;
		Item.shootSpeed = 12f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10f, 0f);
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(331, 12);
		obj.AddIngredient(620, 12);
		obj.AddIngredient(210, 3);
		obj.AddTile(18);
		obj.Register();
	}
}
