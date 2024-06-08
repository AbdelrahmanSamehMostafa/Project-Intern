import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:carousel_slider/carousel_slider.dart';

import 'package:hotel_booking_system_frontend_flutter/Model/hotel.dart';

import 'package:url_launcher/url_launcher.dart';

class CustomerViewHotelDetails extends StatefulWidget {
  final Hotel hotel;
  final int custId;

  const CustomerViewHotelDetails({super.key, required this.hotel, required this.custId});

  @override
  _CustomerViewHotelDetailsState createState() => _CustomerViewHotelDetailsState();
}

class _CustomerViewHotelDetailsState extends State<CustomerViewHotelDetails> {
  late CarouselController _carouselController;
  bool isInWishlist = false; // Track if the hotel is in the wishlist initially
  double? temperature;
  int? humidity;
  double? windSpeed;
  final addressController = TextEditingController();
  List<dynamic> reviews = []; // List to hold fetched reviews
  double averageRating = 0.0;

  @override
  void initState() {
    super.initState();
    _carouselController = CarouselController();
    checkWishlistStatus(); // Check if hotel is in wishlist when widget initializes
    fetchWeatherData(); // Fetch weather data when widget initializes
    fetchReviews(); // Fetch reviews when widget initializes
  }

  Future<void> fetchReviews() async {
    final hotelId = widget.hotel.hotelId;
    final Uri uri = Uri.parse('http://localhost:5187/api/Reviews/Hotel/$hotelId');

    try {
      final response = await http.get(uri);

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
    final customerId = 1; // Replace with actual customer ID
    final hotelId = widget.hotel.hotelId;

    final Uri uri = Uri.parse('http://localhost:5187/api/Customer/$customerId/hotels');

    try {
      final response = await http.get(uri);

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
    final Uri uri = Uri.parse('http://localhost:5187/weather/$hotelId');

    try {
      final response = await http.get(uri);

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
    final customerId = 1; // Replace with actual customer ID
    final hotelId = widget.hotel.hotelId;

    final Uri uri = Uri.parse('http://localhost:5187/api/Customer/$customerId/hotels/$hotelId');

    try {
      if (isInWishlist) {
        final response = await http.delete(uri);
        if (response.statusCode == 204) {
          setState(() {
            isInWishlist = false;
          });
        } else {
          print('Failed to remove hotel from wishlist. Status code: ${response.statusCode}');
        }
      } else {
        final response = await http.post(
          uri,
          headers: <String, String>{
            'Content-Type': 'application/json; charset=UTF-8',
          },
        );

        if (response.statusCode == 200 || response.statusCode == 201) {
          setState(() {
            isInWishlist = true;
          });
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

    final Uri uri = Uri.parse('http://localhost:5187/api/Reviews/$customerId/$hotelId');

    try {
      final response = await http.post(
        uri,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode({
          'comment': comment,
          'rating': rating,
        }),
      );

      if (response.statusCode == 201) {
        // Refresh reviews and averageRating after successful submission
        fetchReviews();
      } else {
        print('Failed to submit review. Status code: ${response.statusCode}');
      }
    } catch (e) {
      print('Error submitting review: $e');
    }
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
            Navigator.pop(context); // Navigate back to the previous screen
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
              widget.hotel.description ?? 'No description available',
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
              children: widget.hotel.entertainments?.map((entertainment) {
                    return Chip(
                      label: Text(entertainment),
                    );
                  }).toList() ??
                  [const Text('No entertainments available')],
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
            const Text(
              'Images:',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 5),
            SizedBox(
              height: 200,
              child: Stack(
                children: [
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
              ),
            ),
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
    String hotelName = "${widget.hotel.name} ${widget.hotel.address.city} ${widget.hotel.address.country}";
    print(hotelName);
    String address = addressController.text;

    double hotelNameLatitude;
    double hotelNameLongitude;
    double addressLatitude;
    double addressLongitude;

    // Call API to fetch coordinates based on hotel name
    final Uri hotelNameUri = Uri.parse('http://localhost:5187/api/GoogleMaps/getCoordinates?address=$hotelName');

    try {
      final responseHotelName = await http.get(hotelNameUri);

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
    final Uri addressUri = Uri.parse('http://localhost:5187/api/GoogleMaps/getCoordinates?address=$address');

    try {
      final responseAddress = await http.get(addressUri);

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
    final routeUri = Uri.parse('http://localhost:5187/api/GoogleMaps/RouteWithETA?originLat=$hotelNameLatitude&originLng=$hotelNameLongitude&destLat=$addressLatitude&destLng=$addressLongitude');

    try {
      final responseRoute = await http.get(routeUri);

      if (responseRoute.statusCode == 200) {
        final dynamic jsonDataRoute = jsonDecode(responseRoute.body);

        var route = jsonDataRoute['route'];
        var duration = jsonDataRoute['duration'];

        launchGoogleMapsRoute(hotelNameLatitude, hotelNameLongitude, addressLatitude, addressLongitude, route);
        print('Route: $route');
        print('Duration: $duration');

        // Handle route and duration as needed (e.g., show on UI, store in state variables)
      } else {
        print('Failed to fetch route and duration. Status code: ${responseRoute.statusCode}');
      }
    } catch (e) {
      print('Error fetching route and duration: $e');
    }
  }
}
