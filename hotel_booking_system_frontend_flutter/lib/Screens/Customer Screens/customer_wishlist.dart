import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:hotel_booking_system_frontend_flutter/Model/hotel.dart';
import 'package:hotel_booking_system_frontend_flutter/widgets/hotel_list_tile.dart';
import 'package:http/http.dart' as http;

import 'customer_view_hotel_details.dart';

class CustomerWishlist extends StatefulWidget {
  final int customerId;

  const CustomerWishlist({super.key, required this.customerId});

  @override
  CustomerWishlistState createState() => CustomerWishlistState();
}

class CustomerWishlistState extends State<CustomerWishlist> {
  List<Hotel> wishlistHotels = []; // List to store fetched wishlist hotels
  bool isLoading = false;
  bool isError = false;

  @override
  void initState() {
    super.initState();
    fetchWishlistHotels();
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
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : isError
              ? const Center(
                  child: Text('Failed to load wishlist hotels'),
                )
              : wishlistHotels.isEmpty
                  ? const Center(
                      child: Text(
                        'Your wishlist is empty',
                        style: TextStyle(fontSize: 35),
                      ),
                    )
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

  Future<void> fetchWishlistHotels() async {
    setState(() {
      isLoading = true;
    });

    final Uri uri = Uri.parse('$customerUrl/${widget.customerId}/hotels');
    final headers = await getAuthHeaders();

    try {
      final response = await http.get(
        uri,
        headers: headers,
      );

      if (response.statusCode == 200) {
        final dynamic jsonData = jsonDecode(response.body);

        // Check if jsonData is not null and is a List
        if (jsonData != null && jsonData is List) {
          setState(() {
            wishlistHotels = jsonData.map((json) => Hotel.fromJson(json)).toList();
            isLoading = false;
          });
        } else {
          throw Exception('Failed to parse wishlist hotels data');
        }
      } else if (response.statusCode == 404) {
        // Wishlist is empty
        setState(() {
          isLoading = false;
        });
      } else {
        throw Exception('Failed to load wishlist hotels: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching wishlist hotels: $e');
      setState(() {
        isLoading = false;
        isError = true;
      });
    }
  }

  void navigateToHotelDetails(Hotel hotel) {
    Navigator.push(context, MaterialPageRoute(builder: (context) => CustomerViewHotelDetails(hotel: hotel, custId: widget.customerId)));
  }
}
