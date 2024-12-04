using Godot;
using System;
using System.Collections.Generic;

public partial class Juego : Node2D
{
	//que sea jugable y que tenga un menu
		private Timer timer;
		private LevelManager levelManager; // Referencia al LevelManager.
		private Player playerBackup;
		private Player player;
		private int nivelActual = 0;
		private TextEdit texto;
		private Dictionary<string, string> levelPaths = new Dictionary<string, string>
	{
		{ "lobby", "res://scenes/levels/lobby.tscn" },
		{ "level1", "res://scenes/levels/level1.tscn" },
		{ "level2", "res://scenes/levels/level2.tscn" },
		{ "level3", "res://scenes/levels/level3.tscn" },
		{ "level4", "res://scenes/levels/level4.tscn" },
		{ "level5", "res://scenes/levels/level5.tscn" },
		{ "level6", "res://scenes/levels/boss.tscn" }
	};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = new Timer();
		timer.WaitTime = 1.0f;
		timer.OneShot = true;
		timer.Timeout += OnTimerTimeout;
		AddChild(timer);
		levelManager = GetTree().Root.GetNode<LevelManager>("/root/Juego/LevelManager");
		player = GetTree().Root.GetNode<Player>("/root/Juego/CharacterBody2D");
		texto = GetTree().Root.GetNode<TextEdit>("/root/Juego/TextEdit");
		playerBackup = player.ClonePlayer();
		levelManager.LoadScene(levelPaths["lobby"]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(player.vida == 0) ReloadScene();
	}
	private void ReloadPlayer(){
	playerBackup.ReloadPlayer(player);
	player.GlobalPosition = new Vector2(-57, -19);
	}
	
	public void LoadScene(string scenePath){
		player.GlobalPosition = new Vector2(-57,-19);
		playerBackup.QueueFree();
		playerBackup = player.ClonePlayer();
		levelManager.LoadScene(scenePath);
	}
	
	public void changeScene(String nivel){
		if(nivel =="salir"){
			var nextScene = ResourceLoader.Load<PackedScene>("res://scenes/despedida.tscn");
			GetTree().ChangeSceneToPacked(nextScene);
		}else{
			LoadScene(levelPaths[nivel]);
		}
			
	}
	
	private void OnTimerTimeout()
	{
		texto.Visible = false;
	}
	
	public void ReloadScene(){
		texto.Visible =true;
		timer.Start();
		levelManager.ReloadScene();
		ReloadPlayer();
		
	}
}
