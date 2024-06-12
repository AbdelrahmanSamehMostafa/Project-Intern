import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:hotel_booking_system_frontend_flutter/Model/Room.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';

class AdminAddHotel extends StatefulWidget {
  final int adminId;
  const AdminAddHotel({super.key, required this.adminId});

  @override
  AdminAddHotelState createState() => AdminAddHotelState();
}

class AdminAddHotelState extends State<AdminAddHotel> {
  final TextEditingController nameController = TextEditingController();
  final TextEditingController descriptionController = TextEditingController();
  final TextEditingController cityController = TextEditingController();
  final TextEditingController countryController = TextEditingController();
  final TextEditingController contactInfoController = TextEditingController();
  final TextEditingController entertainmentsController = TextEditingController();
  final TextEditingController imageUrlsController = TextEditingController();
  final TextEditingController averageRatingController = TextEditingController();

  List<Room> rooms = []; // List to store added rooms
  List<String> entertainments = [];
  List<String> imageUrls = [];

  bool isLoading = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 84, 85, 87),
        title: const Text(
          'Admin Add Hotel',
          style: TextStyle(color: Colors.white),
        ),
        centerTitle: true,
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : Padding(
              padding: const EdgeInsets.all(16.0),
              child: SingleChildScrollView(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: [
                    const Text(
                      "Hotel:",
                      style: TextStyle(fontWeight: FontWeight.bold, fontSize: 30),
                    ),
                    const SizedBox(height: 12),
                    TextField(
                      controller: nameController,
                      decoration: const InputDecoration(
                        labelText: 'Name',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(height: 12),
                    TextField(
                      controller: descriptionController,
                      decoration: const InputDecoration(
                        labelText: 'Description',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                      ),
                      maxLines: 3,
                    ),
                    const SizedBox(height: 12),
                    TextField(
                      controller: contactInfoController,
                      decoration: const InputDecoration(
                        labelText: 'Contact Info',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(height: 12),
                    TextField(
                      controller: averageRatingController,
                      decoration: const InputDecoration(
                        labelText: 'Initial Rating',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                      ),
                      keyboardType: TextInputType.number,
                    ),
                    const SizedBox(height: 12),
                    const Text(
                      "Entertainments:",
                      style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                    ),
                    const SizedBox(height: 12),
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: entertainments.map((entertainment) {
                        return ListTile(
                          title: Text(entertainment),
                          trailing: IconButton(
                            icon: const Icon(Icons.remove_circle),
                            onPressed: () {
                              setState(() {
                                entertainments.remove(entertainment);
                              });
                            },
                          ),
                        );
                      }).toList(),
                    ),
                    TextField(
                      controller: entertainmentsController,
                      decoration: InputDecoration(
                        labelText: 'Add Entertainment',
                        suffixIcon: IconButton(
                          icon: const Icon(Icons.add),
                          onPressed: () {
                            setState(() {
                              entertainments.add(entertainmentsController.text);
                              entertainmentsController.clear();
                            });
                          },
                        ),
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(height: 12),
                    const Text(
                      "Image URLs:",
                      style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                    ),
                    const SizedBox(height: 12),
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: imageUrls.map((imageUrl) {
                        return ListTile(
                          title: Text(imageUrl),
                          trailing: IconButton(
                            icon: const Icon(Icons.remove_circle),
                            onPressed: () {
                              setState(() {
                                imageUrls.remove(imageUrl);
                              });
                            },
                          ),
                        );
                      }).toList(),
                    ),
                    TextField(
                      controller: imageUrlsController,
                      decoration: InputDecoration(
                        labelText: 'Add Image URL',
                        suffixIcon: IconButton(
                          icon: const Icon(Icons.add),
                          onPressed: () {
                            setState(() {
                              imageUrls.add(imageUrlsController.text);
                              imageUrlsController.clear();
                            });
                          },
                        ),
                        border: const OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(height: 12),
                    const Text(
                      "Address:",
                      style: TextStyle(fontWeight: FontWeight.bold, fontSize: 30),
                    ),
                    const SizedBox(height: 12),
                    TextField(
                      controller: cityController,
                      decoration: const InputDecoration(
                        labelText: 'City',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(height: 12),
                    TextField(
                      controller: countryController,
                      decoration: const InputDecoration(
                        labelText: 'Country',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.all(
                            Radius.circular(10),
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(height: 20),
                    const Text(
                      "Rooms:",
                      style: TextStyle(fontWeight: FontWeight.bold, fontSize: 30),
                    ),
                    const SizedBox(height: 12),
                    if (rooms.isEmpty)
                      const Padding(
                        padding: EdgeInsets.symmetric(vertical: 10),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(
                              'No rooms added yet',
                              style: TextStyle(fontStyle: FontStyle.italic, fontSize: 20),
                            ),
                          ],
                        ),
                      ),
                    if (rooms.isNotEmpty)
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: rooms.asMap().entries.map((entry) {
                          final int index = entry.key;
                          final Room room = entry.value;
                          return buildRoomCard(room, index);
                        }).toList(),
                      ),
                    ElevatedButton(
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                        foregroundColor: Colors.white,
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      onPressed: () => _addRoomDialog(context),
                      child: const Text('Add Room'),
                    ),
                    const SizedBox(height: 20),
                    const Text(
                      "Summary:",
                      style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                    ),
                    const SizedBox(height: 12),
                    _buildSummary(),
                    const SizedBox(height: 20),
                    ElevatedButton(
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                        foregroundColor: Colors.white,
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      onPressed: addHotel,
                      child: const Text('Submit Hotel'),
                    ),
                  ],
                ),
              ),
            ),
    );
  }

  Future<void> _addRoomDialog(BuildContext context) async {
    final TextEditingController priceController = TextEditingController();
    final TextEditingController descriptionController = TextEditingController();
    bool isAvailable = true; // Default value
    String roomType = 'Single'; // Default value
    bool isBooked = false;

    await showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Add Room'),
          content: SingleChildScrollView(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.min,
              children: [
                Row(
                  children: [
                    const Text('Room Type'),
                    const SizedBox(width: 15),
                    DropdownButton<String>(
                      value: roomType,
                      onChanged: (String? newValue) {
                        setState(() {
                          roomType = newValue!;
                        });
                      },
                      items: <String>['Single', 'Double', 'Triple', 'Quadra'].map<DropdownMenuItem<String>>((String value) {
                        return DropdownMenuItem<String>(
                          value: value,
                          child: Text(value),
                        );
                      }).toList(),
                    ),
                  ],
                ),
                const SizedBox(height: 12),
                TextField(
                  controller: priceController,
                  decoration: const InputDecoration(
                    labelText: 'Price',
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.all(
                        Radius.circular(10),
                      ),
                    ),
                  ),
                  keyboardType: TextInputType.number,
                ),
                const SizedBox(height: 12),
                TextField(
                  controller: descriptionController,
                  decoration: const InputDecoration(
                    labelText: 'Description',
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.all(
                        Radius.circular(10),
                      ),
                    ),
                  ),
                  maxLines: 3,
                ),
                const SizedBox(height: 12),
                Row(
                  children: [
                    const Text('Available: '),
                    const SizedBox(width: 12),
                    DropdownButton<bool>(
                      value: isAvailable,
                      onChanged: (newValue) {
                        setState(() {
                          isAvailable = newValue!;
                        });
                      },
                      items: const [
                        DropdownMenuItem<bool>(
                          value: true,
                          child: Text('Yes'),
                        ),
                        DropdownMenuItem<bool>(
                          value: false,
                          child: Text('No'),
                        ),
                      ],
                    ),
                  ],
                ),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text('Cancel'),
            ),
            TextButton(
              onPressed: () {
                final Room newRoom = Room(
                  roomType: roomType,
                  price: double.tryParse(priceController.text) ?? 0.0,
                  description: descriptionController.text,
                  isAvailable: isAvailable,
                  isBooked: isBooked,
                );

                setState(() {
                  rooms.add(newRoom);
                });

                Navigator.of(context).pop();
              },
              child: const Text('Add'),
            ),
          ],
        );
      },
    );
  }

  Widget _buildSummary() {
    return Card(
      elevation: 3,
      margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 0),
      child: Padding(
        padding: const EdgeInsets.all(12.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Text(
              'Hotel Details:',
              style: TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 17,
              ),
            ),
            const SizedBox(height: 8),
            buildTextFieldSummary('Name', nameController.text),
            buildTextFieldSummary('Description', descriptionController.text),
            buildTextFieldSummary('Contact Info', contactInfoController.text),
            buildTextFieldSummary('City', cityController.text),
            buildTextFieldSummary('Country', countryController.text),
            const Text(
              'Entertainments:',
              style: TextStyle(
                fontWeight: FontWeight.bold,
              ),
            ),
            if (entertainments.isEmpty)
              const Padding(
                padding: EdgeInsets.symmetric(vertical: 5),
                child: Text(
                  'No entertainments added yet',
                  style: TextStyle(fontStyle: FontStyle.italic),
                ),
              ),
            if (entertainments.isNotEmpty)
              Padding(
                padding: const EdgeInsets.symmetric(vertical: 5),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: entertainments
                      .map((entertainment) => Padding(
                            padding: const EdgeInsets.symmetric(vertical: 2),
                            child: Text(
                              entertainment,
                              style: const TextStyle(fontSize: 16),
                            ),
                          ))
                      .toList(),
                ),
              ),
            const Text(
              'Image URLs:',
              style: TextStyle(
                fontWeight: FontWeight.bold,
              ),
            ),
            if (imageUrls.isEmpty)
              const Padding(
                padding: EdgeInsets.symmetric(vertical: 5),
                child: Text(
                  'No image URLs added yet',
                  style: TextStyle(fontStyle: FontStyle.italic),
                ),
              ),
            if (imageUrls.isNotEmpty)
              Padding(
                padding: const EdgeInsets.symmetric(vertical: 5),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: imageUrls
                      .map((imageUrl) => Padding(
                            padding: const EdgeInsets.symmetric(vertical: 2),
                            child: Text(
                              imageUrl,
                              style: const TextStyle(fontSize: 16),
                            ),
                          ))
                      .toList(),
                ),
              ),
            buildTextFieldSummary('Initial Rating', averageRatingController.text),
            const Text(
              'Rooms:',
              style: TextStyle(
                fontWeight: FontWeight.bold,
              ),
            ),
            if (rooms.isEmpty)
              const Padding(
                padding: EdgeInsets.symmetric(vertical: 5),
                child: Text(
                  'No rooms added yet',
                  style: TextStyle(fontStyle: FontStyle.italic),
                ),
              ),
            if (rooms.isNotEmpty)
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: rooms
                    .map((room) => Padding(
                          padding: const EdgeInsets.symmetric(vertical: 2),
                          child: Text(
                            'Room ${rooms.indexOf(room) + 1}: ${room.roomType}, ${room.price} per night, ${room.isAvailable ? 'Available' : 'Not Available'}',
                            style: const TextStyle(fontSize: 16),
                          ),
                        ))
                    .toList(),
              ),
          ],
        ),
      ),
    );
  }

  Widget buildTextFieldSummary(String label, String value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 5),
      child: Row(
        children: [
          Text(
            '$label: ',
            style: const TextStyle(
              fontWeight: FontWeight.bold,
            ),
          ),
          Flexible(
            child: Text(
              value.isEmpty ? 'No value added yet' : value,
              overflow: TextOverflow.ellipsis,
              maxLines: 2,
              style: const TextStyle(fontStyle: FontStyle.italic),
            ),
          ),
        ],
      ),
    );
  }

  Widget buildRoomCard(Room room, int index) {
    return Card(
      elevation: 2,
      margin: const EdgeInsets.symmetric(vertical: 6, horizontal: 0),
      child: ListTile(
        title: Text('Room ${index + 1}'),
        subtitle: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text('Type: ${room.roomType}'),
            Text('Price per Night: \$${room.price.toStringAsFixed(2)}'),
            Text('Available: ${room.isAvailable ? 'Yes' : 'No'}'),
          ],
        ),
        trailing: IconButton(
          icon: const Icon(Icons.delete),
          onPressed: () {
            setState(() {
              rooms.removeAt(index);
            });
          },
        ),
      ),
    );
  }

  Future<void> addRoom(int hotelId, Map<String, dynamic> roomData) async {
    try {
      final headers = await getAuthHeaders();
      final response = await http.post(
        Uri.parse('$roomsUrl/$hotelId'),
        headers: headers,
        body: json.encode(roomData),
      );

      if (response.statusCode == 201) {
        // Room added successfully
        return;
      } else {
        throw Exception('Failed to add room. Error: ${response.body}');
      }
    } catch (error) {
      throw Exception('Error: $error');
    }
  }

  Future<int?> getHotelIdByName(String name) async {
    try {
      final headers = await getAuthHeaders();
      final response = await http.get(
        Uri.parse('$hotelUrl/admin/${widget.adminId}'),
        headers: headers,
      );

      if (response.statusCode == 200) {
        final List<dynamic> hotels = json.decode(response.body);
        final hotel = hotels.firstWhere((hotel) => hotel['name'] == name, orElse: () => null);
        return hotel != null ? hotel['hotelId'] : null;
      } else {
        throw Exception('Failed to fetch hotels. Error: ${response.body}');
      }
    } catch (error) {
      throw Exception('Error: $error');
    }
  }

  Future<void> addHotel() async {
    if (nameController.text.isEmpty ||
        descriptionController.text.isEmpty ||
        cityController.text.isEmpty ||
        countryController.text.isEmpty ||
        contactInfoController.text.isEmpty ||
        averageRatingController.text.isEmpty ||
        rooms.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Please fill in all fields and add at least one room.')),
      );
      return;
    }

    setState(() {
      isLoading = true;
    });

    // Map hotel request body
    final Map<String, dynamic> hotelData = {
      "name": nameController.text,
      "description": descriptionController.text,
      "contactInfo": contactInfoController.text,
      "averageRating": double.tryParse(averageRatingController.text) ?? 0.0,
      "adminId": widget.adminId,
      "entertainments": entertainments,
      "imageUrls": imageUrls,
      "totalNumberOfRooms": rooms.length,
      "numberOfAvailableRooms": rooms.where((room) => room.isAvailable).length,
      "address": {
        "city": cityController.text,
        "country": countryController.text,
      },
    };

    // Map rooms request body
    final List<Map<String, dynamic>> roomsData = rooms.map((room) {
      return {
        "roomType": room.roomType,
        "price": room.price,
        "isAvailable": room.isAvailable,
        "description": room.description,
      };
    }).toList();

    try {
      final headers = await getAuthHeaders();
      final response = await http.post(
        Uri.parse(hotelUrl),
        headers: headers,
        body: json.encode(hotelData),
      );

      if (response.statusCode == 201) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Hotel added successfully.')),
        );

        // Get the hotel ID by fetching the hotel list and finding the newly added hotel by name
        final hotelId = await getHotelIdByName(nameController.text);
        print(hotelId);
        if (hotelId != null) {
          // Add each room
          for (var roomData in roomsData) {
            await addRoom(hotelId, roomData);
          }
        } else {
          throw Exception('Failed to get the hotel ID.');
        }

        Navigator.of(context).pop();
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Failed to add hotel. Error: ${response.body}')),
        );
      }
    } catch (error) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error: $error')),
      );
    } finally {
      setState(() {
        isLoading = false;
      });
    }
  }
}
