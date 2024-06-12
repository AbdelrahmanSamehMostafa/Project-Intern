class RoomWithId {
  final int roomId;
  final String roomType;
  final String description;
  final double price;
  final bool isAvailable;
  final bool isBooked;

  RoomWithId({
    required this.roomId,
    required this.roomType,
    required this.description,
    required this.price,
    required this.isAvailable,
    required this.isBooked,
  });

  factory RoomWithId.fromJson(Map<String, dynamic> json) {
    return RoomWithId(
      roomId: json['roomId'],
      roomType: json['roomType'],
      description: json['description'],
      price: json['price'],
      isAvailable: json['isAvailable'],
      isBooked: json['isBooked'],
    );
  }
}
