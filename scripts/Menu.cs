using Godot;
using System;

public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void onPressedJugar(){
		var nextScene = ResourceLoader.Load<PackedScene>("res://scenes/principal.tscn");
		GetTree().ChangeSceneToPacked(nextScene);
	}
	public void OnPressedSalir(){
		GetTree().Quit();
	}
}
