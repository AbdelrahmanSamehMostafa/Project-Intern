import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class CustomerReviewHistory extends StatefulWidget {
  final int custId;

  const CustomerReviewHistory({Key? key, required this.custId}) : super(key: key);

  @override
  _CustomerReviewHistoryState createState() => _CustomerReviewHistoryState();
}

class _CustomerReviewHistoryState extends State<CustomerReviewHistory> {
  List<dynamic> reviews = [];

  @override
  void initState() {
    super.initState();
    fetchReviews();
  }

  Future<void> fetchReviews() async {
    try {
      final response = await http.get(
        Uri.parse('http://localhost:5187/api/Reviews/Customer/${widget.custId}'),
        headers: {
          'Content-Type': 'application/json',
        },
      );

      if (response.statusCode == 200) {
        setState(() {
          reviews = json.decode(response.body) as List<dynamic>;
        });
      } else {
        throw Exception('Failed to load reviews: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching reviews: $e');
    }
  }

  Future<String> fetchHotelName(int hotelId) async {
    //print(hotelId);
    try {
      final response = await http.get(
        Uri.parse('http://localhost:5187/api/Hotel/$hotelId'),
        headers: {
          'Content-Type': 'application/json',
        },
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
        child: reviews.isEmpty
            ? const CircularProgressIndicator()
            : ListView.builder(
                itemCount: reviews.length,
                itemBuilder: (context, index) {
                  final review = reviews[index];
                  final hotelId = review['hotelId'];

                  // Debugging: Print hotelId to console
                  print('Fetching hotel details for hotelId: $hotelId');

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
}
