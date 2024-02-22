using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Ranged;

internal class Dawnbreaker : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 126;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 92;
		Item.height = 26;
		Item.useTime = 60;
		Item.useAnimation = 60;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 7f;
		Item.value = Item.sellPrice(0, 6, 0, 0);
		Item.rare = 4;
		Item.UseSound = SoundID.Item12;
		Item.autoReuse = false;
		Item.shoot = 10;
		Item.shootSpeed = 16f;
		Item.useAmmo = Mod.Find<ModItem>("Rebar").Type;
		Item.crit = 16;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(520, 16);
		obj.AddIngredient(528, 2);
		obj.AddIngredient(Mod, "ScopedRifle", 1);
		obj.AddTile(134);
		obj.Register();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0f, 3f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
		muzzleOffset.Y += 0f;
		if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
		{
			position += muzzleOffset;
		}
		return true;
	}
}
