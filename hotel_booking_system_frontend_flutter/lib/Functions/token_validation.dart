import 'package:shared_preferences/shared_preferences.dart';

Future<Map<String, String>> getAuthHeaders() async {
  final prefs = await SharedPreferences.getInstance();
  final token = prefs.getString('auth_token');

  if (token == null) {
    throw const FormatException('Token is null');
  }

  return {
    'Content-Type': 'application/json',
    'Authorization': 'Bearer $token',
  };
}

// Function to delete the token when signing out
Future<void> logout() async {
  final prefs = await SharedPreferences.getInstance();
  await prefs.remove('auth_token');
}
