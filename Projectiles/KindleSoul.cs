using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class KindleSoul : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		Main.projFrames[Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 60;
		((Entity)Projectile).height = 60;
		Projectile.timeLeft = 240;
		Projectile.friendly = false;
		Projectile.hostile = false;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 0;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 1f);
	}

	public override void AI()
	{
		Lighting.AddLight(((Entity)Projectile).Center, 0f, 1f, 0f);
		Player nearestPlayer = (from p in Main.player
			where ((Entity)p).active && !p.dead
			orderby ((Entity)Projectile).Distance(((Entity)p).Center)
			select p).FirstOrDefault();
		Player owner = Main.player[Projectile.owner];
		Vector2 direction = ((Entity)nearestPlayer).Center - ((Entity)Projectile).Center;
		((Vector2)(direction)).Length();
		Projectile projectile = Projectile;
		if (++projectile.frameCounter >= 10)
		{
			Projectile.frameCounter = 0;
			Projectile projectile2 = Projectile;
			if (++projectile2.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
		if (owner.team == nearestPlayer.team && nearestPlayer != owner)
		{
			if (Projectile.timeLeft <= 200)
			{
				Projectile.Kill();
			}
			int num = (int)(direction.X * 1000f);
			int oldVelocityXBy1000 = (int)(((Entity)Projectile).velocity.X * 1000f);
			int velocityYBy1000 = (int)(direction.Y * 1000f);
			int oldVelocityYBy1000 = (int)(((Entity)Projectile).velocity.Y * 1000f);
			if (num != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
			{
				Projectile.netUpdate = true;
			}
			((Entity)Projectile).velocity = ((Entity)nearestPlayer).velocity;
			((Entity)Projectile).position = nearestPlayer.MountedCenter;
		}
	}

	public override void OnKill(int timeLeft)
	{
		Player nearestPlayer = (from p in Main.player
			where ((Entity)p).active && !p.dead
			orderby ((Entity)Projectile).Distance(((Entity)p).Center)
			select p).FirstOrDefault();
		Player owner = Main.player[Projectile.owner];
		Vector2 direction = ((Entity)nearestPlayer).Center - ((Entity)Projectile).Center;
		float distanceToPlayer = ((Vector2)(direction)).Length();
		if (nearestPlayer != owner && distanceToPlayer <= 10f)
		{
			nearestPlayer.HealEffect(Projectile.damage * 3, false);
			nearestPlayer.statLife += Projectile.damage * 3;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Collision.HitTiles(((Entity)Projectile).position, ((Entity)Projectile).velocity, ((Entity)Projectile).width, ((Entity)Projectile).height);
		SoundEngine.PlaySound(SoundID.Item10, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
		return true;
	}
}
