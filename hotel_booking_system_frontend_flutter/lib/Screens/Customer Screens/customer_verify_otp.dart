import 'dart:async';
import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_homepage.dart';
import 'package:http/http.dart' as http; // Add this line

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
  final pinController1 = TextEditingController();
  final pinController2 = TextEditingController();
  final pinController3 = TextEditingController();
  final pinController4 = TextEditingController();
  final pinController5 = TextEditingController();
  final pinController6 = TextEditingController();

  void verifyOTP() async {
    String userOTP = pinController1.text + pinController2.text + pinController3.text + pinController4.text + pinController5.text + pinController6.text;

    // Replace with your backend endpoint URL
    String verifyOTPUrl = 'http://localhost:5187/api/Customer/verify-email';

    try {
      // Make POST request to verify OTP
      var response = await http.post(
        Uri.parse(verifyOTPUrl),
        headers: {
          'Content-Type': 'application/json', // Set content-type header
        },
        body: json.encode(
          {
            'email': widget.email.text, // Correctly pass the email
            'otp': userOTP,
          },
        ),
      );

      if (response.statusCode == 200) {
        // Successful OTP verification
        Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerHomePage()));
        print('Email verified successfully');
      } else {
        print('Failed to verify OTP: ${response.statusCode}');
        // Handle other status codes if needed
      }
    } catch (e) {
      print('Error verifying OTP: $e');
      // Handle error
    }
  }

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
              Image.asset("lib/assets/verify_otp.png", height: 270, width: 270
                  // Adjust size and other properties as needed
                  ),
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
                    otpTextField(controller: pinController1),
                    otpTextField(controller: pinController2),
                    otpTextField(controller: pinController3),
                    otpTextField(controller: pinController4),
                    otpTextField(controller: pinController5),
                    otpTextField(controller: pinController6),
                  ],
                ),
              ),
              const SizedBox(height: 20),
              // Row(
              //   mainAxisAlignment: MainAxisAlignment.center,
              //   children: [
              //     const SizedBox(width: 40),
              //   ],
              // ),
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
}
