import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class CustomerAccountInfo extends StatefulWidget {
  final int custId;

  const CustomerAccountInfo({super.key, required this.custId});

  @override
  CustomerAccountInfoState createState() => CustomerAccountInfoState();
}

class CustomerAccountInfoState extends State<CustomerAccountInfo> {
  Map<String, dynamic>? customerData;
  bool isLoading = true;
  bool hasError = false;

  TextEditingController nameController = TextEditingController();
  TextEditingController emailController = TextEditingController();

  @override
  void initState() {
    super.initState();
    fetchCustomerData();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
        title: const Text(
          'Customer Account Info',
          style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
        ),
        centerTitle: true,
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : hasError
              ? const Center(child: Text('Error loading customer data'))
              : Padding(
                  padding: const EdgeInsets.all(16.0),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Customer Details',
                        style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                      ),
                      const SizedBox(height: 20),
                      _buildTextRow('Name', customerData!['name']),
                      const SizedBox(height: 10),
                      _buildTextRow('Email', customerData!['email']),
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
                updateCustomer();
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> fetchCustomerData() async {
    final url = Uri.parse('$customerUrl/${widget.custId}');
    final headers = await getAuthHeaders();

    try {
      final response = await http.get(url, headers: headers);

      if (response.statusCode == 200) {
        setState(() {
          customerData = json.decode(response.body);
          isLoading = false;

          // Initialize text controllers with fetched data
          nameController.text = customerData!['name'];
          emailController.text = customerData!['email'];
        });
      } else {
        throw Exception('Failed to load customer data');
      }
    } catch (e) {
      setState(() {
        isLoading = false;
        hasError = true;
      });
    }
  }

  Future<void> updateCustomer() async {
    final updatedName = nameController.text;
    final updatedEmail = emailController.text;

    final url = Uri.parse('$customerUrl/${widget.custId}');
    final headers = await getAuthHeaders();

    try {
      final response = await http.put(
        url,
        headers: headers,
        body: json.encode({
          'customerId': widget.custId,
          'name': updatedName,
          'email': updatedEmail,
        }),
      );

      if (response.statusCode == 204) {
        // Successful update
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Customer information updated')),
        );
        fetchCustomerData(); // Refresh the displayed data after update
      } else {
        throw Exception('Failed to update customer information');
      }
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Failed to update customer information')),
      );
    }
  }
}
