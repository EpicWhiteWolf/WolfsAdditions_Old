using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Ranged;

internal class FrostSniper : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 150;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 78;
		Item.height = 26;
		Item.useTime = 60;
		Item.useAnimation = 60;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 7f;
		Item.value = Item.sellPrice(0, 0, 1, 0);
		Item.rare = 1;
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
		obj.AddIngredient(1198, 12);
		obj.AddIngredient(2161, 1);
		obj.AddTile(134);
		obj.Register();
		Recipe obj2 = CreateRecipe(1);
		obj2.AddIngredient(391, 12);
		obj2.AddIngredient(2161, 1);
		obj2.AddTile(134);
		obj2.Register();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
		muzzleOffset.Y += -6f;
		if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
		{
			position += muzzleOffset;
		}
		if (type == 14)
		{
			Projectile.NewProjectile((IEntitySource)(object)source, position, velocity, Mod.Find<ModProjectile>("DeepChillProjectile").Type, damage, knockback, player.whoAmI, 0f, 0f, 0f);
			return false;
		}
		return true;
	}
}
