using Godot;
using System;

public partial class Enemigo : Area2D
{
	private Sprite2D _sprite;
	
	private CollisionShape2D cuerpo;
	[Export]
	private float timer;
	// Called when the node enters the scene tree for the first time.
	//toma de contacto
	public override void _Ready()
	{	
		cuerpo = GetNode<CollisionShape2D>("cuerpo");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnBodyEnteredBullet(Bullet body){
		this.QueueFree();
	}
	public void OnBodyEntered(Player body){
		body.Position = new Vector2(body.animation.FlipH ? 12 : -12, body.Position.Y);
	}
}
