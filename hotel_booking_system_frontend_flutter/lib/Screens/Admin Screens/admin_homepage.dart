import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:hotel_booking_system_frontend_flutter/Model/hotel.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Admin%20Screens/admin_account_info.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Admin%20Screens/admin_add_hotel.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Admin%20Screens/admin_view_hotel_details.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Main%20Screens/welcome_screen.dart';
import 'package:hotel_booking_system_frontend_flutter/widgets/hotel_list_tile.dart';
import 'package:http/http.dart' as http;

class AdminHomePage extends StatefulWidget {
  final int adminId;

  const AdminHomePage({super.key, required this.adminId});

  @override
  AdminHomePageState createState() => AdminHomePageState();
}

class AdminHomePageState extends State<AdminHomePage> {
  List<Hotel> adminHotels = []; // List to store fetched admin hotels
  bool isLoading = false;
  bool isError = false;

  @override
  void initState() {
    super.initState();
    fetchAdminHotels();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 84, 85, 87),
        title: const Text(
          'Admin Homepage',
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
                Icons.account_circle_rounded,
                color: Colors.blue,
              ),
              title: const Text('My Account'),
              onTap: navigateToMyAccount,
            ),
            ListTile(
              leading: const Icon(
                Icons.home_work_outlined,
                color: Colors.blue,
              ),
              title: const Text('Add Hotel'),
              onTap: navigateToAdminAddHotel,
            ),
            ListTile(
              leading: const Icon(
                Icons.logout,
                color: Colors.blue,
              ),
              title: const Text('Logout'),
              onTap: () => _signOut(context),
            ),
          ],
        ),
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : isError
              ? const Center(
                  child: Text(
                    'Failed to load admin hotels',
                    style: TextStyle(fontSize: 35),
                  ),
                )
              : adminHotels.isEmpty
                  ? const Center(
                      child: Text(
                        'No hotels found',
                        style: TextStyle(fontSize: 35),
                      ),
                    )
                  : ListView.builder(
                      itemCount: adminHotels.length,
                      itemBuilder: (context, index) {
                        final hotel = adminHotels[index];
                        return HotelListTile(
                          hotel: hotel,
                          onTap: () => navigateToHotelDetailsAdminPOV(hotel),
                        );
                      },
                    ),
    );
  }

  Future<void> fetchAdminHotels() async {
    setState(() {
      isLoading = true;
    });

    final Uri uri = Uri.parse('$hotelUrl/admin/${widget.adminId}');
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
            adminHotels = jsonData.map((json) => Hotel.fromJson(json)).toList();
            isLoading = false;
          });
        } else {
          throw Exception('Failed to parse admin hotels data');
        }
      } else if (response.statusCode == 404) {
        // No hotels found
        setState(() {
          isLoading = false;
        });
      } else {
        throw Exception('Failed to load admin hotels: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching admin hotels: $e');
      setState(() {
        isLoading = false;
        isError = true;
      });
    }
  }

  void navigateToHotelDetailsAdminPOV(Hotel hotel) {
    Navigator.push(context, MaterialPageRoute(builder: (context) => AdminViewHotelDetails(hotelId: hotel.hotelId)));
  }

  void navigateToHome() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => AdminHomePage(adminId: widget.adminId)));
  }

  void navigateToMyAccount() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => AdminAccountInfo(adminId: widget.adminId)));
  }

  void navigateToAdminAddHotel() {
    Navigator.push(context, MaterialPageRoute(builder: (context) => AdminAddHotel(adminId: widget.adminId)));
  }

  Future<void> _signOut(BuildContext context) async {
    logout();
    // Navigate back to welcome screen
    Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => const WelcomeScreen()));
  }
}
