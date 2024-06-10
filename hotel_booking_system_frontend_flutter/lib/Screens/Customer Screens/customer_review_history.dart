import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class CustomerReviewHistory extends StatefulWidget {
  final int custId;

  const CustomerReviewHistory({super.key, required this.custId});

  @override
  CustomerReviewHistoryState createState() => CustomerReviewHistoryState();
}

class CustomerReviewHistoryState extends State<CustomerReviewHistory> {
  List<dynamic> reviews = [];

  bool isLoading = false;
  bool isError = false;

  @override
  void initState() {
    super.initState();
    fetchReviews();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
        title: const Text(
          'Customer Review History',
          style: TextStyle(fontSize: 28, fontWeight: FontWeight.bold),
        ),
        centerTitle: true,
      ),
      body: Center(
        child: isLoading
            ? const CircularProgressIndicator()
            : isError
                ? const Center(
                    child: Text('Failed to load review history'),
                  )
                : reviews.isEmpty
                    ? const Center(
                        child: Text(
                          'No review history available',
                          style: TextStyle(fontSize: 35),
                        ),
                      )
                    : ListView.builder(
                        itemCount: reviews.length,
                        itemBuilder: (context, index) {
                          final review = reviews[index];
                          final hotelId = review['hotelId'];
                          return FutureBuilder(
                            future: fetchHotelName(hotelId),
                            builder: (context, AsyncSnapshot<String> snapshot) {
                              if (snapshot.connectionState == ConnectionState.waiting) {
                                return const CircularProgressIndicator();
                              } else if (snapshot.hasError) {
                                return const Text('Error loading hotel name');
                              } else {
                                final hotelName = snapshot.data ?? 'Unknown Hotel';
                                return Card(
                                  margin: const EdgeInsets.all(10),
                                  child: ListTile(
                                    title: Column(
                                      crossAxisAlignment: CrossAxisAlignment.start,
                                      children: [
                                        Text('Hotel Name: $hotelName'),
                                        Row(
                                          children: [
                                            Icon(
                                              Icons.star,
                                              color: Colors.yellow[700],
                                            ),
                                            const SizedBox(width: 3),
                                            Text('Rating: ${review['rating']}'),
                                          ],
                                        ),
                                        Text('Comment: ${review['comment']}'),
                                      ],
                                    ),
                                  ),
                                );
                              }
                            },
                          );
                        },
                      ),
      ),
    );
  }

  Future<void> fetchReviews() async {
    setState(() {
      isLoading = true;
    });

    final headers = await getAuthHeaders();

    try {
      final response = await http.get(
        Uri.parse('$reviewUrl/CustomerReviews/${widget.custId}'),
        headers: headers,
      );

      if (response.statusCode == 200) {
        setState(() {
          reviews = json.decode(response.body) as List<dynamic>;
          isLoading = false;
        });
      } else if (response.statusCode == 404) {
        // No review history found
        setState(() {
          isLoading = false;
        });
      } else {
        throw Exception('Failed to load reviews: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching reviews: $e');
      setState(() {
        isLoading = false;
        isError = true;
      });
    }
  }

  Future<String> fetchHotelName(int hotelId) async {
    final headers = await getAuthHeaders();
    try {
      final response = await http.get(
        Uri.parse('$hotelUrl/$hotelId'),
        headers: headers,
      );

      if (response.statusCode == 200) {
        final hotel = json.decode(response.body);
        return hotel['name'];
      } else {
        throw Exception('Failed to load hotel details: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching hotel details: $e');
      return 'Unknown Hotel';
    }
  }
}
