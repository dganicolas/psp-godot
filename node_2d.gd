extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$HTTPRequest.request_completed.connect(_on_request_completed)
	$HTTPRequest.request("https://api.github.com/repos/godotengine/godot/releases/latest")

func _on_request_completed(result, response_code, headers, body):
	var json = JSON.parse_string(body.get_string_from_utf8())
	print(json["name"])
	print("prueba")
	print(json)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
