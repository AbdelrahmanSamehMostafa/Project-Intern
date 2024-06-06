import 'package:flutter/material.dart';

class CustomerAccountInfo extends StatelessWidget {
  const CustomerAccountInfo({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
        title: const Text(
          'CustomerAccountInfo',
          style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
        ),
        centerTitle: true,
      ),
      body: Center(
        child: Text("CustomerAccountInfo"),
      ),
    );
  }
}
