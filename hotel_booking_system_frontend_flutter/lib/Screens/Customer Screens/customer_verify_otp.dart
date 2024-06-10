// ignore_for_file: use_build_context_synchronously

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Main%20Screens/main_login.dart';
import 'package:http/http.dart' as http;

class CustomerVerifyOTP extends StatefulWidget {
  const CustomerVerifyOTP({super.key, required this.email, required this.firstName, required this.lastName, required this.password});
  final TextEditingController email;
  final TextEditingController firstName;
  final TextEditingController lastName;
  final TextEditingController password;

  @override
  CustomerVerifyOTPState createState() => CustomerVerifyOTPState();
}

class CustomerVerifyOTPState extends State<CustomerVerifyOTP> {
  final digit1 = TextEditingController();
  final digit2 = TextEditingController();
  final digit3 = TextEditingController();
  final digit4 = TextEditingController();
  final digit5 = TextEditingController();
  final digit6 = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Verify OTP',
          style: TextStyle(fontSize: 32),
        ),
        centerTitle: true,
      ),
      body: SafeArea(
        child: Container(
          margin: const EdgeInsets.only(top: 100),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              Image.asset("lib/assets/verify_otp.png", height: 270, width: 270),
              const SizedBox(height: 15),
              const Text(
                "Check Your Email",
                style: TextStyle(
                  fontSize: 32,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const Text(
                "OTP has been sent to",
                style: TextStyle(
                  fontSize: 32,
                  fontWeight: FontWeight.bold,
                ),
              ),
              Text(
                widget.email.text,
                style: const TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 22),
              Container(
                margin: const EdgeInsets.all(20),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    otpTextField(controller: digit1),
                    otpTextField(controller: digit2),
                    otpTextField(controller: digit3),
                    otpTextField(controller: digit4),
                    otpTextField(controller: digit5),
                    otpTextField(controller: digit6),
                  ],
                ),
              ),
              const SizedBox(height: 20),
              const SizedBox(height: 40),
              SizedBox(
                width: 400,
                height: 60,
                child: ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                    foregroundColor: Colors.white,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  onPressed: verifyOTP,
                  child: const Text(
                    "Verify OTP",
                    style: TextStyle(fontSize: 24),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget otpTextField({required TextEditingController controller}) {
    return Container(
      height: 60,
      width: 45,
      margin: const EdgeInsets.symmetric(horizontal: 5),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(15),
        color: Colors.grey[400],
      ),
      child: TextFormField(
        onChanged: (value) {
          if (value.length == 1) {
            // Automatically move focus to the next field when a digit is entered
            FocusScope.of(context).nextFocus();
          }
        },
        style: const TextStyle(
          color: Colors.white,
          fontSize: 25,
        ),
        keyboardType: TextInputType.number,
        textAlign: TextAlign.center,
        inputFormatters: [
          LengthLimitingTextInputFormatter(1),
          FilteringTextInputFormatter.digitsOnly,
        ],
        controller: controller,
        decoration: const InputDecoration(
          border: InputBorder.none,
        ),
      ),
    );
  }

  void verifyOTP() async {
    String userOTP = digit1.text + digit2.text + digit3.text + digit4.text + digit5.text + digit6.text;

    final headers = await getAuthHeaders();

    try {
      var response = await http.post(
        Uri.parse(verifyEmailUrl),
        headers: headers,
        body: json.encode(
          {
            'email': widget.email.text,
            'otp': userOTP,
          },
        ),
      );

      if (response.statusCode == 200) {
        // Successful OTP verification
        Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerAndAdminLogin()));
        print('Email verified successfully');
      } else {
        print('Failed to verify OTP: ${response.statusCode}');
      }
    } catch (e) {
      print('Error verifying OTP: $e');
    }
  }
}
