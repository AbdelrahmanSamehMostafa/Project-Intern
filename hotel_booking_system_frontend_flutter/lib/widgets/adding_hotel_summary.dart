import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Model/Room.dart';

Widget buildTextFieldSummary(String label, String value) {
  return Padding(
    padding: const EdgeInsets.symmetric(vertical: 4),
    child: Row(
      children: [
        Text(
          '$label: ',
          style: const TextStyle(fontWeight: FontWeight.w600),
        ),
        Text(value),
      ],
    ),
  );
}

Widget buildRoomSummary(Room room, int index) {
  return Padding(
    padding: const EdgeInsets.symmetric(vertical: 4),
    child: Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          'Room ${index + 1}: ',
          style: const TextStyle(fontWeight: FontWeight.bold),
        ),
        Text('Room Type: ${room.roomType}'),
        Text('Price: \$${room.price.toStringAsFixed(2)}'),
        Text('Description: ${room.description}'),
        Text('Available: ${room.isAvailable ? 'Yes' : 'No'}'),
      ],
    ),
  );
}
