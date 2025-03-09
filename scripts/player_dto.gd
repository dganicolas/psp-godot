class_name PlayerDto  # Hace que Godot reconozca la clase en otros scripts

var id: String
var name: String
var max_score: int

func _init(id: String, name: String, max_score: int):
	self.id = id
	self.name = name
	self.max_score = max_score
