import 'package:flutter/material.dart';
import '../../Model/hotel.dart';

class CustomerViewHotelDetails extends StatelessWidget {
  final Hotel hotel;

  const CustomerViewHotelDetails({super.key, required this.hotel});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(hotel.name),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text('Hotel Name: ${hotel.name}', style: TextStyle(fontSize: 18)),
            Text('Rating: ${hotel.rating.toStringAsFixed(1)}', style: TextStyle(fontSize: 18)),
            Text('Available Rooms: ${hotel.numberOfAvailableRooms}', style: TextStyle(fontSize: 18)),
            // Add more details as needed
          ],
        ),
      ),
    );
  }
}
