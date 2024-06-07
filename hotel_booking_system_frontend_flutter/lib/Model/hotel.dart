import 'address.dart';

class Hotel {
  final int hotelId;
  final String name;
  final double rating;
  final Address address;
  final int numberOfAvailableRooms;
  final int totalNumberOfRooms;
  final String description;
  final String contactInfo;
  final List<String> imageUrls;
  final List<String> entertainments;

  Hotel({
    required this.hotelId,
    required this.name,
    required this.rating,
    required this.address,
    required this.numberOfAvailableRooms,
    required this.imageUrls,
    required this.description,
    required this.entertainments,
    required this.contactInfo,
    required this.totalNumberOfRooms,
  });

  factory Hotel.fromJson(Map<String, dynamic> json) {
    return Hotel(
      hotelId: json['hotelId'],
      name: json['name'],
      description: json['description'],
      contactInfo: json['contactInfo'],
      rating: json['rating'].toDouble(),
      address: Address.fromJson(json['address']),
      numberOfAvailableRooms: json['numberOfAvailableRooms'],
      totalNumberOfRooms: json['totalNumberOfRooms'],
      imageUrls: List<String>.from(json['imageUrls']),
      entertainments: List<String>.from(json['entertainments']),
    );
  }
}
