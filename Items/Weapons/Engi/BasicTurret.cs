using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WolfsAdditions.Projectiles.Turret;

namespace WolfsAdditions.Items.Weapons.Engi;

public class BasicTurret : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 7;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 22;
		Item.height = 22;
		Item.useTime = 40;
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.noMelee = true;
		Item.knockBack = 2f;
		Item.value = Item.sellPrice(0, 0, 5, 0);
		Item.rare = 7;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;
		Item.shoot = ModContent.ProjectileType<BasicTurretProjectile>();
		Item.shootSpeed = 10f;
		Item.crit = 0;
		Item.noUseGraphic = true;
		Item.consumable = true;
		Item.maxStack = 9999;
		Item.useTurn = false;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.GetModPlayer<WolfsPlayer>().turretActive)
		{
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddRecipeGroup("IronBar", 1);
		obj.AddIngredient(97, 40);
		obj.AddTile(16);
		obj.Register();
	}
}
