import 'package:flutter/material.dart';

class CustomerPickDate extends StatefulWidget {
  final DateTime dateIn;
  final DateTime dateOut;
  final Function(DateTime) onDateInChanged;
  final Function(DateTime) onDateOutChanged;

  const CustomerPickDate({
    Key? key,
    required this.dateIn,
    required this.dateOut,
    required this.onDateInChanged,
    required this.onDateOutChanged,
  }) : super(key: key);

  @override
  State<CustomerPickDate> createState() => _CustomerPickDateState();
}

class _CustomerPickDateState extends State<CustomerPickDate> {
  late DateTime selectedCheckinDate;
  late DateTime selectedCheckOutDate;

  @override
  void initState() {
    super.initState();
    selectedCheckinDate = widget.dateIn;
    selectedCheckOutDate = widget.dateOut;
  }

  Future<void> selectCheckinDate(BuildContext context, DateTime selectedDate) async {
    final DateTime? picked = await showDatePicker(
      context: context,
      initialDate: selectedDate,
      firstDate: DateTime(2000),
      lastDate: DateTime(2100),
    );
    if (picked != null && picked != selectedDate) {
      setState(() {
        selectedCheckinDate = picked; // Update check-in date
        widget.onDateInChanged(selectedCheckinDate); // Notify parent widget
      });
    }
  }

  Future<void> selectCheckoutDate(BuildContext context, DateTime selectedDate) async {
    final DateTime? picked = await showDatePicker(
      context: context,
      initialDate: selectedDate,
      firstDate: DateTime(2000),
      lastDate: DateTime(2100),
    );
    if (picked != null && picked != selectedDate) {
      setState(() {
        selectedCheckOutDate = picked; // Update checkout date
        widget.onDateOutChanged(selectedCheckOutDate); // Notify parent widget
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Row(
          children: [
            const Text("Check in Date: "),
            const SizedBox(width: 5),
            ElevatedButton(
              onPressed: () => selectCheckinDate(context, selectedCheckinDate),
              child: Text('${selectedCheckinDate.day}/${selectedCheckinDate.month}/${selectedCheckinDate.year}'),
            ),
          ],
        ),
        const SizedBox(height: 10),
        Row(
          children: [
            const Text("Check Out Date: "),
            const SizedBox(width: 5),
            ElevatedButton(
              onPressed: () => selectCheckoutDate(context, selectedCheckOutDate),
              child: Text('${selectedCheckOutDate.day}/${selectedCheckOutDate.month}/${selectedCheckOutDate.year}'),
            ),
          ],
        ),
        const SizedBox(height: 10),
      ],
    );
  }
}
