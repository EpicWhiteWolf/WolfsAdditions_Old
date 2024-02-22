using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class ShroomSporeProj : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		Main.projFrames[Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 14;
		((Entity)Projectile).height = 14;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 240;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.aiStyle = 0;
		DrawOriginOffsetY = -4;
	}

	public override void AI()
	{
		Projectile projectile = Projectile;
		((Entity)projectile).velocity = ((Entity)projectile).velocity * 0.95f;
		Lighting.AddLight(((Entity)Projectile).Center, 0f, 0f, 1f);
		Color glow = default(Color);
		glow = new Color(1f, 1f, 1f, 1f);
		GetAlpha(glow);
		Projectile projectile2 = Projectile;
		if (++projectile2.frameCounter >= 10)
		{
			Projectile.frameCounter = 0;
			Projectile projectile3 = Projectile;
			if (++projectile3.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (((Entity)Projectile).velocity.X != oldVelocity.X)
		{
			((Entity)Projectile).velocity.X = 0f - oldVelocity.X;
		}
		if (((Entity)Projectile).velocity.Y != oldVelocity.Y)
		{
			((Entity)Projectile).velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}
}
