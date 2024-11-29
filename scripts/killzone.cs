using Godot;
using System;

public partial class Killzone : Area2D
{
	private Juego juego;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		juego = GetTree().Root.GetNode<Juego>("Juego");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnBodyEnteredKillzone(CharacterBody2D body){
		juego.ReloadScene();
		
	}
}
