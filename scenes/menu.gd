extends Control

# Variables de entrada y de solicitud HTTP
var nombreInput : LineEdit
var textEdit : TextEdit
var httpRequest : HTTPRequest
func _ready():
	# Asignamos las referencias de los nodos
	nombreInput = $LineEdit
	textEdit = $TextEdit2
	httpRequest = $HTTPRequest
	httpRequest.connect("request_completed", Callable(self, "_on_request_completed"))
	httpRequest.connect("jugadores_obtenidos", Callable(self, "_on_jugadores_obtenidos"))
	httpRequest.recogerCincoJugadores()
	# Conectar la señal de respuesta

func _on_jugadores_obtenidos(players: Array):
	print("Jugadores obtenidos: ", players)
	
	# Construir el texto para mostrar en el TextEdit
	var jugadores_texto = ""  # Inicializamos una cadena vacía
	var index = 0
	# Recorremos cada jugador en la lista
	for player in players:
		# Construimos una línea con el nombre y la puntuación del jugador
		index +=1
		var player_line = str(index) + ")"+"Nombre: " + player.name + " | Puntuación: " + str(player.max_score) + "\n"
		jugadores_texto += player_line  # Agregamos la línea a la cadena

	# Ahora escribimos ese texto en el TextEdit
	textEdit.text = jugadores_texto
	print(jugadores_texto)
# Método para manejar el evento de jugar
func on_pressed_jugar():
	var nextScene = load("res://scenes/levels/lobby.tscn")
	get_tree().change_scene_to_file("res://scenes/levels/lobby.tscn")

# Método para salir de la aplicación
func on_pressed_salir():
	get_tree().quit()
