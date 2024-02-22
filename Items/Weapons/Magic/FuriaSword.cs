using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Weapons.Magic;

internal class FuriaSword : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.damage = 20;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 20;
		Item.width = 88;
		Item.height = 18;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 7f;
		Item.value = Item.sellPrice(0, 0, 1, 0);
		Item.rare = 1;
		Item.UseSound = SoundID.Item36;
		Item.autoReuse = true;
		Item.shoot = 14;
		Item.shootSpeed = 16f;
		Item.crit = 4;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Player playerNearCursor = (from i in Main.player
			where ((Entity)i).active && !i.dead
			orderby Vector2.Distance(Main.MouseWorld, ((Entity)i).Center)
			select i).FirstOrDefault();
		int owner = Main.myPlayer;
		if (player.altFunctionUse == 2)
		{
			if (!(Vector2.Distance(Main.MouseWorld, ((Entity)playerNearCursor).Center) < 400f) || Main.player[owner].team != playerNearCursor.team || playerNearCursor == Main.player[owner])
			{
				player.AddBuff(Mod.Find<ModBuff>("KindleSoulCD").Type, 180, true, false);
				return false;
			}
			Projectile.NewProjectile((IEntitySource)(object)source, ((Entity)playerNearCursor).position, ((Entity)playerNearCursor).velocity, Mod.Find<ModProjectile>("KindleSoul").Type, damage, 0f, player.whoAmI, 0f, 0f, 0f);
			player.AddBuff(Mod.Find<ModBuff>("KindleSoulCD").Type, 180, true, false);
		}
		else
		{
			for (int j = 0; j < 6; j++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(velocity.X, velocity.Y), (double)MathHelper.ToRadians(3f));
				Projectile.NewProjectile((IEntitySource)(object)source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI, 0f, 0f, 0f);
			}
		}
		return false;
	}

	public override bool AltFunctionUse(Player player)
	{
		if (!player.HasBuff(Mod.Find<ModBuff>("KindleSoulCD").Type))
		{
			return true;
		}
		return false;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			Item.UseSound = SoundID.Item1;
		}
		else
		{
			Item.UseSound = SoundID.Item36;
		}
		return CanUseItem(player);
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3458, 18);
		obj.AddIngredient(1225, 16);
		obj.AddTile(412);
		obj.Register();
	}
}
