using Godot;
using System;

public partial class Enemigo : Area2D
{
	private Sprite2D _sprite;
	private Timer _timer;
	private CollisionShape2D cuerpo;
	[Export]
	private float timer;
	// Called when the node enters the scene tree for the first time.
	//toma de contacto
	public override void _Ready()
	{	
		_timer = GetNode<Timer>("Timer");
		cuerpo = GetNode<CollisionShape2D>("cuerpo");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void Death(){
		_timer.Start(timer);
		this.QueueFree();
		
	}
		public void OnBodyEnteredBullet(Bullet body){
		Death();
	}
	public void OnBodyEntered(Player body){
		
		int mover = body.animation.FlipH ? 55 : -55;
		body.Position = new Vector2(mover, body.Position.Y);
		body.recibirDano(1);
	}
}
