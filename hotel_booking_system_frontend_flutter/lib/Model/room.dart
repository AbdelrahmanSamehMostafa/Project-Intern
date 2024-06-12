class Room {
  final String roomType;
  final double price;
  final String description;
  final bool isAvailable;
  final bool isBooked;

  Room({
    required this.roomType,
    required this.price,
    required this.description,
    required this.isAvailable,
    required this.isBooked,
  });

  Map<String, dynamic> toJson() {
    return {
      'roomType': roomType,
      'price': price,
      'description': description,
      'isAvailable': isAvailable,
      'isBooked': isBooked,
    };
  }
}
