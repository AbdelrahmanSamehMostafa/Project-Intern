import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_homepage.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_verify_otp.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Main%20Screens/welcome_screen.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Hotel Booking System',
      home: WelcomeScreen(),
    );
  }
}
