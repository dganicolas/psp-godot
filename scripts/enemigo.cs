using Godot;
using System;

public partial class Enemigo : Area2D
{
	private Sprite2D _sprite;
	// Called when the node enters the scene tree for the first time.
	//toma de contacto
	public override void _Ready()
	{	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnBodyEnteredBullet(Area2D body){
		GetParent().QueueFree();
		body.QueueFree();
	}
}
