using Godot;
using System;

public partial class Enemigo : Area2D
{
	private PackedScene bullet;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	bullet = GD.Load<PackedScene>("res://scenes/bullet.tscn");	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnBodyEntered(Node2D body){
		GD.Print("xd");
				
	}
	
	public void OnBodyEnteredBullet(Area2D body){
		GD.Print("auuu");
		Bullet instBullet = (Bullet) bullet.Instantiate();
				instBullet.Position = new Vector2(0,19);
				instBullet.speedBullet = 30;
				AddChild(instBullet);
	}
}
