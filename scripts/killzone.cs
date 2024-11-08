using Godot;
using System;

public partial class Area2d2 : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnBodyEnteredKillzone(Node2D body){
		GD.Print(body.Name);
		GetTree().ReloadCurrentScene();
	}
}
