import 'package:flutter/material.dart';

class CustomerBookingHistory extends StatelessWidget {
  const CustomerBookingHistory({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
        title: const Text(
          'CustomerBookingHistory',
          style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
        ),
        centerTitle: true,
      ),
      body: Center(
        child: Text("CustomerBookingHistory page"),
      ),
    );
  }
}
