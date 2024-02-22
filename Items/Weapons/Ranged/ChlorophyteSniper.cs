using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Ranged;

public class ChlorophyteSniper : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 70;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 74;
		Item.height = 20;
		Item.useTime = 60;
		Item.useAnimation = 60;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 7f;
		Item.value = Item.sellPrice(0, 5, 52, 0);
		Item.rare = 7;
		Item.UseSound = SoundID.Item11;
		Item.autoReuse = false;
		Item.shoot = 10;
		Item.shootSpeed = 16f;
		Item.useAmmo = AmmoID.Bullet;
		Item.crit = 16;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(1006, 12);
		obj.AddTile(134);
		obj.Register();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10f, -4f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int i = 0; i < 3; i++)
		{
			Vector2 projSpread = Utils.RotatedBy(new Vector2(velocity.X, velocity.Y), (double)MathHelper.ToRadians((float)(i - 1)), default(Vector2));
			Projectile.NewProjectile((IEntitySource)(object)source, position.X, position.Y - 4f, projSpread.X, projSpread.Y, type, damage, knockback, player.whoAmI, 0f, 0f, 0f);
		}
		return false;
	}
}
