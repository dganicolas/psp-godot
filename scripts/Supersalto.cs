using Godot;
using System;

public partial class Supersalto : Area2D
{
	private Timer timer;
	private float salto;
	[Export]
	private float saltoPotenciado  { get; set; }
	private Player player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = new Timer();
		timer.WaitTime = 3.0f;
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
		salto = body.JumpVelocity;
		body.powerUps.Play("supersalto");
		body.JumpVelocity = saltoPotenciado;
		this.Visible=false;
		timer.Start();
	}
	private void OnTimerTimeout()
	{
		player.powerUps.Play("defecto");
		player.JumpVelocity = salto;
		this.QueueFree();
	}
}
