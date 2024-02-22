using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class EmberProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 8;
		((Entity)Projectile).height = 8;
		Projectile.timeLeft = 240;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 0;
		Projectile.hide = true;
		Projectile.penetrate = -1;
	}

	public override void AI()
	{
		Projectile.rotation = Utils.ToRotation(((Entity)Projectile).velocity) + MathHelper.ToRadians(90f);
		Vector2 randPos = default(Vector2);
		randPos = new Vector2(Utils.NextFloat(Main.rand, -20f, 20f), Utils.NextFloat(Main.rand, -4f, 6f));
		int dustnumber = Dust.NewDust(((Entity)Projectile).position + randPos, ((Entity)Projectile).width, ((Entity)Projectile).height, 6, 0f, 0f, 0, default(Color), 2f);
		Main.dust[dustnumber].noGravity = true;
		Main.dust[dustnumber].velocity.Y = -1.5f;
		((Entity)Projectile).velocity.X *= 0.95f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 240, false);
	}

    public override bool OnTileCollide(Vector2 oldVelocity)
	{
		((Entity)Projectile).velocity.X = 0f;
		return false;
	}
}
