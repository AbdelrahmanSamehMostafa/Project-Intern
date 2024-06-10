import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:hotel_booking_system_frontend_flutter/Model/hotel.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_account_info.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_booking_history.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_review_history.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_view_hotel_details.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_wishlist.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Main%20Screens/welcome_screen.dart';
import 'package:hotel_booking_system_frontend_flutter/widgets/hotel_list_tile.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class CustomerHomePage extends StatefulWidget {
  final int custId;

  const CustomerHomePage({super.key, required this.custId});

  @override
  CustomerHomePageState createState() => CustomerHomePageState();
}

class CustomerHomePageState extends State<CustomerHomePage> {
  late List<Hotel> hotels = []; // List to store fetched hotels
  late List<Hotel> filteredHotels = []; // List to store filtered hotels
  String searchQuery = '';
  String selectedSortOption = 'none'; // Default sorting option

  @override
  void initState() {
    super.initState();
    fetchHotels();
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
          TextButton(
            onPressed: () => _signOut(context),
            child: const Row(
              children: [
                Icon(Icons.logout, color: Colors.white),
                SizedBox(width: 5),
                Text(
                  'Logout',
                  style: TextStyle(color: Colors.white),
                ),
              ],
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
              leading: const Icon(
                Icons.history,
                color: Colors.blue,
              ),
              title: const Text('Booking History'),
              onTap: navigateToBookingHistory,
            ),
            ListTile(
              leading: const Icon(
                Icons.rate_review,
                color: Colors.blue,
              ),
              title: const Text('Review History'),
              onTap: navigateToReviewHistory,
            ),
            ListTile(
              leading: const Icon(
                Icons.favorite,
                color: Colors.red,
              ),
              title: const Text('Wishlist'),
              onTap: navigateToWishlist,
            ),
            ListTile(
              leading: const Icon(
                Icons.account_circle_rounded,
                color: Colors.blue,
              ),
              title: const Text('Account'),
              onTap: navigateToMyAccount,
            ),
          ],
        ),
      ),
      body: Column(
        children: [
          const SizedBox(height: 20),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 8.0),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    onChanged: updateSearchQuery,
                    style: const TextStyle(color: Colors.black),
                    decoration: InputDecoration(
                      hintText: 'Search hotels...',
                      hintStyle: const TextStyle(color: Colors.black54),
                      filled: true,
                      fillColor: Colors.grey[300],
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(30.0),
                        borderSide: BorderSide.none,
                      ),
                      prefixIcon: const Icon(Icons.search, color: Colors.black),
                    ),
                  ),
                ),
                const SizedBox(width: 10),
                const Text(
                  "OrderBy: ",
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(width: 10),
                DropdownButton<String>(
                  value: selectedSortOption,
                  icon: const Icon(Icons.sort, color: Colors.black),
                  onChanged: (String? newValue) {
                    setState(() {
                      selectedSortOption = newValue!;
                      fetchHotels(orderBy: selectedSortOption);
                    });
                  },
                  items: <String>['none', 'rating', 'availablerooms', 'address', 'name'].map<DropdownMenuItem<String>>((String value) {
                    return DropdownMenuItem<String>(
                      value: value,
                      child: Text(
                        value[0].toUpperCase() + value.substring(1),
                        style: const TextStyle(color: Colors.black),
                      ),
                    );
                  }).toList(),
                ),
              ],
            ),
          ),
          const SizedBox(height: 20),
          Expanded(
            child: hotels.isEmpty
                ? const Center(child: CircularProgressIndicator())
                : ListView.builder(
                    itemCount: filteredHotels.length,
                    itemBuilder: (context, index) {
                      final hotel = filteredHotels[index];
                      return HotelListTile(
                        hotel: hotel,
                        onTap: () => navigateToHotelDetails(hotel),
                      );
                    },
                  ),
          ),
        ],
      ),
    );
  }

  Future<void> fetchHotels({String orderBy = ''}) async {
    final url = Uri.parse('$hotelUrl?orderBy=$orderBy');
    final headers = await getAuthHeaders();

    try {
      final response = await http.get(url, headers: headers);

      if (response.statusCode == 200) {
        // If the server returns a 200 OK response, parse the JSON
        final List<dynamic> jsonData = json.decode(response.body);
        setState(() {
          hotels = jsonData.map((json) => Hotel.fromJson(json)).toList();
          filteredHotels = hotels;
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
    Navigator.push(context, MaterialPageRoute(builder: (context) => CustomerHomePage(custId: widget.custId)));
  }

  void navigateToMyAccount() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => CustomerAccountInfo(custId: widget.custId)));
  }

  void navigateToBookingHistory() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerBookingHistory()));
  }

  void navigateToReviewHistory() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => CustomerReviewHistory(custId: widget.custId)));
  }

  void navigateToWishlist() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => CustomerWishlist(customerId: widget.custId)));
  }

  void navigateToHotelDetails(Hotel hotel) {
    Navigator.push(context, MaterialPageRoute(builder: (context) => CustomerViewHotelDetails(hotel: hotel, custId: widget.custId)));
  }

  void updateSearchQuery(String query) {
    setState(() {
      searchQuery = query;
      filteredHotels = hotels.where((hotel) => hotel.name.toLowerCase().contains(query.toLowerCase())).toList();
    });
  }

  Future<void> _signOut(BuildContext context) async {
    logout();
    // Navigate back to welcome screen
    Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => const WelcomeScreen()));
  }
}
