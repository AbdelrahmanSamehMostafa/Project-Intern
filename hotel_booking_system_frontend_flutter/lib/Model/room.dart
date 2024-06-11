class Room {
  final String roomType;
  final double price;
  final String description;
  final bool isAvailable;

  Room({
    required this.roomType,
    required this.price,
    required this.description,
    required this.isAvailable,
  });

  Map<String, dynamic> toJson() {
    return {
      'roomType': roomType,
      'price': price,
      'description': description,
      'isAvailable': isAvailable,
    };
  }
}
