import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Main%20Screens/main_login.dart';
import 'package:http/http.dart' as http;

class AdminRegister extends StatefulWidget {
  const AdminRegister({super.key});

  @override
  AdminRegisterState createState() => AdminRegisterState();
}

class AdminRegisterState extends State<AdminRegister> {
  final TextEditingController signupFirstNameController = TextEditingController();
  final TextEditingController signupLastNameController = TextEditingController();
  final TextEditingController signupPasswordController = TextEditingController();
  final TextEditingController signupConfirmPasswordController = TextEditingController();
  final TextEditingController signupEmailController = TextEditingController();

  String? signupFirstNameErrorText;
  String? signupLastNameErrorText;
  String? signupPasswordErrorText;
  String? signupConfirmPasswordErrorText;
  String? signupEmailErrorText;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: SingleChildScrollView(
          child: Column(
            children: [
              const SizedBox(height: 100),
              const Text(
                "Admin Registration",
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
                        labelText: 'First Name',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: signupFirstNameErrorText,
                      ),
                    ),
                    const SizedBox(height: 16),
                    TextField(
                      controller: signupLastNameController,
                      decoration: InputDecoration(
                        labelText: 'Last Name',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: signupLastNameErrorText,
                      ),
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
                        errorText: signupEmailErrorText,
                      ),
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
                        errorText: signupPasswordErrorText,
                      ),
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
                        errorText: signupConfirmPasswordErrorText,
                      ),
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
                            // Check first name
                            if (signupFirstNameController.text.isEmpty) {
                              signupFirstNameErrorText = "First Name is required";
                              hasError = true;
                            } else {
                              signupFirstNameErrorText = null;
                            }
                            // Check last name
                            if (signupLastNameController.text.isEmpty) {
                              signupLastNameErrorText = "Last Name is required";
                              hasError = true;
                            } else {
                              signupLastNameErrorText = null;
                            }
                            // Check password
                            if (signupPasswordController.text.isEmpty) {
                              signupPasswordErrorText = "Password is required";
                              hasError = true;
                            } else if (signupPasswordController.text.length < 6) {
                              signupPasswordErrorText = "Password must be at least 6 characters";
                              hasError = true;
                            } else {
                              signupPasswordErrorText = null;
                            }
                            // Check confirm password
                            if (signupConfirmPasswordController.text.isEmpty) {
                              signupConfirmPasswordErrorText = "Confirm Password is required";
                              hasError = true;
                            } else if (signupPasswordController.text != signupConfirmPasswordController.text) {
                              signupConfirmPasswordErrorText = "Passwords do not match";
                              hasError = true;
                            } else {
                              signupConfirmPasswordErrorText = null;
                            }
                            // Check email
                            if (signupEmailController.text.isEmpty) {
                              signupEmailErrorText = "Email is required";
                              hasError = true;
                            } else if (!signupEmailController.text.contains('@')) {
                              signupEmailErrorText = "Email format is invalid";
                              hasError = true;
                            } else {
                              signupEmailErrorText = null;
                            }
                          });
                          if (!hasError) {
                            signUp();
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
      ),
    );
  }

  Future<void> signUp() async {
    final url = Uri.parse(adminUrl);
    final response = await http.post(
      url,
      headers: {
        'Content-Type': 'application/json',
      },
      body: json.encode({
        'firstName': signupFirstNameController.text.trim(),
        'lastName': signupLastNameController.text.trim(),
        'email': signupEmailController.text.trim(),
        'password': signupPasswordController.text.trim(),
      }),
    );

    if (response.statusCode == 200) {
      // Successfully signed up
      Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => const CustomerAndAdminLogin()));
    } else {
      // Error during signup
      print('Signup failed: ${response.statusCode}');
      // Show an error message to the user
    }
  }
}
