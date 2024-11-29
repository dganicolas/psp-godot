using Godot;
using System;

public partial class Portal : Area2D
{
	
	private Juego juego;
	private bool tp = false;
	[Export]
	string textoMensaje;
	[Export]
	string instruccion;
	
	private Label texto;
	private Sprite2D mensaje;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mensaje = GetNode<Sprite2D>("Sprite2D");
		texto = GetNode<Label>("Sprite2D/Label");
		texto.Text= textoMensaje;
		juego = GetTree().Root.GetNode<Juego>("Juego");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("teleport") && tp)
		{
			juego.changeScene(instruccion);
		}
	}
	public void OnBodyEntered(Player body){
		mensaje.Visible = true;
		tp = true;
	}
	public void OnBodyExit(Player body){
		mensaje.Visible = false;
		tp = false;
	}
}
