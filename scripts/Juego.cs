using Godot;
using System;
using System.Collections.Generic;

public partial class Juego : Node2D
{
		private LevelManager levelManager; // Referencia al LevelManager.
		private Player playerBackup;
		private Player player;
		private Random random = new Random();
		private TextEdit texto;
		private Dictionary<string, string> levelPaths = new Dictionary<string, string>
	{
		{ "lobby", "res://scenes/levels/lobby.tscn" },
		{ "level1", "res://scenes/levels/level1.tscn" },
		{ "level2", "res://scenes/levels/level2.tscn" },
		{ "level3", "res://scenes/levels/level3.tscn" },
		{ "level4", "res://scenes/levels/level4.tscn" },
		{ "level5", "res://scenes/levels/level5.tscn" },
		{ "boss", "res://scenes/levels/boss.tscn" }
	};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		levelManager = GetTree().Root.GetNode<LevelManager>("/root/Juego/LevelManager");
		player = GetTree().Root.GetNode<Player>("/root/Juego/CharacterBody2D");
		texto = GetTree().Root.GetNode<TextEdit>("/root/Juego/TextEdit");
		playerBackup = player.ClonePlayer();
		levelManager.LoadScene(levelPaths["lobby"]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(player.vida == 0) ReloadSceneWhitDead();
	}
	private void ReloadPlayer(){
		player.QueueFree();
		player = playerBackup.ClonePlayer();
		GetTree().Root.CallDeferred("add_child", player);
		player.GlobalPosition = new Vector2(-57,-19);
	}
	
	public void LoadScene(string scenePath){
		player.GlobalPosition = new Vector2(-57,-19);
		playerBackup.QueueFree();
		playerBackup = player.ClonePlayer();
		levelManager.LoadScene(scenePath);
	}
	
	public void changeScene(String nivel){
		if(nivel != "random"){
			playerBackup = player.ClonePlayer();
			LoadScene(levelPaths[nivel]);
		}else{
			loadSceneRamdonLevel();
		}
	}
	
	public void loadSceneRamdonLevel(){
		int randomLevel = random.Next(1, 6);
		LoadScene(levelPaths[$"level{randomLevel}"]);
	}
	
	public void ReloadSceneWhitDead(){
		texto.Visible =true;
		ReloadPlayer();
		levelManager.LoadScene(levelPaths["lobby"]);;
	}
	
	public void ReloadScene(){
		levelManager.ReloadScene();
		ReloadPlayer();
		
	}
}
