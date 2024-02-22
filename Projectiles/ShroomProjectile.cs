using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class ShroomProjectile : ModProjectile
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
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.aiStyle = 0;
		Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		Projectile.rotation = Utils.ToRotation(((Entity)Projectile).velocity) + MathHelper.ToRadians(90f);
		Lighting.AddLight(((Entity)Projectile).Center, 0f, 0f, 1f);
		Color glow = default(Color);
		glow = new Color(1f, 1f, 1f, 1f);
		GetAlpha(glow);
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 8; i++)
		{
			int a = Projectile.NewProjectile(Projectile.InheritSource(Projectile), ((Entity)Projectile).Center.X, ((Entity)Projectile).Center.Y, (float)Main.rand.Next(-10, 10) * 0.25f, (float)Main.rand.Next(-10, 10) * 0.25f, Mod.Find<ModProjectile>("ShroomSporeProj").Type, (int)((double)Projectile.damage * 0.1), 1f, Projectile.owner, 0f, 0f, 0f);
			Projectile obj = Main.projectile[a];
			obj.timeLeft += Main.rand.Next(240);
		}
		Collision.HitTiles(((Entity)Projectile).position, ((Entity)Projectile).velocity, ((Entity)Projectile).width, ((Entity)Projectile).height);
		SoundEngine.PlaySound(SoundID.Item10, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
	}

	public override bool PreKill(int timeLeft)
	{
		Projectile.type = Mod.Find<ModProjectile>("ShroomProjectile").Type;
		return true;
	}
}
