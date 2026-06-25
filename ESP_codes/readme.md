# ESP32 Force Sensor Firmware

This folder contains the ESP32 firmware used by the Newton 3 Lab project.

## Files

### hx711_force_sender_UDP_WIFI.ino

Current recommended firmware.

Features:

- ESP32 + HX711 load cell interface
- Wi-Fi connection
- UDP data transmission to the PC
- configurable device ID (A or B)
- configurable Wi-Fi credentials
- real-time force measurement

Used with the latest Newton 3 Lab software.

---

### hx711_force_sender_bluetooth.ino

Legacy Bluetooth version.

Features:

- ESP32 + HX711 load cell interface
- Bluetooth Serial communication
- virtual COM port on Windows
- real-time force measurement

Maintained for compatibility with earlier versions of the project.

---

## Hardware

- ESP32 (Lolin32 Lite or compatible)
- HX711 load cell amplifier
- Load cell

---

## Recommendation

For new installations, the **Wi-Fi / UDP** version is recommended.

It offers simpler classroom setup, lower latency, and allows multiple sensors and smartphone accelerometers to operate simultaneously on the same local network.
