import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_verify_otp.dart';
import 'package:http/http.dart' as http; // Add this line

class CustomerRegister extends StatefulWidget {
  const CustomerRegister({super.key});

  @override
  State<CustomerRegister> createState() => _CustomerRegisterState();
}

class _CustomerRegisterState extends State<CustomerRegister> {
  final signupFirstNameController = TextEditingController();
  final signupLastNameController = TextEditingController();
  final signupPasswordController = TextEditingController();
  final signupConfirmPasswordController = TextEditingController();
  final signupEmailController = TextEditingController();

  String? signupFirstNameErrorText;
  String? signupLastNameErrorText;
  String? signupPasswordErrorText;
  String? signupConfirmPasswordErrorText;
  String? signupEmailErrorText;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          children: [
            const SizedBox(height: 250),
            const Text(
              "Customer Registration",
              style: TextStyle(
                color: Colors.black,
                fontSize: 24,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 16),
            Container(
              width: 500,
              margin: const EdgeInsets.all(10),
              padding: const EdgeInsets.all(16),
              child: Column(
                children: [
                  TextField(
                    controller: signupFirstNameController,
                    decoration: InputDecoration(
                        labelText: 'FirstName',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: signupFirstNameErrorText),
                  ),
                  const SizedBox(height: 16),
                  TextField(
                    controller: signupLastNameController,
                    decoration: InputDecoration(
                        labelText: 'LastName',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: signupLastNameErrorText),
                  ),
                  const SizedBox(height: 16),
                  TextField(
                    controller: signupEmailController,
                    decoration: InputDecoration(
                        labelText: 'Email',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: signupEmailErrorText),
                  ),
                  const SizedBox(height: 16),
                  TextField(
                    obscureText: true,
                    controller: signupPasswordController,
                    decoration: InputDecoration(
                        labelText: 'Password',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: signupPasswordErrorText),
                  ),
                  const SizedBox(height: 16),
                  TextField(
                    obscureText: true,
                    controller: signupConfirmPasswordController,
                    decoration: InputDecoration(
                        labelText: 'Confirm Password',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: signupConfirmPasswordErrorText),
                  ),
                  const SizedBox(height: 16),
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
                      onPressed: () {
                        bool hasError = false;
                        setState(() {
                          //check first name
                          if (signupFirstNameController.text.isEmpty) {
                            signupFirstNameErrorText = "FirstName is required";
                            hasError = true;
                          } else {
                            signupFirstNameErrorText = null;
                          }
                          //check last name
                          if (signupLastNameController.text.isEmpty) {
                            signupLastNameErrorText = "LastName is required";
                            hasError = true;
                          } else {
                            signupLastNameErrorText = null;
                          }
                          //check password
                          if (signupPasswordController.text.isEmpty) {
                            signupPasswordErrorText = "Password is required";
                            hasError = true;
                          } else if (signupPasswordController.text.length < 6) {
                            signupPasswordErrorText = "Username must be at lease 6 characters";
                            hasError = true;
                          } else {
                            signupPasswordErrorText = null;
                          }
                          //check confirm password
                          if (signupConfirmPasswordController.text.isEmpty) {
                            signupConfirmPasswordErrorText = "Confirm Password is required";
                            hasError = true;
                          } else if (signupPasswordController.text != signupConfirmPasswordController.text &&
                              signupPasswordController.text.isNotEmpty &&
                              signupConfirmPasswordController.text.isNotEmpty) {
                            signupConfirmPasswordErrorText = "Passwords don't match!";
                            hasError = true;
                          } else {
                            signupConfirmPasswordErrorText = null;
                          } //check email
                          if (signupEmailController.text.isEmpty) {
                            signupEmailErrorText = "Email is required";
                            hasError = true;
                          } else if (!signupEmailController.text.contains("@")) {
                            signupEmailErrorText = "Email format is not valid";
                            hasError = true;
                          } else {
                            signupEmailErrorText = null;
                          }
                        });
                        if (hasError == false) {
                          registerAndSendOTP();
                        }
                      },
                      child: const Text("Register", style: TextStyle(fontSize: 22)),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  Future<void> registerAndSendOTP() async {
    final url = Uri.parse(customerUrl);
    final response = await http.post(
      url,
      headers: {
        'Content-Type': 'application/json',
      },
      body: json.encode({
        'firstName': signupFirstNameController.text,
        'lastName': signupLastNameController.text,
        'email': signupEmailController.text,
        'password': signupPasswordController.text,
        'wishlists': [],
      }),
    );

    if (response.statusCode == 201) {
      // Successfully signed up
      print('Signup successful');
      Navigator.push(
        context,
        MaterialPageRoute(
          builder: (context) => CustomerVerifyOTP(
            email: signupEmailController,
            firstName: signupFirstNameController,
            lastName: signupLastNameController,
            password: signupPasswordController,
          ),
        ),
      );
    } else {
      // Error during signup
      print('Signup failed: ${response.statusCode}');
    }
  }
}
