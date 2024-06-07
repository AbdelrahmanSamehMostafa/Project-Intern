import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Admin%20Screens/admin_homepage.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_homepage.dart';
import 'package:http/http.dart' as http;

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

  Future<void> login() async {
    final Uri uri = Uri.parse('http://localhost:5187/api/Authentication/Login');
    final Map<String, String> headers = {'Content-Type': 'application/json'};
    final Map<String, dynamic> body = {
      'email': loginEmailController.text.trim(),
      'password': loginPasswordController.text.trim(),
    };

    try {
      final response = await http.post(uri, headers: headers, body: jsonEncode(body));

      if (response.statusCode == 200) {
        final jsonResponse = jsonDecode(response.body);
        if (jsonResponse.containsKey('token')) {
          final token = jsonResponse['token'] as String;
          decodeToken(token); // Call the decodeToken method with the obtained token
        } else {
          // Handle unexpected response format
          print('Unexpected response format: ${response.body}');
        }
      } else {
        // Handle non-200 status code
        print('HTTP Error: ${response.statusCode}');
      }
    } catch (e) {
      // Handle network errors or JSON parsing errors
      print('Error: $e');
    }
  }

  void decodeToken(String token) {
    try {
      final parts = token.split('.');
      if (parts.length != 3) {
        throw FormatException('Invalid token');
      }

      final payload = parts[1];
      final String decodedPayload = utf8.decode(base64Url.decode(base64.normalize(payload)));
      final Map<String, dynamic> decodedMap = json.decode(decodedPayload);

      final role = decodedMap['role'] as String;

      // Ensure to parse 'id' field as int
      final customerId = int.tryParse(decodedMap['id'].toString()) ?? 0;

      // Navigate based on role
      if (role == 'Customer') {
        Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => CustomerHomePage(custId: customerId)));
      } else if (role == 'Admin') {
        Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => const AdminHomePage()));
      } else {
        // Handle unknown role
        print('Unknown role: $role');
      }
    } catch (e) {
      print('Error decoding token: $e');
    }
  }

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
                        onPressed: login,
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
}
