import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class AdminAccountInfo extends StatefulWidget {
  final int adminId;

  const AdminAccountInfo({super.key, required this.adminId});

  @override
  AdminAccountInfoState createState() => AdminAccountInfoState();
}

class AdminAccountInfoState extends State<AdminAccountInfo> {
  Map<String, dynamic>? adminData;
  bool isLoading = true;
  bool hasError = false;

  TextEditingController nameController = TextEditingController();
  TextEditingController emailController = TextEditingController();

  @override
  void initState() {
    super.initState();
    fetchAdminData();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
        title: const Text(
          'Admin Account Info',
          style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
        ),
        centerTitle: true,
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : hasError
              ? const Center(child: Text('Error loading admin data'))
              : Padding(
                  padding: const EdgeInsets.all(16.0),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Admin Details',
                        style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                      ),
                      const SizedBox(height: 20),
                      _buildTextRow('Name', adminData!['name']),
                      const SizedBox(height: 10),
                      _buildTextRow('Email', adminData!['email']),
                      const SizedBox(height: 20),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          SizedBox(
                            height: 40,
                            width: 300,
                            child: ElevatedButton(
                              style: ElevatedButton.styleFrom(
                                backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                                foregroundColor: Colors.white,
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(10),
                                ),
                              ),
                              onPressed: () {
                                // Open dialog for updating info
                                _showUpdateDialog();
                              },
                              child: const Text(
                                'Update your data',
                                style: TextStyle(fontSize: 20),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ],
                  ),
                ),
    );
  }

  Widget _buildTextRow(String label, String value) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Text(
          label,
          style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
        ),
        Text(
          value,
          style: const TextStyle(fontSize: 18),
        ),
      ],
    );
  }

  Future<void> _showUpdateDialog() async {
    return showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Update Your Information'),
          content: SingleChildScrollView(
            child: Column(
              children: <Widget>[
                TextField(
                  controller: nameController,
                  decoration: const InputDecoration(labelText: 'Name'),
                ),
                TextField(
                  controller: emailController,
                  decoration: const InputDecoration(labelText: 'Email'),
                ),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text('Update'),
              onPressed: () {
                Navigator.of(context).pop();
                updateAdmin();
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> fetchAdminData() async {
    final url = Uri.parse('$adminUrl/${widget.adminId}');
    final headers = await getAuthHeaders();

    try {
      final response = await http.get(url, headers: headers);

      if (response.statusCode == 200) {
        setState(() {
          adminData = json.decode(response.body);
          isLoading = false;

          // Initialize text controllers with fetched data
          nameController.text = adminData!['name'];
          emailController.text = adminData!['email'];
        });
      } else {
        throw Exception('Failed to load admin data');
      }
    } catch (e) {
      setState(() {
        isLoading = false;
        hasError = true;
      });
    }
  }

  Future<void> updateAdmin() async {
    final updatedName = nameController.text;
    final updatedEmail = emailController.text;

    final url = Uri.parse('$adminUrl/${widget.adminId}');
    final headers = await getAuthHeaders();

    try {
      final response = await http.put(
        url,
        headers: headers,
        body: json.encode({
          'adminId': widget.adminId,
          'name': updatedName,
          'email': updatedEmail,
        }),
      );

      if (response.statusCode == 204) {
        // Successful update
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Admin information updated')),
        );
        fetchAdminData(); // Refresh the displayed data after update
      } else {
        throw Exception('Failed to update admin information');
      }
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Failed to update admin information')),
      );
    }
  }
}
