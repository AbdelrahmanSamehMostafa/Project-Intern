import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Admin%20Screens/admin_homepage.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_homepage.dart';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';

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

  Future<bool> getAdminStatus(int adminId) async {
    final Uri uri = Uri.parse('$adminUrl/$adminId/status');

    try {
      final response = await http.get(uri);

      if (response.statusCode == 200) {
        final jsonResponse = jsonDecode(response.body);
        return jsonResponse['isActive'] as bool;
      } else {
        print('Failed to fetch admin status: ${response.statusCode}');
        return false;
      }
    } catch (e) {
      print('Error fetching admin status: $e');
      return false;
    }
  }

  Future<void> login() async {
    final Uri uri = Uri.parse(loginUrl);
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
          await saveToken(token); // Save the token
          decodeToken(token); // Decode the token
        } else {
          print('Unexpected response format: ${response.body}');
        }
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Please Enter Valid credentials!'),
          ),
        );
        print('HTTP Error: ${response.statusCode}');
      }
    } catch (e) {
      print('Error: $e');
    }
  }

  Future<void> saveToken(String token) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString('auth_token', token);
  }

  Future<void> decodeToken(String token) async {
    try {
      final parts = token.split('.');
      if (parts.length != 3) {
        throw const FormatException('Invalid token');
      }

      final payload = parts[1];
      final String decodedPayload = utf8.decode(base64Url.decode(base64.normalize(payload)));
      final Map<String, dynamic> decodedMap = json.decode(decodedPayload);
      print('Decoded JWT Payload: $decodedMap'); // Print the payload
      final role = decodedMap['Role'] as String?;
      final id = int.tryParse(decodedMap['sub'].toString()) ?? 0;

      if (role == null) {
        throw const FormatException('Role is null');
      }

      if (role == 'Customer') {
        Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => CustomerHomePage(custId: id)));
      } else if (role == 'Admin') {
        final isActive = await getAdminStatus(id);
        if (isActive) {
          Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => const AdminHomePage()));
        } else {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
              content: Text('Wait for the super admin to accept your request...'),
            ),
          );
        }
      } else {
        print('Unknown role: $role');
      }
    } catch (e) {
      print('Error decoding token: $e');
    }
  }
}
