using Godot;
using System;
using System.Text;
using System.Text.Json;

public partial class Lobby : Node2D
{
	private Player player;
	private Button btnEnviarSolicitud;
	private Godot.HttpRequest httpRequest;
	private Timer _timer;
	private TextEdit textoUsuario;
	private Button btnGuardaNombre;
	private String nombre = "usuario";

	public override void _Ready()
	{
		// Obtener nodos
		player = GetNode<Player>("CharacterBody2D");
		btnEnviarSolicitud = GetNode<Button>("CharacterBody2D/BtnEnviarSolicitud");
		btnGuardaNombre = GetNode<Button>("CharacterBody2D/btnGuardaNombre");
		textoUsuario = GetNode<TextEdit>("CharacterBody2D/textoUsuario");
		httpRequest = GetNode<Godot.HttpRequest>("HTTPRequest");
		_timer = GetNode<Timer>("Timer"); // Asegúrate de que tengas un Timer en la escena
 		_timer.Connect("timeout", new Callable(this, nameof(_on_Timer_timeout)));
		_timer.WaitTime = 2.0f;  // Establece el tiempo de espera (en segundos)
		_timer.OneShot = true;  
		httpRequest.Connect("request_completed", new Callable(this, nameof(OnRequestCompleted)));
		
		if (player == null)
		{
			GD.Print("No se pudo encontrar el nodo Player.");
		}

		if (btnEnviarSolicitud != null)
		{
			btnEnviarSolicitud.Pressed += OnBtnEnviarSolicitudPressed;
		}
		else
		{
			GD.Print("No se encontró el botón de enviar solicitud.");
		}
	}

	public override void _Process(double delta)
	{
		if (player != null && player.vida < 0)
		{
			GetTree().ReloadCurrentScene();
		}
	}

	private void OnBtnEnviarSolicitudPressed()
	{
		GD.Print("Botón presionado, enviando solicitud HTTP...");

		string url = "https://webapi-a0bi.onrender.com/api/player";
		var monedas = player.monedas;
		var playerData = $"{{\"name\": \"{nombre}\", \"maxScore\": {monedas}}}";
		GD.Print(playerData);
		var headers = new string[] { "Content-Type: application/json" };
		var error = httpRequest.Request(url, headers, HttpClient.Method.Put, playerData);

		if (error != Error.Ok)
		{
			GD.Print("Error al enviar la solicitud HTTP: " + error);
		}
		else
		{
			GD.Print("Solicitud enviada con éxito.");
			_timer.Start();
		}
	}

	// Método para manejar la respuesta de la solicitud HTTP
	private void OnRequestCompleted(int result, int responseCode, string body)
	{
		GD.Print("Solicitud completada. Código de respuesta: " + responseCode);
		GD.Print("Cuerpo de la respuesta: " + body);
		GD.Print(responseCode);
		
		if (responseCode == 200)
		{
			GD.Print("Solicitud exitosa.");
			 // Inicia el timer para esperar 2 segundos antes de cambiar la escena
		}
		else
		{
			GD.Print("Error en la solicitud HTTP. Código: " + responseCode);
		}
	}

	// Este método se ejecutará cuando el Timer termine
	private void _on_Timer_timeout()
	{
		GD.Print("El timer terminó. Cambiando de escena...");
		GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
	}

	private void OnBtnGuardarNombrePressed()
	{
		GD.Print("Botón presionado, guardando nombre");
		if (textoUsuario != null)
		{
			if (textoUsuario.Text != "")
				nombre = textoUsuario.Text;
			GD.Print("Nombre guardado: " + nombre);

			// Hacer invisible e inactivo
			textoUsuario.Visible = false;
			textoUsuario.Editable = false;
		}
		if (btnGuardaNombre != null)
		{
			btnGuardaNombre.Visible = false;
			btnGuardaNombre.Disabled = true;
		}
	}

	public void OnBodyEnteredKillzone(Player body)
	{
		GetTree().ReloadCurrentScene();
	}
}
