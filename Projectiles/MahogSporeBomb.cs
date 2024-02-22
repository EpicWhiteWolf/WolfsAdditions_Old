using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class MahogSporeBomb : ModProjectile
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
		Projectile.timeLeft = 60;
		Projectile.friendly = false;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = false;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 0;
	}

	public override void AI()
	{
		Projectile.rotation = 1f;
		((Entity)Projectile).velocity.Y += 0.15f;
		Vector2 trueCenter = default(Vector2);
		trueCenter = new Vector2(-4f, -4f);
		Vector2 randVect = default(Vector2);
		randVect = new Vector2((float)Main.rand.Next(-7, 7), (float)Main.rand.Next(-7, 7));
		int dustnumber = Dust.NewDust(((Entity)Projectile).Center + trueCenter + randVect, 0, 0, 53, 0f, 10f, 0, default(Color), 1f);
		Main.dust[dustnumber].noGravity = true;
		Main.dust[dustnumber].velocity = ((Entity)Projectile).velocity * 0.25f;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
		for (int i = 0; i < 8; i++)
		{
			int a = Projectile.NewProjectile(Projectile.InheritSource(Projectile), ((Entity)Projectile).Center, new Vector2((float)Main.rand.Next(-4, 4), (float)Main.rand.Next(-4, 4)), Mod.Find<ModProjectile>("MahogSpore").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f, 0f);
			Projectile obj = Main.projectile[a];
			obj.timeLeft += Main.rand.Next(240);
		}
	}
}
