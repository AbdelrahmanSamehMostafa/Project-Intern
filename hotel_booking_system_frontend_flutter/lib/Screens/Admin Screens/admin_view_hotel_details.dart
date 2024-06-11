import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:hotel_booking_system_frontend_flutter/Constants/urls.dart';
import 'package:hotel_booking_system_frontend_flutter/Functions/token_validation.dart';
import 'package:hotel_booking_system_frontend_flutter/Model/Room.dart';
import 'package:http/http.dart' as http;

class AdminViewHotelDetails extends StatefulWidget {
  final int hotelId;

  const AdminViewHotelDetails({Key? key, required this.hotelId}) : super(key: key);

  @override
  State<AdminViewHotelDetails> createState() => _AdminViewHotelDetailsState();
}

class _AdminViewHotelDetailsState extends State<AdminViewHotelDetails> {
  late TextEditingController nameController;
  late TextEditingController addressController;
  late TextEditingController descriptionController;
  late TextEditingController ratingController;
  List<String> entertainments = [];
  List<String> imageUrls = [];
  List<Room> rooms = []; // List to store added rooms
  bool _isLoading = true;
  bool _isEditing = false; // Track if editing mode is active

  @override
  void initState() {
    super.initState();
    nameController = TextEditingController();
    addressController = TextEditingController();
    descriptionController = TextEditingController();
    ratingController = TextEditingController();
    _fetchHotelDetails();
    _fetchRoomsByHotelId(); // Fetch rooms data when initializing
  }

  @override
  void dispose() {
    // Dispose the controllers when they are no longer needed
    nameController.dispose();
    addressController.dispose();
    descriptionController.dispose();
    ratingController.dispose();
    super.dispose();
  }

  Future<void> _fetchHotelDetails() async {
    setState(() {
      _isLoading = true;
    });
    try {
      final headers = await getAuthHeaders();
      final url = Uri.parse('$hotelUrl/${widget.hotelId}');
      final response = await http.get(url, headers: headers);

      if (response.statusCode == 200) {
        final jsonData = json.decode(response.body);
        setState(() {
          nameController.text = jsonData['name'];
          addressController.text = '${jsonData['address']['city']}, ${jsonData['address']['country']}';
          descriptionController.text = jsonData['description'];
          ratingController.text = jsonData['rating'].toString();
          entertainments = List<String>.from(jsonData['entertainments']);
          imageUrls = List<String>.from(jsonData['imageUrls']);
          _isLoading = false;
        });
      } else {
        setState(() {
          _isLoading = false;
        });
        showDialog(
          context: context,
          builder: (BuildContext context) {
            return AlertDialog(
              title: const Text('Error'),
              content: const Text('Failed to fetch hotel details'),
              actions: <Widget>[
                TextButton(
                  child: const Text('OK'),
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                ),
              ],
            );
          },
        );
      }
    } catch (e) {
      print('Error fetching hotel details: $e');
      setState(() {
        _isLoading = false;
      });
      showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: const Text('Error'),
            content: const Text('Failed to fetch hotel details'),
            actions: <Widget>[
              TextButton(
                child: const Text('OK'),
                onPressed: () {
                  Navigator.of(context).pop();
                },
              ),
            ],
          );
        },
      );
    }
  }

  Future<void> _submitEdits() async {
    final updatedData = {
      'name': nameController.text,
      'address': addressController.text,
      'description': descriptionController.text,
      'rating': double.parse(ratingController.text),
      'entertainments': entertainments,
      'imageUrls': imageUrls,
    };

    final url = Uri.parse('$hotelUrl/${widget.hotelId}');
    final headers = await getAuthHeaders();

    try {
      final response = await http.put(
        url,
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
          ...headers,
        },
        body: jsonEncode(updatedData),
      );

      if (response.statusCode == 204) {
        showDialog(
          context: context,
          builder: (BuildContext context) {
            return AlertDialog(
              title: const Text('Success'),
              content: const Text('Hotel details updated successfully'),
              actions: <Widget>[
                TextButton(
                  child: const Text('OK'),
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                ),
              ],
            );
          },
        );
        setState(() {
          _isEditing = false; // Exit editing mode after successful update
        });
      } else {
        showDialog(
          context: context,
          builder: (BuildContext context) {
            return AlertDialog(
              title: const Text('Error'),
              content: const Text('Failed to update hotel details'),
              actions: <Widget>[
                TextButton(
                  child: const Text('OK'),
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                ),
              ],
            );
          },
        );
      }
    } catch (e) {
      print('Error updating hotel details: $e');
      showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: const Text('Error'),
            content: const Text('Failed to update hotel details'),
            actions: <Widget>[
              TextButton(
                child: const Text('OK'),
                onPressed: () {
                  Navigator.of(context).pop();
                },
              ),
            ],
          );
        },
      );
    }
  }

  Future<void> _fetchRoomsByHotelId() async {
    try {
      final headers = await getAuthHeaders();
      final url = Uri.parse('$roomsUrl/Hotel/${widget.hotelId}');
      final response = await http.get(url, headers: headers);

      if (response.statusCode == 200) {
        final List<dynamic> jsonData = json.decode(response.body);
        List<Room> fetchedRooms = []; // Temporary list to hold fetched rooms

        // Parse each JSON object into a Room object and add to fetchedRooms list
        jsonData.forEach((roomData) {
          Room room = Room(
            roomType: roomData['roomType'],
            description: roomData['description'],
            price: roomData['price'],
            isAvailable: roomData['isAvailable'],
          );
          fetchedRooms.add(room);
        });

        setState(() {
          rooms = fetchedRooms; // Update the state with fetched rooms data
        });
      } else {
        showDialog(
          context: context,
          builder: (BuildContext context) {
            return AlertDialog(
              title: const Text('Error'),
              content: const Text('Failed to fetch rooms data'),
              actions: <Widget>[
                TextButton(
                  child: const Text('OK'),
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                ),
              ],
            );
          },
        );
      }
    } catch (e) {
      print('Error fetching rooms data: $e');
      showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: const Text('Error'),
            content: const Text('Failed to fetch rooms data'),
            actions: <Widget>[
              TextButton(
                child: const Text('OK'),
                onPressed: () {
                  Navigator.of(context).pop();
                },
              ),
            ],
          );
        },
      );
    }
  }

  Widget _buildRoomsList() {
    return ListView.builder(
      shrinkWrap: true,
      itemCount: rooms.length,
      itemBuilder: (context, index) {
        Room room = rooms[index]; // Get the Room object at the current index
        return Card(
          margin: const EdgeInsets.all(8),
          child: ListTile(
            title: Text("Room ${index + 1}"),
            subtitle: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text('Room Type: ${room.roomType}'),
                Text('Description: ${room.description}'),
                Text('Price: \$${room.price}'),
                Text('Availability: ${room.isAvailable}'),
              ],
            ),
          ),
        );
      },
    );
  }

  Widget _buildHotelDetailsText() {
    return Padding(
      padding: const EdgeInsets.all(28.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: <Widget>[
          Row(
            children: [
              const Text(
                'Hotel Name: ',
                style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
              ),
              Text(
                nameController.text,
                style: const TextStyle(fontSize: 20),
              ),
            ],
          ),
          const SizedBox(height: 10),
          Row(
            children: [
              const Text(
                'Address: ',
                style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
              ),
              Text(
                addressController.text,
                style: const TextStyle(fontSize: 20),
              ),
            ],
          ),
          const SizedBox(height: 10),
          Row(
            children: [
              const Text(
                'Description: ',
                style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
              ),
              Text(
                descriptionController.text,
                style: const TextStyle(fontSize: 20),
              ),
            ],
          ),
          const SizedBox(height: 10),
          Row(
            children: [
              const Text(
                'Rating: ',
                style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
              ),
              Text(
                ratingController.text,
                style: const TextStyle(fontSize: 20),
              ),
            ],
          ),
          const SizedBox(height: 10),
          const Text(
            'Entertainments:',
            style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
          ),
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: entertainments.map((entertainment) {
              return Padding(
                padding: const EdgeInsets.symmetric(vertical: 4.0),
                child: Text(
                  entertainment,
                  style: const TextStyle(fontSize: 20),
                ),
              );
            }).toList(),
          ),
          const SizedBox(height: 10),
          const Text(
            'Image URLs:',
            style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
          ),
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: imageUrls.map((imageUrl) {
              return Padding(
                padding: const EdgeInsets.symmetric(vertical: 4.0),
                child: Text(
                  imageUrl,
                  style: const TextStyle(fontSize: 20),
                ),
              );
            }).toList(),
          ),
          const SizedBox(height: 10),
          SizedBox(
            width: 30,
            height: 50,
            child: ElevatedButton(
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              onPressed: () {
                setState(() {
                  _isEditing = true; // Enable editing mode
                });
              },
              child: const Text(
                'Edit Hotel',
                style: TextStyle(fontSize: 20),
              ),
            ),
          ),
          const SizedBox(height: 20),
          const Text(
            'Rooms:',
            style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 10),
          _buildRoomsList(),
          const SizedBox(height: 10),
          SizedBox(
            width: 30,
            height: 50,
            child: ElevatedButton(
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              onPressed: () => _addRoomDialog(context),
              child: const Text(
                'Add Room',
                style: TextStyle(fontSize: 20),
              ),
            ),
          ), // Render rooms as cards
        ],
      ),
    );
  }

  Future<void> _addRoomDialog(BuildContext context) async {
    final TextEditingController priceController = TextEditingController();
    final TextEditingController descriptionController = TextEditingController();
    bool isAvailable = true; // Default value
    String roomType = 'Single'; // Default value

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
                        if (newValue != null) {
                          setState(() {
                            roomType = newValue;
                          });
                        }
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
                // Show confirmation dialog
                showDialog(
                  context: context,
                  builder: (BuildContext context) {
                    return AlertDialog(
                      title: const Text('Confirm Room Addition'),
                      content: const Text('Are you sure you want to add this room to the hotel?'),
                      actions: <Widget>[
                        TextButton(
                          onPressed: () {
                            // Add room and close dialog
                            final Room newRoom = Room(
                              roomType: roomType,
                              price: double.tryParse(priceController.text) ?? 0.0,
                              description: descriptionController.text,
                              isAvailable: isAvailable,
                            );
                            addRoom(widget.hotelId, newRoom);
                            setState(() {
                              rooms.add(newRoom);
                            });

                            Navigator.of(context).pop(); // Close confirm dialog
                            Navigator.of(context).pop(); // Close add room dialog
                          },
                          child: const Text('Yes'),
                        ),
                        TextButton(
                          onPressed: () {
                            Navigator.of(context).pop(); // Close confirm dialog
                          },
                          child: const Text('Cancel'),
                        ),
                      ],
                    );
                  },
                );
              },
              child: const Text('Add'),
            ),
          ],
        );
      },
    );
  }

  Future<void> addRoom(int hotelId, Room roomData) async {
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

  Widget _buildHotelDetailsForm() {
    return SingleChildScrollView(
      padding: const EdgeInsets.all(16.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: <Widget>[
          TextFormField(
            controller: nameController,
            decoration: const InputDecoration(
              labelText: 'Hotel Name',
              border: OutlineInputBorder(
                borderRadius: BorderRadius.all(
                  Radius.circular(10),
                ),
              ),
            ),
          ),
          const SizedBox(height: 15),
          TextFormField(
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
          const SizedBox(height: 15),
          TextFormField(
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
          const SizedBox(height: 15),
          TextFormField(
            controller: ratingController,
            decoration: const InputDecoration(
              labelText: 'Rating',
              border: OutlineInputBorder(
                borderRadius: BorderRadius.all(
                  Radius.circular(10),
                ),
              ),
            ),
            keyboardType: TextInputType.number,
          ),
          const SizedBox(height: 20),
          const Text(
            'Entertainments:',
            style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 15),
          Column(
            children: entertainments.asMap().entries.map((entry) {
              int index = entry.key;
              String entertainment = entry.value;
              return Column(
                children: [
                  Row(
                    children: [
                      Expanded(
                        child: TextFormField(
                          initialValue: entertainment,
                          decoration: InputDecoration(
                            labelText: 'Entertainment $index',
                            border: const OutlineInputBorder(
                              borderRadius: BorderRadius.all(
                                Radius.circular(10),
                              ),
                            ),
                          ),
                          onChanged: (value) {
                            setState(() {
                              entertainments[index] = value;
                            });
                          },
                        ),
                      ),
                      IconButton(
                        icon: const Icon(
                          Icons.delete,
                          color: Colors.red,
                        ),
                        onPressed: () {
                          setState(() {
                            entertainments.removeAt(index);
                          });
                        },
                      ),
                    ],
                  ),
                  const SizedBox(height: 15),
                ],
              );
            }).toList(),
          ),
          const SizedBox(height: 10),
          const Text(
            'Image URLs:',
            style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 15),
          Column(
            children: imageUrls.asMap().entries.map((entry) {
              int index = entry.key;
              String imageUrl = entry.value;
              return Column(
                children: [
                  Row(
                    children: [
                      Expanded(
                        child: TextFormField(
                          initialValue: imageUrl,
                          decoration: InputDecoration(
                            labelText: 'Image URL $index',
                            border: const OutlineInputBorder(
                              borderRadius: BorderRadius.all(
                                Radius.circular(10),
                              ),
                            ),
                          ),
                          onChanged: (value) {
                            setState(() {
                              imageUrls[index] = value;
                            });
                          },
                        ),
                      ),
                      IconButton(
                        icon: const Icon(
                          Icons.delete,
                          color: Colors.red,
                        ),
                        onPressed: () {
                          setState(() {
                            imageUrls.removeAt(index);
                          });
                        },
                      ),
                    ],
                  ),
                  const SizedBox(height: 15),
                ],
              );
            }).toList(),
          ),
          const SizedBox(height: 20),
          ElevatedButton(
            style: ElevatedButton.styleFrom(
              backgroundColor: const Color.fromARGB(255, 67, 84, 236),
              foregroundColor: Colors.white,
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(10),
              ),
            ),
            onPressed: _submitEdits,
            child: const Text('Submit Edit'),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Hotel Details'),
        centerTitle: true,
      ),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _isEditing
              ? SingleChildScrollView(child: _buildHotelDetailsForm())
              : SingleChildScrollView(child: _buildHotelDetailsText()),
    );
  }
}
