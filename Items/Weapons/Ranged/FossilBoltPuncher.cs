using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Ranged;

public class FossilBoltPuncher : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 65;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 96;
		Item.height = 20;
		Item.useTime = 60;
		Item.useAnimation = 60;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 7f;
		Item.value = Item.sellPrice(0, 0, 50, 0);
		Item.rare = 1;
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = false;
		Item.shoot = 10;
		Item.shootSpeed = 8f;
		Item.useAmmo = Mod.Find<ModItem>("BoneBolt").Type;
		Item.crit = 16;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddRecipeGroup("IronBar", 5);
		obj.AddIngredient(3380, 8);
		obj.AddTile(16);
		obj.Register();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10f, -4f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
		muzzleOffset.Y += -5f;
		if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
		{
			position += muzzleOffset;
		}
		return true;
	}
}
