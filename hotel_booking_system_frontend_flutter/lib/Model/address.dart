class Address {
  final int addressId;
  final String city;
  final String country;

  Address({
    required this.addressId,
    required this.city,
    required this.country,
  });

  factory Address.fromJson(Map<String, dynamic> json) {
    return Address(
      addressId: json['addressId'],
      city: json['city'],
      country: json['country'],
    );
  }
}
