using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Ranged;

internal class DaedelusGunbow : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(3029);
		Item.useAmmo = AmmoID.Bullet;
		Item.UseSound = SoundID.Item11;
		Item.useAnimation = 10;
		Item.useTime = 10;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(324, 1);
		obj.AddIngredient(3029, 1);
		obj.AddTile(16);
		obj.Register();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX + (float)Main.rand.Next(-100, 100), (float)Main.mouseY);
		float ceilingLimit = target.Y;
		if (ceilingLimit > player.Center.Y - 200f)
		{
			ceilingLimit = player.Center.Y - 200f;
		}
		for (int i = 0; i < 3; i++)
		{
			position = Main.screenPosition + new Vector2((float)Main.rand.Next(-200, 200) + (float)Main.mouseX, -600f - (float)(i * 100));
			position.Y -= 100 * i;
			Vector2 heading = target - position;
			heading.Normalize();
			Vector2 val = heading;
			Vector2 val2 = new Vector2(velocity.X, velocity.Y);
			heading = val * val2.Length();
			velocity.X = heading.X;
			velocity.Y = heading.Y + (float)Main.rand.Next(-40, 41) * 0.02f;
			Projectile.NewProjectile((IEntitySource)(object)source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, ceilingLimit, 0f, 0f);
		}
		return false;
	}
}
