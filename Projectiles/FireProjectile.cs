using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class FireProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 14;
		((Entity)Projectile).height = 14;
		Projectile.timeLeft = 80;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = false;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 0;
		Projectile.hide = true;
		Projectile.penetrate = -1;
	}

	public override void AI()
	{
		Projectile.rotation = Utils.ToRotation(((Entity)Projectile).velocity) + MathHelper.ToRadians(90f);
		Vector2 randVect = default(Vector2);
		randVect = new Vector2((float)Main.rand.Next(-7, 7), (float)Main.rand.Next(-7, 7));
		int dustnumber = Dust.NewDust(((Entity)Projectile).position + randVect, ((Entity)Projectile).width, ((Entity)Projectile).height, 6, 0f, 0f, 0, default(Color), 2f);
		Main.dust[dustnumber].noGravity = true;
		Main.dust[dustnumber].velocity = ((Entity)Projectile).velocity * 0.25f;
		if (Utils.NextBool(Main.rand, 100))
		{
			Projectile.NewProjectile(Projectile.InheritSource(Projectile), ((Entity)Projectile).Center, new Vector2(((Entity)Projectile).velocity.X, 5f), Mod.Find<ModProjectile>("EmberProjectile").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f, 0f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 240, false);
	}
}
