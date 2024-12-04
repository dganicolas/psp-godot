using Godot;
using System;

public partial class NpcAgresivo : Area2D
{
	[Export]
	public float speedBullet = 100f; // Velocidad del proyectil
	[Export]
	public float shootCooldown = 1f; // Tiempo entre disparos
	[Export]
	public int vida = 10;

	private RayCast2D raycast;
	private PackedScene bullet;
	private Timer shootTimer;

	public override void _Ready()
	{
		// Configura el RayCast2D y carga la escena del proyectil
		raycast = GetNode<RayCast2D>("RayCast2D");
		bullet = GD.Load<PackedScene>("res://scenes/entity/bullet.tscn");

		// Crea y configura el Timer dinámicamente
		shootTimer = new Timer();
		shootTimer.WaitTime = shootCooldown;
		shootTimer.OneShot = true;
		AddChild(shootTimer);

		// Conecta la señal Timeout al método OnShootTimerTimeout
		shootTimer.Timeout += OnShootTimerTimeout;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (raycast.IsColliding() && shootTimer.IsStopped())
		{
			shootTimer.Start(); // Inicia el Timer
		}
	}
	private void OnShootTimerTimeout()
	{
		// Instancia el proyectil
		Bullet instBullet = (Bullet)bullet.Instantiate();
		instBullet.Position = this.GlobalPosition + new Vector2(0, -5);
		instBullet.speedBullet = -speedBullet;
		GetParent().AddChild(instBullet);
	}
	public void OnBodyEnteredBullet(Bullet body){
		vida -= 1;
		if(vida == 0){
			this.QueueFree();
		}
	}
}
