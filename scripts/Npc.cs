using Godot;
using System;

public partial class Npc : Area2D
{
	[Export]
	string textoMensaje;
	private Label texto;
	private Timer timer;
	private Sprite2D mensaje;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = new Timer();
		timer.WaitTime = 2.0f;
		timer.OneShot = true;
		AddChild(timer);
		mensaje = GetNode<Sprite2D>("Sprite2D");
		texto = GetNode<Label>("Sprite2D/Label");
		timer.Timeout += OnTimerTimeout;
		texto.Text= textoMensaje;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	
	public void OnBodyEnteredBullet(Bullet body){
		texto.Text= "no me ataques, no te he hecho nada, solo ve por el oro y vete";
		mensaje.Visible = true;
		body.QueueFree();
		timer.Start();
	}
	private void OnTimerTimeout()
	{
		mensaje.Visible = false;
		texto.Text= textoMensaje;
	}
	
	public void OnBodyEntered(Player body){
		mensaje.Visible = true;
	}
	
	public void OnBodyExit(Player body){
		mensaje.Visible = false;
	}
}
