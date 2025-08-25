# Car Simulator 🚗

A simple C# console application that simulates a driver and a car.  
The program tracks **fuel**, **driver fatigue**, and **car direction**,  
and allows the user to control the car with text commands.

---

## Features
- 📋 Show all available commands before each input
- ⛽ Refuel car (always fills to 100%)
- 🔄 Move forward, reverse, or turn (North, South, East, West)
- 😴 Rest to reduce driver fatigue
- ⚠️ Fatigue warnings when exceeding threshold
- ❌ Prevents driving if fuel is empty
- 🏁 Exit option to quit the simulator

---

## Commands
- `forward` → Move forward
- `reverse` → Move backward
- `turn left` / `turn right` → Change direction
- `refuel` → Fill fuel tank to 100%
- `rest` → Reduce driver fatigue
- `status` → Show current driver & car status
- `exit` → Quit the simulator

---


## How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/CarSimulator.git
   cd CarSimulator
