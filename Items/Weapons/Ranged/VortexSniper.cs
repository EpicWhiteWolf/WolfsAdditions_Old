using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Ranged;

public class VortexSniper : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 250;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 100;
		Item.height = 24;
		Item.useTime = 60;
		Item.useAnimation = 60;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 7f;
		Item.value = Item.sellPrice(0, 10, 0, 0);
		Item.rare = 10;
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
		obj.AddIngredient(3456, 18);
		obj.AddTile(16);
		obj.Register();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-11f, -3f);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return Utils.NextFloat(Main.rand) >= 1f;
		}
		return Utils.NextFloat(Main.rand) >= 0f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.UseSound = SoundID.Item12;
		}
		else
		{
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.UseSound = SoundID.Item11;
		}
		return CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
		muzzleOffset.Y += -6f;
		if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
		{
			position += muzzleOffset;
		}
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile((IEntitySource)(object)source, position, velocity, Mod.Find<ModProjectile>("VortexMarkProj").Type, damage, knockback, player.whoAmI, 0f, 0f, 0f);
			return false;
		}
		if (type == 14)
		{
			Projectile.NewProjectile((IEntitySource)(object)source, position, velocity, Mod.Find<ModProjectile>("VortexSniperProj").Type, damage, knockback, player.whoAmI, 0f, 0f, 0f);
			return false;
		}
		return true;
	}
}
