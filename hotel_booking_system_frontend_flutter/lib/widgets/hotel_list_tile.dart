import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Model/hotel.dart';

class HotelListTile extends StatelessWidget {
  final Hotel hotel;
  final VoidCallback onTap;

  const HotelListTile({
    super.key,
    required this.hotel,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return ListTile(
      leading: CircleAvatar(
        radius: 30.0,
        backgroundImage: NetworkImage(hotel.imageUrls.isNotEmpty ? hotel.imageUrls.first : 'https://via.placeholder.com/150'),
      ),
      title: Text(
        hotel.name,
        style: TextStyle(fontWeight: FontWeight.bold, fontSize: 22),
      ),
      subtitle: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(' ${hotel.address.city}, ${hotel.address.country}'),
          Row(
            children: [
              Icon(Icons.star, color: Colors.yellow[700]),
              const SizedBox(width: 5),
              Text(
                hotel.rating.toStringAsFixed(1), // Adjusted rating text
                style: const TextStyle(fontSize: 18),
              ),
            ],
          ),
          //Text('Rating: ${hotel.rating.toStringAsFixed(1)}'),
        ],
      ),
      onTap: onTap,
    );
  }
}
