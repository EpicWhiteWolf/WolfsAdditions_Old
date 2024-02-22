using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class HealProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 12;
		((Entity)Projectile).height = 12;
		Projectile.timeLeft = 960;
		Projectile.friendly = false;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
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
		Vector2 dustSpread = default(Vector2);
		dustSpread = new Vector2((float)Main.rand.Next(-6, 6), (float)Main.rand.Next(-6, 6));
		if (Utils.NextBool(Main.rand, 10))
		{
			Dust.NewDust(((Entity)Projectile).Center + dustSpread, 0, 0, Mod.Find<ModDust>("HealDust").Type, 0f, 10f, 0, default(Color), 1f);
		}
		Player nearestPlayer = (from p in Main.player
			where ((Entity)p).active && !p.dead
			orderby ((Entity)Projectile).Distance(((Entity)p).Center)
			select p).FirstOrDefault();
		Player owner = Main.player[Projectile.owner];
		Vector2 direction = ((Entity)nearestPlayer).Center - ((Entity)Projectile).Center;
		float distanceToPlayer = ((Vector2)(direction)).Length();
		Projectile.rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.ToRadians(90f);
		if (owner.team == nearestPlayer.team && nearestPlayer != owner)
		{
			if (distanceToPlayer <= 20f && nearestPlayer != owner)
			{
				Projectile.Kill();
			}
			float speed = 14f;
			float inertia = 10f;
			((Vector2)(direction)).Normalize();
			direction *= speed;
			int num = (int)(direction.X * 1000f);
			int oldVelocityXBy1000 = (int)(((Entity)Projectile).velocity.X * 1000f);
			int velocityYBy1000 = (int)(direction.Y * 1000f);
			int oldVelocityYBy1000 = (int)(((Entity)Projectile).velocity.Y * 1000f);
			if (num != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
			{
				Projectile.netUpdate = true;
			}
			((Entity)Projectile).velocity = (((Entity)Projectile).velocity * (inertia - 1f) + direction) / inertia;
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
			nearestPlayer.HealEffect(Projectile.damage, false);
			nearestPlayer.statLife += Projectile.damage;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Collision.HitTiles(((Entity)Projectile).position, ((Entity)Projectile).velocity, ((Entity)Projectile).width, ((Entity)Projectile).height);
		SoundEngine.PlaySound(SoundID.Item10, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
		return true;
	}
}
