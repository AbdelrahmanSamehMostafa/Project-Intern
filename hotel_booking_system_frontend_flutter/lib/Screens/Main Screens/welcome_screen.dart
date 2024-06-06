import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Admin%20Screens/admin_register.dart';
import 'package:hotel_booking_system_frontend_flutter/Screens/Customer%20Screens/customer_register.dart';

import 'main_login.dart';

class WelcomeScreen extends StatefulWidget {
  const WelcomeScreen({super.key});

  @override
  State<WelcomeScreen> createState() => _WelcomeScreenState();
}

class _WelcomeScreenState extends State<WelcomeScreen> {
  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: MyHomePage(),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key});
  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  // Boolean variables to track the state of each icon
  bool _is1stIconChanged = false;
  bool _is2ndIconChanged = false;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: SingleChildScrollView(
          child: Center(
            child: Column(
              //crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const SizedBox(height: 250),
                const Padding(
                  padding: EdgeInsets.symmetric(horizontal: 16.0),
                  child: Text(
                    "WELCOME TO OUR BOOKING SYSTEM",
                    style: TextStyle(
                      color: Colors.black,
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                const SizedBox(height: 20),
                const Padding(
                  padding: EdgeInsets.symmetric(horizontal: 16.0),
                  child: Text(
                    "Join As Customer Or Admin",
                    style: TextStyle(color: Colors.black, fontSize: 20, fontWeight: FontWeight.bold),
                  ),
                ),
                const SizedBox(height: 20),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  child: Container(
                    width: 450,
                    height: 135,
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10),
                      border: Border.all(
                        color: Colors.black,
                        width: 1,
                      ),
                    ),
                    child: Stack(
                      children: [
                        const Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Padding(
                              padding: EdgeInsets.all(8),
                              child: Icon(Icons.person),
                            ),
                            Padding(
                              padding: EdgeInsets.all(8),
                              child: Text(
                                "I'm a Customer, looking for hotels to book what I want",
                                style: TextStyle(color: Colors.black, fontSize: 18),
                              ),
                            ),
                          ],
                        ),
                        Align(
                          alignment: Alignment.topRight,
                          child: IconButton(
                            icon: _is1stIconChanged ? const Icon(Icons.circle) : const Icon(Icons.circle_outlined),
                            onPressed: () {
                              setState(() {
                                _is1stIconChanged = true;
                                _is2ndIconChanged = false;
                              });
                            },
                            color: Colors.black,
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
                const SizedBox(height: 15),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  child: Container(
                    width: 450,
                    height: 135,
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10),
                      border: Border.all(
                        color: Colors.black,
                        width: 1,
                      ),
                    ),
                    child: Stack(
                      children: [
                        const Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Padding(
                              padding: EdgeInsets.all(8),
                              child: Icon(Icons.home_work_outlined),
                            ),
                            Padding(
                              padding: EdgeInsets.all(8),
                              child: Text(
                                "I'm an Admin, I have many hotels and I want to show it in the app",
                                style: TextStyle(color: Colors.black, fontSize: 18),
                              ),
                            ),
                          ],
                        ),
                        Align(
                          alignment: Alignment.topRight,
                          child: IconButton(
                            icon: _is2ndIconChanged ? const Icon(Icons.circle) : const Icon(Icons.circle_outlined),
                            onPressed: () {
                              setState(() {
                                _is2ndIconChanged = true;
                                _is1stIconChanged = false;
                              });
                            },
                            color: Colors.black,
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
                const SizedBox(height: 23),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  child: SizedBox(
                    width: 450,
                    height: 65,
                    child: ElevatedButton(
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color.fromARGB(255, 67, 84, 236),
                        foregroundColor: Colors.white,
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      onPressed: () {
                        // Navigate where to go when pressed user or service provider
                        if (_is1stIconChanged) {
                          Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerRegister()));
                        } else if (_is2ndIconChanged) {
                          Navigator.push(context, MaterialPageRoute(builder: (context) => const AdminRegister()));
                        }
                      },
                      child: const Text(
                        "Create Account",
                        style: TextStyle(fontSize: 20),
                      ),
                    ),
                  ),
                ),
                const SizedBox(height: 23),
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const Text(
                      'Already have an account?',
                      style: TextStyle(fontSize: 16),
                    ),
                    const SizedBox(width: 6),
                    GestureDetector(
                      onTap: () {
                        Navigator.push(context, MaterialPageRoute(builder: (context) => const CustomerAndAdminLogin()));
                      },
                      child: const Text(
                        'Login',
                        style: TextStyle(color: Color.fromARGB(255, 67, 84, 236), fontWeight: FontWeight.bold),
                      ),
                    ),
                  ],
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
