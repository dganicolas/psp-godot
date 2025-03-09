extends HTTPRequest  

signal jugadores_obtenidos(players)
var players: Array[PlayerDto] = [] 
const API_URL = "https://webapi-a0bi.onrender.com/api/player"
@onready var http_request = get_node("HTTPRequest")
func _ready():
# Crear un HTTPRequest y agregarlo al árbol de nodos
	http_request = HTTPRequest.new()
	self.add_child(http_request)

# Conectar la señal request_completed para manejar la respuesta
	http_request.connect("request_completed", Callable(self, "_on_request_completed"))

# Llamar al método que recoge los jugadores
	recogerCincoJugadores()

# Método para realizar la solicitud
func recogerCincoJugadores():
	print("procesando")
# Verificar si ya se está realizando una solicitud
	if http_request.get_http_client_status() == HTTPClient.STATUS_REQUESTING:
		print("Aún se está procesando la solicitud anterior. Intentando más tarde.")
		return 

	var headers = ["Content-Type: application/json"]

# Realizar la solicitud GET a la API
	var error = http_request.request(API_URL, headers, HTTPClient.METHOD_GET)

# Método que se ejecuta cuando la solicitud HTTP se completa
func _on_request_completed(result: int, response_code: int, headers: Array, body: PackedByteArray) -> void:
	var json_string = body.get_string_from_utf8()
	var json = JSON.new()
	var json_data = json.parse_string(json_string)
	 # Lista vacía de PlayerDto
	
	for player_dict in json_data:
		if typeof(player_dict) == TYPE_DICTIONARY:
			var player = PlayerDto.new(
			player_dict.get("Id", ""),  # Si falta, usa ""
			player_dict.get("Name", ""),  # Si falta, usa ""
			player_dict.get("MaxScore", 0))
			players.append(player)  # Agregar a la lista de jugadoresplayers
	emit_signal("jugadores_obtenidos", players)
	print("señalemitida")
# Aquí puedes procesar o mostrar los jugadores obtenidos
