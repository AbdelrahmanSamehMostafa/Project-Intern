import 'package:flutter/material.dart';

class CustomerWishlist extends StatelessWidget {
  const CustomerWishlist({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
        title: const Text(
          'CustomerWishlist',
          style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
        ),
        centerTitle: true,
      ),
      body: Center(
        child: Text("CustomerWishlist page"),
      ),
    );
  }
}
