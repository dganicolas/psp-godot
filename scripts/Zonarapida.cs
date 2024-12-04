using Godot;
using System;

public partial class Zonarapida : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnBodyEntered(Player body){
		body.Speed += 600;
	}
	
	public void OnBodyExit(Player body){
		body.Speed -= 600;
	}
}
