using Godot;
using System;

public partial class Supersalto : Area2D
{
	private Timer timer;
	private float salto;
	[Export]
	private float saltoPotenciado = 250;
	private Player player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = new Timer();
		timer.WaitTime = 1.5f;
		timer.OneShot = true;
		timer.Timeout += OnTimerTimeout;
		AddChild(timer);
		player = GetTree().Root.GetNode<Player>("/root/Juego/CharacterBody2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnBodyEntered(Player body){
		salto = body.Speed;
		body.powerUps.Play("supersalto");
		body.Speed = saltoPotenciado;
		this.Visible=false;
		timer.Start();
	}
	private void OnTimerTimeout()
	{
		player.powerUps.Play("defecto");
		player.Speed = salto;
		this.QueueFree();
	}
}
