using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WolfsAdditions.Projectiles;

namespace WolfsAdditions.Items.Weapons.Melee;

public class BladeOfCthulhu : ModItem
{
	private const string CthuluBladePath = "WolfsAdditions/Items/Weapons/Melee/BladeOfCthulhuItem";

	private Asset<Texture2D> CthuluBlade;

	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.useStyle = 5;
		Item.width = 24;
		Item.height = 24;
		Item.useAnimation = 10;
		Item.useTime = 10;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
		Item.damage = 9;
		Item.rare = 0;
		Item.DamageType = DamageClass.MeleeNoSpeed;
		Item.channel = true;
		Item.noMelee = true;
		Item.autoReuse = true;
		Item.shoot = ModContent.ProjectileType<EyeBlade>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		CthuluBlade = ModContent.Request<Texture2D>("WolfsAdditions/Items/Weapons/Melee/BladeOfCthulhuItem", (AssetRequestMode)2);
		Texture2D texture = CthuluBlade.Value;
		spriteBatch.Draw(texture, position, (Rectangle?)null, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		Vector2 offsetPositon = default(Vector2);
		offsetPositon = new Vector2(16f, 16f);
		CthuluBlade = ModContent.Request<Texture2D>("WolfsAdditions/Items/Weapons/Melee/BladeOfCthulhuItem", (AssetRequestMode)2);
		Texture2D texture = CthuluBlade.Value;
		Vector2 position = Item.position - Main.screenPosition + new Vector2((float)(Item.width / 2), (float)Item.height - (float)texture.Height * 0.5f + 2f);
		spriteBatch.Draw(texture, position + offsetPositon, (Rectangle?)null, alphaColor, rotation, Utils.Size(texture), scale, (SpriteEffects)0, 0f);
		return false;
	}
}
