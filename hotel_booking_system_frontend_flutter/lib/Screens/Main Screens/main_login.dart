import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'forget_password.dart'; // Add this line

class CustomerAndAdminLogin extends StatefulWidget {
  const CustomerAndAdminLogin({super.key});

  @override
  State<CustomerAndAdminLogin> createState() => _CustomerAndAdminLoginState();
}

class _CustomerAndAdminLoginState extends State<CustomerAndAdminLogin> {
  final loginEmailController = TextEditingController();
  final loginPasswordController = TextEditingController();
  String? loginEmailErrorText;
  String? loginPasswordErrorText;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Container(
          width: 500,
          margin: const EdgeInsets.all(10),
          padding: const EdgeInsets.all(16),
          child: Column(
            children: [
              const SizedBox(height: 250),
              const Text(
                "Login To Your Account!",
                style: TextStyle(
                  color: Colors.black,
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 16),
              Container(
                margin: const EdgeInsets.all(10),
                padding: const EdgeInsets.all(16),
                child: Column(
                  children: [
                    TextField(
                      // Text field for entering email
                      controller: loginEmailController,
                      decoration: InputDecoration(
                        labelText: 'Email',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: loginEmailErrorText,
                      ),
                    ),
                    const SizedBox(height: 16),
                    TextField(
                      // Text field for entering password
                      obscureText: true,
                      controller: loginPasswordController,
                      decoration: InputDecoration(
                        labelText: 'Password',
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                        errorText: loginPasswordErrorText,
                      ),
                    ),
                    const SizedBox(height: 14),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.end,
                      children: [
                        GestureDetector(
                          onTap: () {
                            // Defines the action to perform on tapping forget password
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => const ForgetPassword(),
                              ),
                            );
                          },
                          child: const Text(
                            // Text widget for displaying 'Forget Password'
                            'Forget Password',
                            style: TextStyle(
                              color: Color.fromARGB(255, 67, 84, 236),
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),
                      ],
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
                            //check email
                            if (loginEmailController.text.isEmpty) {
                              loginEmailErrorText = "FirstName is required";
                              hasError = true;
                            } else {
                              loginEmailErrorText = null;
                            }
                            //check password
                            if (loginPasswordController.text.isEmpty) {
                              loginPasswordErrorText = "LastName is required";
                              hasError = true;
                            } else {
                              loginPasswordErrorText = null;
                            }
                          });
                          if (hasError == false) {
                            Login();
                          }
                        },
                        child: const Text("Login", style: TextStyle(fontSize: 22)),
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

  Future<void> Login() async {}
}
