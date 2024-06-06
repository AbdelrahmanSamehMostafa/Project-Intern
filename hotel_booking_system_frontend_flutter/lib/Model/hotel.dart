class Hotel {
  final String name;
  final double rating;
  //final String address;
  final int numberOfAvailableRooms;

  Hotel({
    required this.name,
    required this.rating,
   // required this.address,
    required this.numberOfAvailableRooms,
  });

  factory Hotel.fromJson(Map<String, dynamic> json) {
    return Hotel(
      name: json['name'],
      rating: json['rating'].toDouble(),
     // address: json['address'],
      numberOfAvailableRooms: json['numberOfAvailableRooms'],
    );
  }
}
