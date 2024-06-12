// ignore_for_file: avoid_print

import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:hotel_booking_system_frontend_flutter/Model/room_with_id.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_pick_date.dart';
import 'package:http/http.dart' as http;
import 'package:carousel_slider/carousel_slider.dart';
import 'package:hotel_booking_system_frontend_flutter/Model/hotel.dart';
import 'package:intl/intl.dart';
import 'package:url_launcher/url_launcher.dart';
//import 'package:date_time_picker/date_time_picker.dart';
//import 'package:flutter_datetime_picker/flutter_datetime_picker.dart' as dt_picker;

class CustomerViewHotelDetails extends StatefulWidget {
  final Hotel hotel;
  final int custId;

  const CustomerViewHotelDetails({super.key, required this.hotel, required this.custId});

  @override
  CustomerViewHotelDetailsState createState() => CustomerViewHotelDetailsState();
}

class CustomerViewHotelDetailsState extends State<CustomerViewHotelDetails> {
  late CarouselController _carouselController;
  bool isInWishlist = false; // Track if the hotel is in the wishlist initially
  double? temperature;
  int? humidity;
  double? windSpeed;
  final addressController = TextEditingController();
  List<dynamic> reviews = []; // List to hold fetched reviews
  double averageRating = 0.0;
  List<RoomWithId> rooms = [];
  // DateTime? _selectedcheckInDate;
  // TimeOfDay? _selectedTime;

  @override
  void initState() {
    super.initState();
    _carouselController = CarouselController();
    checkWishlistStatus(); // Check if hotel is in wishlist when widget initializes
    fetchWeatherData(); // Fetch weather data when widget initializes
    fetchReviews(); // Fetch reviews when widget initializes
    fetchRooms();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.hotel.name),
        centerTitle: true,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () {
            Navigator.pop(context);
          },
        ),
        actions: [
          TextButton(
            onPressed: toggleWishlist,
            child: Row(
              children: [
                Text(isInWishlist ? "Remove from Wishlist" : "Add to Wishlist"),
                const SizedBox(width: 10),
                Icon(
                  isInWishlist ? Icons.favorite_outlined : Icons.favorite_border,
                  color: isInWishlist ? Colors.red : null,
                ),
              ],
            ),
          ),
        ],
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Center(
              child: Hero(
                tag: 'hotelImage${widget.hotel.hotelId}',
                child: CircleAvatar(
                  radius: 80,
                  backgroundImage: NetworkImage(
                    widget.hotel.imageUrls.isNotEmpty ? widget.hotel.imageUrls.first : 'https://via.placeholder.com/150',
                  ),
                ),
              ),
            ),
            const SizedBox(height: 20),
            Text(
              widget.hotel.name,
              style: const TextStyle(fontSize: 30, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 10),
            Row(
              children: [
                Icon(Icons.star, color: Colors.yellow[700]),
                const SizedBox(width: 5),
                Text(
                  '${widget.hotel.rating.toStringAsFixed(1)} / 10',
                  style: const TextStyle(fontSize: 18),
                ),
              ],
            ),
            const SizedBox(height: 10),
            Text(
              'Total Rooms: ${widget.hotel.totalNumberOfRooms}',
              style: const TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 10),
            Text(
              'Available Rooms: ${widget.hotel.numberOfAvailableRooms}',
              style: const TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 10),
            if (temperature != null && humidity != null && windSpeed != null) ...[
              const SizedBox(height: 10),
              const Text(
                'Weather:',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 5),
              Text(
                'Temperature: $temperature °C',
                style: const TextStyle(fontSize: 18),
              ),
              const SizedBox(height: 5),
              Text(
                'Humidity: $humidity %',
                style: const TextStyle(fontSize: 18),
              ),
              const SizedBox(height: 5),
              Text(
                'Wind Speed: $windSpeed m/s',
                style: const TextStyle(fontSize: 18),
              ),
            ],
            const SizedBox(height: 10),
            const Text(
              'Address: ',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 5),
            Text(
              '${widget.hotel.address.city}, ${widget.hotel.address.country} ',
              style: const TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 10),
            const Text(
              "Write your address to generate route to the hotel",
              style: TextStyle(
                fontWeight: FontWeight.w800,
                fontStyle: FontStyle.italic,
                fontSize: 17,
              ),
            ),
            const SizedBox(height: 10),
            Row(
              children: [
                SizedBox(
                  width: 600,
                  height: 50,
                  child: TextField(
                    controller: addressController,
                    decoration: const InputDecoration(
                      labelText: 'Address',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(
                          Radius.circular(10),
                        ),
                      ),
                    ),
                  ),
                ),
                const SizedBox(width: 10),
                SizedBox(
                  height: 50,
                  child: ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                      foregroundColor: Colors.white,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10),
                      ),
                    ),
                    onPressed: generateRoute,
                    child: const Text("Generate Route", style: TextStyle(fontSize: 22)),
                  ),
                ),
              ],
            ),
            const SizedBox(height: 10),
            const Text(
              'Description:',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 5),
            Text(
              widget.hotel.description,
              style: const TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 20),
            const Text(
              'Entertainments:',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 5),
            Wrap(
              spacing: 10,
              runSpacing: 10,
              children: widget.hotel.entertainments.map((entertainment) {
                return Chip(
                  label: Text(entertainment),
                );
              }).toList(),
            ),
            const SizedBox(height: 20),
            const Text(
              'Contact Info:',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 5),
            Text(
              widget.hotel.contactInfo,
              style: const TextStyle(fontSize: 16),
            ),
            const SizedBox(height: 20),
            if (widget.hotel.imageUrls.length > 1) ...[
              const Text(
                'Images:',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 5),
              SizedBox(
                height: 200,
                child: Stack(
                  children: [
                    if (widget.hotel.imageUrls.isNotEmpty) ...[
                      CarouselSlider(
                        carouselController: _carouselController,
                        items: widget.hotel.imageUrls.map((imageUrl) {
                          return Builder(
                            builder: (BuildContext context) {
                              return GestureDetector(
                                onTap: () {
                                  Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                      builder: (context) => Scaffold(
                                        appBar: AppBar(),
                                        body: Center(
                                          child: Hero(
                                            tag: imageUrl,
                                            child: Image.network(
                                              imageUrl,
                                              fit: BoxFit.contain,
                                              height: double.infinity,
                                              width: double.infinity,
                                            ),
                                          ),
                                        ),
                                      ),
                                    ),
                                  );
                                },
                                child: Hero(
                                  tag: imageUrl,
                                  child: ClipRRect(
                                    borderRadius: BorderRadius.circular(8.0),
                                    child: Image.network(
                                      imageUrl,
                                      fit: BoxFit.cover,
                                    ),
                                  ),
                                ),
                              );
                            },
                          );
                        }).toList(),
                        options: CarouselOptions(
                          height: 200,
                          enableInfiniteScroll: false,
                          enlargeCenterPage: true,
                          viewportFraction: 0.9,
                          initialPage: 0,
                        ),
                      ),
                      Positioned(
                        top: 0,
                        bottom: 0,
                        left: 10,
                        child: IconButton(
                          icon: const Icon(Icons.arrow_back_ios),
                          onPressed: () {
                            _carouselController.previousPage();
                          },
                        ),
                      ),
                      Positioned(
                        top: 0,
                        bottom: 0,
                        right: 10,
                        child: IconButton(
                          icon: const Icon(Icons.arrow_forward_ios),
                          onPressed: () {
                            _carouselController.nextPage();
                          },
                        ),
                      ),
                    ],
                  ],
                ),
              ),
            ] else ...[
              const Text(
                'Images:',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 5),
              const SizedBox(
                height: 30,
                child: Text(
                  'Unfortunately, No Images for this hotel..',
                  style: TextStyle(fontSize: 18),
                ),
              ),
            ],
            if (rooms.isNotEmpty) ...[
              const SizedBox(height: 20),
              const Text(
                'Rooms Available:',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              // Display rooms as cards with a "Book Room" button
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: rooms.map((roomWithId) {
                  return Card(
                    margin: const EdgeInsets.symmetric(vertical: 5),
                    child: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'RoomType: ${roomWithId.roomType}',
                            style: const TextStyle(fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(height: 5),
                          Text('Description: ${roomWithId.description}'),
                          const SizedBox(height: 5),
                          Text('Price: ${roomWithId.price}'),
                          const SizedBox(height: 10),
                          // "Book Room" button
                          ElevatedButton(
                            onPressed: () {
                              // Call showBookingDialog method with room details
                              showBookingDialog(context, roomWithId, widget.custId);
                            },
                            child: const Text('Book Room'),
                          ),
                        ],
                      ),
                    ),
                  );
                }).toList(),
              ),
            ] else ...[
              const Text(
                'Rooms:',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 5),
              const SizedBox(
                height: 30,
                child: Text(
                  'Unfortunately, No Rooms available for this hotel..',
                  style: TextStyle(fontSize: 18),
                ),
              ),
            ],
            if (reviews.isNotEmpty) ...[
              const SizedBox(height: 20),
              const Text(
                'Reviews:',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              // Display reviews as cards
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: reviews.map((review) {
                  return Card(
                    margin: const EdgeInsets.symmetric(vertical: 5),
                    child: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'Rating: ${review['rating']}/10',
                            style: const TextStyle(fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(height: 5),
                          Text(review['comment']),
                          const SizedBox(height: 5),
                          Text(
                            'Posted ${formatDateTime(review['date'])}',
                            style: const TextStyle(fontStyle: FontStyle.italic),
                          ),
                        ],
                      ),
                    ),
                  );
                }).toList(),
              ),
              const SizedBox(height: 20),
              Row(
                children: [
                  Icon(Icons.star, color: Colors.yellow[700]),
                  const SizedBox(width: 5),
                  Text(
                    'Average Rating: ${averageRating.toStringAsFixed(1)}/10',
                    style: const TextStyle(fontSize: 18),
                  ),
                ],
              ),
            ],
            if (reviews.isEmpty) ...[
              const SizedBox(height: 20),
              const Text(
                'Reviews:',
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              const Row(
                children: [
                  //Icon(Icons.star, color: Colors.yellow[700]),
                  SizedBox(width: 5),
                  Text(
                    'Unfortunately, No Reviews for his hotel..',
                    style: TextStyle(fontSize: 18),
                  ),
                ],
              ),
            ],
            const SizedBox(height: 15),
            ElevatedButton(
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              onPressed: () {
                showDialog(
                  context: context,
                  builder: (BuildContext context) {
                    String comment = '';
                    int rating = 5; // Default rating

                    return AlertDialog(
                      title: const Text('Submit Review'),
                      content: Column(
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          TextField(
                            onChanged: (value) {
                              comment = value;
                            },
                            decoration: const InputDecoration(
                              labelText: 'Comment',
                            ),
                          ),
                          const SizedBox(height: 10),
                          Row(
                            children: [
                              const Text('Rating: '),
                              DropdownButton<int>(
                                value: rating,
                                onChanged: (int? newValue) {
                                  if (newValue != null) {
                                    setState(() {
                                      rating = newValue;
                                    });
                                  }
                                },
                                items: List.generate(10, (index) {
                                  return DropdownMenuItem<int>(
                                    value: index + 1,
                                    child: Text((index + 1).toString()),
                                  );
                                }),
                              ),
                            ],
                          ),
                        ],
                      ),
                      actions: [
                        TextButton(
                          onPressed: () {
                            Navigator.of(context).pop(); // Close dialog
                          },
                          child: const Text('Cancel'),
                        ),
                        ElevatedButton(
                          onPressed: () async {
                            // Perform submit logic here
                            await submitReview(comment, rating);
                            Navigator.of(context).pop(); // Close dialog
                          },
                          child: const Text('Submit'),
                        ),
                      ],
                    );
                  },
                );
              },
              child: const Text('Add Your Review'),
            ),
          ],
        ),
      ),
    );
  }

  void showBookingDialog(BuildContext context, RoomWithId room, int custId) {
    DateTime checkInDate = DateTime.now();
    DateTime checkOutDate = DateTime.now();

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return StatefulBuilder(
          builder: (context, setState) {
            return AlertDialog(
              title: Text('Book ${room.roomType}'),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  CustomerPickDate(
                    dateIn: checkInDate,
                    dateOut: checkOutDate,
                    onDateInChanged: (newDate) {
                      setState(() {
                        checkInDate = newDate;
                      });
                    },
                    onDateOutChanged: (newDate) {
                      setState(() {
                        checkOutDate = newDate;
                      });
                    },
                  ),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                  child: const Text('Cancel'),
                ),
                TextButton(
                  onPressed: () {
                    showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                          title: const Text('Confirm Booking'),
                          content: Text(
                            'Are you sure you want to book this room from ${DateFormat('yyyy-MM-dd').format(checkInDate)} to ${DateFormat('yyyy-MM-dd').format(checkOutDate)}?',
                          ),
                          actions: [
                            TextButton(
                              onPressed: () {
                                Navigator.of(context).pop();
                              },
                              child: const Text('No'),
                            ),
                            TextButton(
                              onPressed: () {
                                confirmBook(custId, room.roomId, checkInDate, checkOutDate);
                                Navigator.of(context).pop(); // Close confirmation dialog
                                Navigator.of(context).pop(); // Close booking dialog
                              },
                              child: const Text('Yes'),
                            ),
                          ],
                        );
                      },
                    );
                  },
                  child: const Text('Confirm'),
                ),
              ],
            );
          },
        );
      },
    );
  }

  void launchGoogleMapsRoute(double originLat, double originLng, double destLat, double destLng, String route) async {
    // Construct the URL
    final url = 'https://www.google.com/maps/dir/?api=1&origin=$originLat,$originLng&destination=$destLat,$destLng&travelmode=driving&dir_action=navigate';

    // Print the generated URL
    print('Generated Google Maps URL: $url');

    // Create a Uri object from the URL string
    final uri = Uri.parse(url);

    // Check if the URL can be launched
    if (await canLaunchUrl(uri)) {
      await launchUrl(uri);
    } else {
      throw 'Could not launch $url';
    }
  }

  void generateRoute() async {
    final headers = await getAuthHeaders();
    String hotelName = "${widget.hotel.name} ${widget.hotel.address.city} ${widget.hotel.address.country}";
    print(hotelName);
    String address = addressController.text;

    double hotelNameLatitude;
    double hotelNameLongitude;
    double addressLatitude;
    double addressLongitude;

    // Call API to fetch coordinates based on hotel name
    final Uri hotelNameUri = Uri.parse('$googleMapsUrl/getCoordinates?address=$hotelName');

    try {
      final responseHotelName = await http.get(
        hotelNameUri,
        headers: headers,
      );

      if (responseHotelName.statusCode == 200) {
        final dynamic jsonDataHotelName = jsonDecode(responseHotelName.body);

        hotelNameLatitude = double.parse(jsonDataHotelName['latitude'].toString());
        hotelNameLongitude = double.parse(jsonDataHotelName['longitude'].toString());
        print('Hotel Name Latitude: $hotelNameLatitude');
        print('Hotel Name Longitude: $hotelNameLongitude');
      } else {
        print('Failed to fetch hotel name coordinates. Status code: ${responseHotelName.statusCode}');
        return;
      }
    } catch (e) {
      print('Error fetching hotel name coordinates: $e');
      return;
    }

    // Call API to fetch coordinates based on address
    final Uri addressUri = Uri.parse('$googleMapsUrl/getCoordinates?address=$address');

    try {
      final responseAddress = await http.get(
        addressUri,
        headers: headers,
      );

      if (responseAddress.statusCode == 200) {
        final dynamic jsonDataAddress = jsonDecode(responseAddress.body);

        addressLatitude = double.parse(jsonDataAddress['latitude'].toString());
        addressLongitude = double.parse(jsonDataAddress['longitude'].toString());
        print('Address Latitude: $addressLatitude');
        print('Address Longitude: $addressLongitude');
      } else {
        print('Failed to fetch address coordinates. Status code: ${responseAddress.statusCode}');
        return;
      }
    } catch (e) {
      print('Error fetching address coordinates: $e');
      return;
    }

    // Call API to fetch route and ETA
    final routeUri = Uri.parse('$googleMapsUrl/RouteWithETA?originLat=$hotelNameLatitude&originLng=$hotelNameLongitude&destLat=$addressLatitude&destLng=$addressLongitude');

    try {
      final responseRoute = await http.get(
        routeUri,
        headers: headers,
      );

      if (responseRoute.statusCode == 200) {
        final dynamic jsonDataRoute = jsonDecode(responseRoute.body);

        var route = jsonDataRoute['route'];
        var duration = jsonDataRoute['duration'];

        launchGoogleMapsRoute(hotelNameLatitude, hotelNameLongitude, addressLatitude, addressLongitude, route);
        print('Route: $route');
        print('Duration: $duration');
      } else {
        print('Failed to fetch route and duration. Status code: ${responseRoute.statusCode}');
      }
    } catch (e) {
      print('Error fetching route and duration: $e');
    }
  }

  Future<void> confirmBook(int customerId, int roomId, DateTime checkIn, DateTime checkOut) async {
    final Uri uri = Uri.parse('$bookingUrl/$customerId/$roomId');
    final headers = await getAuthHeaders();

    final body = jsonEncode({
      "checkInDate": checkIn.toUtc().toIso8601String(),
      "checkOutDate": checkOut.toUtc().toIso8601String(),
      "status": "Confirmed",
    });

    try {
      final response = await http.post(
        uri,
        headers: headers,
        body: body,
      );

      if (response.statusCode == 200) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Booking completed successfully. Check your booking history.')),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Failed to complete this booking.')),
        );
        print('Failed to confirm booking. Status code: ${response.statusCode}');
        print('Response body: ${response.body}');
      }
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Error confirming booking. Please try again later.')),
      );
      print('Error confirming booking: $e');
    }
  }

  Future<void> fetchRooms() async {
    final hotelId = widget.hotel.hotelId;
    final Uri uri = Uri.parse('$roomsUrl/Hotel/$hotelId');
    final headers = await getAuthHeaders();

    try {
      final response = await http.get(
        uri,
        headers: headers,
      );

      if (response.statusCode == 200) {
        final List<dynamic> fetchedData = jsonDecode(response.body);

        setState(() {
          rooms = fetchedData.map((roomJson) => RoomWithId.fromJson(roomJson)).where((room) => room.isAvailable == true && room.isBooked == false).toList();
        });
      } else {
        print('Failed to fetch rooms. Status code: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching rooms: $e');
    }
  }

  Future<void> fetchReviews() async {
    final hotelId = widget.hotel.hotelId;
    final Uri uri = Uri.parse('$reviewUrl/HotelReviews/$hotelId');
    final headers = await getAuthHeaders();

    try {
      final response = await http.get(
        uri,
        headers: headers,
      );

      if (response.statusCode == 200) {
        final Map<String, dynamic> fetchedData = jsonDecode(response.body);

        setState(() {
          reviews = fetchedData['reviews'];
          averageRating = fetchedData['averageRating'];
        });
      } else {
        print('Failed to fetch reviews. Status code: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching reviews: $e');
    }
  }

  Future<void> checkWishlistStatus() async {
    final customerId = widget.custId;
    final hotelId = widget.hotel.hotelId;
    final headers = await getAuthHeaders();

    final Uri uri = Uri.parse('$customerUrl/$customerId/hotels');

    try {
      final response = await http.get(
        uri,
        headers: headers,
      );

      if (response.statusCode == 200) {
        final List<dynamic> wishlistHotels = jsonDecode(response.body);
        setState(() {
          isInWishlist = wishlistHotels.any((hotel) => hotel['hotelId'] == hotelId);
        });
      } else {
        print('Failed to fetch wishlist hotels. Status code: ${response.statusCode}');
      }
    } catch (e) {
      print('Error checking wishlist status: $e');
    }
  }

  Future<void> fetchWeatherData() async {
    final hotelId = widget.hotel.hotelId;
    final headers = await getAuthHeaders();
    final Uri uri = Uri.parse('$weatherUrl/$hotelId');

    try {
      final response = await http.get(
        uri,
        headers: headers,
      );

      if (response.statusCode == 200) {
        final dynamic jsonData = jsonDecode(response.body);

        setState(() {
          temperature = jsonData['temperature'];
          humidity = jsonData['humidity'];
          windSpeed = jsonData['windSpeed'];
        });
      } else {
        print('Failed to fetch weather data. Status code: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching weather data: $e');
    }
  }

  Future<void> toggleWishlist() async {
    final customerId = widget.custId;
    final hotelId = widget.hotel.hotelId;
    final headers = await getAuthHeaders();
    final Uri uri = Uri.parse('$customerUrl/$customerId/hotels/$hotelId');

    try {
      if (isInWishlist) {
        final response = await http.delete(
          uri,
          headers: headers,
        );
        if (response.statusCode == 204) {
          setState(() {
            isInWishlist = false;
          });
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(content: Text('Hotel removed from your Wishlist')),
          );
        } else {
          print('Failed to remove hotel from wishlist. Status code: ${response.statusCode}');
        }
      } else {
        final response = await http.post(
          uri,
          headers: headers,
        );

        if (response.statusCode == 200 || response.statusCode == 201) {
          setState(() {
            isInWishlist = true;
          });
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(content: Text('Hotel added to your Wishlist')),
          );
        } else {
          print('Failed to add hotel to wishlist. Status code: ${response.statusCode}');
        }
      }
    } catch (e) {
      print('Error adding/removing hotel to/from wishlist: $e');
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

  Future<void> submitReview(String comment, int rating) async {
    final customerId = widget.custId;
    final hotelId = widget.hotel.hotelId;

    final Uri uri = Uri.parse('$reviewUrl/$customerId/$hotelId');
    final headers = await getAuthHeaders();

    try {
      final response = await http.post(
        uri,
        headers: headers,
        body: jsonEncode({
          'comment': comment,
          'rating': rating,
        }),
      );

      if (response.statusCode == 201) {
        fetchReviews(); // Refresh reviews and averageRating after successful submission
      } else {
        print('Failed to submit review. Status code: ${response.statusCode}');
      }
    } catch (e) {
      print('Error submitting review: $e');
    }
  }
}
