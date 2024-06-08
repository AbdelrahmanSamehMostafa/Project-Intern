import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import '../../Model/hotel.dart';
import '../../widgets/hotel_list_tile.dart';
import 'customer_view_hotel_details.dart';

class CustomerWishlist extends StatefulWidget {
  final int customerId;

  const CustomerWishlist({super.key, required this.customerId});

  @override
  CustomerWishlistState createState() => CustomerWishlistState();
}

class CustomerWishlistState extends State<CustomerWishlist> {
  late List<Hotel> wishlistHotels = []; // List to store fetched wishlist hotels

  @override
  void initState() {
    super.initState();
    fetchWishlistHotels();
  }

  Future<void> fetchWishlistHotels() async {
    final Uri uri = Uri.parse('http://localhost:5187/api/Customer/${widget.customerId}/hotels');

    try {
      final response = await http.get(uri);

      if (response.statusCode == 200) {
        final dynamic jsonData = jsonDecode(response.body);

        // Check if jsonData is not null and is a List
        if (jsonData != null && jsonData is List) {
          setState(() {
            wishlistHotels = jsonData.map((json) => Hotel.fromJson(json)).toList();
          });
        } else {
          throw Exception('Failed to parse wishlist hotels data');
        }
      } else {
        throw Exception('Failed to load wishlist hotels: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching wishlist hotels: $e');
      // Handle error, show an error message to the user
    }
  }

  void navigateToHotelDetails(Hotel hotel) {
    Navigator.push(context, MaterialPageRoute(builder: (context) => CustomerViewHotelDetails(hotel: hotel, custId: widget.customerId)));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Wishlist',
          style: TextStyle(fontSize: 30),
        ),
        centerTitle: true,
        backgroundColor: Colors.grey,
      ),
      body: wishlistHotels.isEmpty
          ? const Center(child: CircularProgressIndicator())
          : ListView.builder(
              itemCount: wishlistHotels.length,
              itemBuilder: (context, index) {
                final hotel = wishlistHotels[index];
                return HotelListTile(
                  hotel: hotel,
                  onTap: () => navigateToHotelDetails(hotel),
                );
              },
            ),
    );
  }
}
