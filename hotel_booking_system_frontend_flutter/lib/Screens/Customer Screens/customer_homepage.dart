import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/Customer_account_info.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_booking_history.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_review_history.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_wishlist.dart';
import 'package:http/http.dart' as http;

import '../../Model/hotel.dart';
import 'Customer_view_hotel_details.dart'; // Import the new page

class CustomerHomePage extends StatefulWidget {
  const CustomerHomePage({Key? key}) : super(key: key);

  @override
  _CustomerHomePageState createState() => _CustomerHomePageState();
}

class _CustomerHomePageState extends State<CustomerHomePage> {
  late List<Hotel> hotels = []; // List to store fetched hotels

  @override
  void initState() {
    super.initState();
    fetchHotels();
  }

  Future<void> fetchHotels() async {
    final url = Uri.parse('http://localhost:5187/api/Hotel');

    try {
      final response = await http.get(url);

      if (response.statusCode == 200) {
        // If the server returns a 200 OK response, parse the JSON
        final List<dynamic> jsonData = json.decode(response.body);
        setState(() {
          hotels = jsonData.map((json) => Hotel.fromJson(json)).toList();
          print(hotels);
        });
      } else {
        // If the server returns an error response, throw an exception
        throw Exception('Failed to load hotels');
      }
    } catch (e) {
      print('Error fetching hotels: $e');
      // Handle error, show an error message to the user
    }
  }

  void navigateToHome() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerHomePage()));
  }

  void navigateToMyAccount() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerAccountInfo()));
  }

  void navigateToBookingHistory() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerBookingHistory()));
  }

  void navigateToReviewHistory() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerReviewHistory()));
  }

  void navigateToWishlist() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerWishlist()));
  }

  void navigateToHotelDetails(Hotel hotel) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => CustomerViewHotelDetails(hotel: hotel),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 84, 85, 87),
        title: const Text(
          'Customer Homepage',
          style: TextStyle(color: Colors.white),
        ),
        centerTitle: true,
        actions: [
          TextButton(
            onPressed: navigateToHome,
            child: const Text(
              'Home',
              style: TextStyle(color: Colors.white),
            ),
          ),
          TextButton(
            onPressed: navigateToMyAccount,
            child: const Text(
              'My Account',
              style: TextStyle(color: Colors.white),
            ),
          ),
        ],
      ),
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: <Widget>[
            const DrawerHeader(
              decoration: BoxDecoration(
                color: Color.fromARGB(255, 84, 85, 87),
              ),
              child: Text(
                'Menu',
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 24,
                ),
              ),
            ),
            ListTile(
              leading: const Icon(Icons.history),
              title: const Text('Booking History'),
              onTap: navigateToBookingHistory,
            ),
            ListTile(
              leading: const Icon(Icons.rate_review),
              title: const Text('Review History'),
              onTap: navigateToReviewHistory,
            ),
            ListTile(
              leading: const Icon(Icons.favorite),
              title: const Text('Wishlist'),
              onTap: navigateToWishlist,
            ),
            ListTile(
              leading: const Icon(Icons.account_circle_rounded),
              title: const Text('Account'),
              onTap: navigateToMyAccount,
            ),
          ],
        ),
      ),
      body: hotels.isEmpty
          ? const Center(child: CircularProgressIndicator())
          : Center(
              child: SingleChildScrollView(
                scrollDirection: Axis.vertical,
                child: SingleChildScrollView(
                  scrollDirection: Axis.horizontal,
                  child: DataTable(
                    columns: const [
                      DataColumn(label: Text('Hotel Name')),
                      DataColumn(label: Text('Rating')),
                      DataColumn(label: Text('Available Rooms')),
                    ],
                    rows: hotels
                        .map(
                          (hotel) => DataRow(
                            cells: [
                              DataCell(
                                Text(hotel.name),
                                onTap: () => navigateToHotelDetails(hotel),
                              ),
                              DataCell(Text(hotel.rating.toStringAsFixed(1))),
                              DataCell(Text(hotel.numberOfAvailableRooms.toString())),
                            ],
                          ),
                        )
                        .toList(),
                  ),
                ),
              ),
            ),
    );
  }
}
