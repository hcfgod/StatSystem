# Stat System

##Overview

Stat System is a robust and flexible Unity-based system designed to manage various statistics for game elements, including characters, weapons, vehicles, and more. Adhering to solid coding principles, the system is easily extendable and customizable to fit any game's needs.

----------------------------------------------------------------------------------------------------

##Features

-Core Features

-Basic Stats: Manage fundamental stats like health, mana, and speed.
-Dynamic Stat Caps: Set minimum and maximum values for stats dynamically.
-Event Handling: Granular events for stat changes, modifier additions/removals, etc.
-Stat Decay and Regeneration: Built-in methods for stat depletion and regeneration over time.

-Advanced Features

-Stat Modifiers: Apply temporary buffs and debuffs.
-Stat Conditions: Trigger events based on stat conditions (e.g., low health).
-Stat Formulas: Use custom formulas to calculate stat values.
-Stat Interactions: Define complex interactions between different stats.

---------------------------------------------------------------------------------------------------

##Installation

1. Clone Or Download the repository: git clone https://github.com/hcfgod/StatSystem.git
2. Drag the cloned folder into your project
3. Navigate to the StatSystem folder and explore the examples.

---------------------------------------------------------------------------------------------------

##Usage

Creating a Stat: Create a new StatData ScriptableObject and set its initial and maximum values.
Managing Stats: Use the StatManager class to add, remove, or retrieve stats.
Applying Modifiers: Use the AddModifier and RemoveModifier methods to apply temporary buffs or debuffs.

---------------------------------------------------------------------------------------------------

##Examples

Check the Examples folder for sample implementations of various features.

---------------------------------------------------------------------------------------------------

##Contributing

Feel free to fork the project, create a feature branch, and open a pull request.

---------------------------------------------------------------------------------------------------

##License

This project is licensed under the MIT License.

---------------------------------------------------------------------------------------------------
