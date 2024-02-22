using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Magic;

internal class StoneWand : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 5;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 3;
		Item.width = 20;
		Item.height = 20;
		Item.useTime = 7;
		Item.useAnimation = 7;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 4f;
		Item.value = Item.sellPrice(0, 0, 5, 0);
		Item.rare = 1;
		Item.UseSound = SoundID.Item32;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("StoneProjectile").Type;
		Item.shootSpeed = 12f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(75, 1);
		obj.AddRecipeGroup("Wood", 1);
		obj.AddIngredient(3, 1);
		obj.AddTile(18);
		obj.Register();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(velocity.X, velocity.Y), (double)MathHelper.ToRadians(3f));
		velocity.X = perturbedSpeed.X;
		velocity.Y = perturbedSpeed.Y;
		return true;
	}
}
