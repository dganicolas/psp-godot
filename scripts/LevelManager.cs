using Godot;

public partial class LevelManager : Node
{

	private Node currentScene;
	private string RouteScene;

	public override void _Ready()
	{
	}

	public void LoadScene(string scenePath)
	{
		RouteScene = scenePath;
		if (currentScene != null)
		{
			currentScene. CallDeferred("queue_free");; // Limpia la escena actual.
		}

		var nextScene = GD.Load<PackedScene>(scenePath);
		if (nextScene != null)
		{
			currentScene = nextScene.Instantiate();
			GetTree().Root.CallDeferred("add_child", currentScene); // Añade la nueva escena.
		}
		else
		{
			GD.PrintErr($"No se pudo cargar la escena en {scenePath}");
		}
	}
	
	public void ReloadScene(){
		LoadScene(RouteScene);
	}
}
