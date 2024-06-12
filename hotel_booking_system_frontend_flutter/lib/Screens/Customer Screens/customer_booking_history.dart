import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:http/http.dart' as http;
import 'package:intl/intl.dart';

class CustomerBookingHistory extends StatefulWidget {
  final int customerId;

  const CustomerBookingHistory({Key? key, required this.customerId}) : super(key: key);

  @override
  _CustomerBookingHistoryState createState() => _CustomerBookingHistoryState();
}

class _CustomerBookingHistoryState extends State<CustomerBookingHistory> {
  List<Map<String, dynamic>> bookings = [];

  @override
  void initState() {
    super.initState();
    fetchBookings(); // Fetch bookings data when the widget initializes
  }

  Future<void> fetchBookings() async {
    final Uri uri = Uri.parse('$bookingUrl/CustomerBookings/${widget.customerId}');
    final headers = await getAuthHeaders(); // Your function to get auth headers

    try {
      final response = await http.get(
        uri,
        headers: headers,
      );

      if (response.statusCode == 200) {
        final List<dynamic> jsonList = jsonDecode(response.body);
        setState(() {
          bookings = jsonList.map((item) => item as Map<String, dynamic>).toList();
        });
      } else {
        print('Failed to fetch bookings. Status code: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching bookings: $e');
    }
  }

  String formatDateTime(String dateTimeString) {
    DateTime dateTime = DateTime.parse(dateTimeString);
    Duration difference = DateTime.now().difference(dateTime);

    if (difference.inSeconds < 5) {
      return 'just now';
    } else if (difference.inMinutes < 1) {
      return '${difference.inSeconds} seconds ago';
    } else if (difference.inHours < 1) {
      return '${difference.inMinutes} minutes ago';
    } else if (difference.inDays < 1) {
      return '${difference.inHours} hours ago';
    } else if (difference.inDays < 7) {
      return '${difference.inDays} days ago';
    } else {
      int years = (difference.inDays / 365).floor();
      return years <= 1 ? 'one year ago' : '$years years ago';
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
        title: const Text(
          'Customer Booking History',
          style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
        ),
        centerTitle: true,
      ),
      body: bookings.isEmpty
          ? const Center(
              child: CircularProgressIndicator(), // Show loading indicator while fetching data
            )
          : ListView.builder(
              itemCount: bookings.length,
              itemBuilder: (context, index) {
                final booking = bookings[index];
                return Card(
                  elevation: 3,
                  margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 16),
                  child: ListTile(
                    title: Text('Booking ${index + 1}'),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Room ID: ${booking['roomId']}'),
                        Text('Check-in Date: ${DateFormat('dd-MM-yyyy').format(DateTime.parse(booking['checkInDate']))}'),
                        Text('Check-out Date: ${DateFormat('dd-MM-yyyy').format(DateTime.parse(booking['checkOutDate']))}'),
                        Text('Status: ${booking['status']}'),
                      ],
                    ),
                  ),
                );
              },
            ),
    );
  }
}
