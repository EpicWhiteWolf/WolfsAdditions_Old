using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Ranged;

internal class Minigun : ModItem
{
	public int test;

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 50;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 74;
		Item.height = 20;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 1f;
		Item.value = Item.sellPrice(0, 0, 54, 0);
		Item.rare = 3;
		Item.UseSound = SoundID.Item11;
		Item.autoReuse = true;
		Item.shoot = 10;
		Item.shootSpeed = 10f;
		Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-16f, 10f);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Utils.NextFloat(Main.rand) >= 0.66f;
	}

	public override void HoldItem(Player player)
	{
		WolfsPlayer p = player.GetModPlayer<WolfsPlayer>();
		Item.useTime = (int)p.minigun;
		Item.useAnimation = (int)p.minigun;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (!PlayerInput.Triggers.Current.MouseLeft)
		{
			return false;
		}
		Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
		muzzleOffset.Y += 8f;
		Vector2 spread = Utils.RotatedByRandom(new Vector2(velocity.X, velocity.Y), (double)MathHelper.ToRadians(1f));
		velocity.X = spread.X;
		velocity.Y = spread.Y;
		position += muzzleOffset;
		Projectile.NewProjectile((IEntitySource)(object)source, position, spread, type, damage, knockback, player.whoAmI, 0f, 0f, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(1225, 12);
		obj.AddIngredient(324, 1);
		obj.AddIngredient(548, 10);
		obj.AddIngredient(547, 10);
		obj.AddTile(134);
		obj.Register();
	}
}
