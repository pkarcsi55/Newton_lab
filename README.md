# Newton 3 – Force and Accelerometer Lab

Educational C# Windows Forms application for classroom physics experiments.

Newton 3 Lab visualizes force and acceleration data in real time, using low-cost wireless sensors and smartphones.

The main goal of the project is to demonstrate Newton’s third law, force–reaction force pairs, collisions, and acceleration measurements in a simple classroom environment.

---

## Main features

- real-time graph display
- two independent force channels
- Wi-Fi / UDP data reception
- smartphone accelerometer integration
- Bluetooth force sensor support
- measurement reset / clear graph
- live PC IP address display for easier setup
- suitable for classroom demonstrations and student experiments

---

## Typical experiments

- Newton’s third law: force and reaction force
- pulling and pushing experiments
- collision experiments
- acceleration vs force
- impulse measurements
- smartphone motion measurements
- vibration and impact analysis

---

## Screenshot

![Newton 3 Lab screenshot](images/utkzes.png)

---

## Operating modes

Newton 3 Lab supports several measurement modes.

### 1. Wi-Fi force sensor mode

In the current version, the preferred communication method is Wi-Fi using UDP packets.

One or two ESP32-based force sensors can connect to the same Wi-Fi network as the PC application.  
This can be a normal local network or a mobile hotspot created by a laptop or smartphone.

The sensors transmit force data in real time, which is displayed on the graph.

Typical classroom use:

- demonstrating Newton’s third law
- comparing force and reaction force
- pulling and pushing experiments
- collision experiments
- short impulse measurements

### 2. Bluetooth force sensor mode

The software can also communicate with Bluetooth force sensors through virtual COM ports.

This mode is kept for compatibility with earlier versions of the project.

### 3. Smartphone accelerometer mode

Newton 3 Lab can receive acceleration data from a smartphone.

The phone’s built-in accelerometer sends real-time data wirelessly to the PC application.  
The measured acceleration is displayed as a live graph.

Typical use:

- motion experiments
- acceleration measurements
- vibration analysis
- impact measurements
- classroom use of mobile sensors

### Combined mode

The different modes can be used together.

For example, during a cart experiment the force sensor measures interaction forces while the smartphone records acceleration.

This allows students to compare measured force and acceleration directly and helps demonstrate Newton’s laws in practice.

---

## Educational concept

The system is based on low-cost, open hardware and custom-developed software.

One advantage of the project is that old, unused smartphones can also be used as acceleration sensors.  
This reduces cost and electronic waste, while giving students a practical example of sustainable technology use.

Newton 3 Lab is designed not only as a demonstration tool, but also as a STEM project that students can build, modify, and further develop.# Newton_lab
