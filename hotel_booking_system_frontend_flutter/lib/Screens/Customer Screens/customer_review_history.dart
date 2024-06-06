import 'package:flutter/material.dart';

class CustomerReviewHistory extends StatelessWidget {
  const CustomerReviewHistory({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
        title: const Text(
          'CustomerReviewHistory',
          style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
        ),
        centerTitle: true,
      ),
      body: Center(
        child: Text("CustomerReviewHistory page"),
      ),
    );
  }
}
