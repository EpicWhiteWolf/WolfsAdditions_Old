using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class AirProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 14;
		Projectile.height = 14;
		Projectile.timeLeft = 60;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = false;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 0;
	}

	public override void AI()
	{
		Projectile.rotation = Utils.ToRotation(((Entity)Projectile).velocity) + MathHelper.ToRadians(90f);
		Vector2 randVect = default(Vector2);
		randVect = new Vector2((float)Main.rand.Next(-15, 15), (float)Main.rand.Next(-15, 15));
		int dustnumber = Dust.NewDust(((Entity)Projectile).position + randVect, ((Entity)Projectile).width, ((Entity)Projectile).height, Mod.Find<ModDust>("AirDust").Type, 0f, 0f, 100, default(Color), 1.2f);
		Main.dust[dustnumber].velocity = ((Entity)Projectile).velocity * 0.5f;
	}

	public override void OnKill(int timeLeft)
	{
		Collision.HitTiles(((Entity)Projectile).position, ((Entity)Projectile).velocity, ((Entity)Projectile).width, ((Entity)Projectile).height);
	}
}
