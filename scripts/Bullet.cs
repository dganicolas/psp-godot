using Godot;
using System;

public partial class Bullet : Area2D
{
		[Export]
		public float speedBullet = 1.0f;
		private Timer timer; // Variable para el temporizadorç
		
		
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = new Timer();
		this.AddChild(timer); // Añadir el temporizador como hijo del nodo Bullet

		// Configurar el temporizador para que dure 5 segundos
		timer.WaitTime = 1.0f; // 5 segundos
		timer.OneShot = true; // Que el temporizador solo se ejecute una vez
		timer.Start(); // Iniciar el temporizador

		// Conectar la señal de timeout para que elimine la bala
		timer.Timeout += OnTimeout;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new  Vector2(speedBullet * (float) delta, 0.0f);
	
	}
	
	public void OnBodyEntered(Node body){
		if (body is Enemigo enemigo)
		{
			GD.Print($"{enemigo.Name} ha sido alcanzado por la bala");
			this.QueueFree();
		}
	}
	
	private void OnTimeout()
	{
		GD.Print("La bala ha sido eliminada por el tiempo.");
		this.QueueFree(); // Eliminar la bala después de 5 segundos
	}
}
