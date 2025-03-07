The Silent Awakening: A VR Empathy Project for the Deaf Community
The Silent Awakening is an immersive VR experience designed to foster empathy for the deaf community. Through innovative storytelling and interactive scenes, this project provides users with a deep understanding of the challenges faced by individuals who are deaf or hard of hearing. The project includes elements such as hospital scenarios, deafness simulation, sign language tutorials, and interactions with NPCs, alongside future plans for real-time gesture recognition for sign language learning.

Table of Contents
Introduction
Features
Project Setup
Scene Descriptions
Technologies Used
Installation
Usage
Future Plans
Contributing
License
Contact
Introduction
The Silent Awakening aims to bridge the communication gap between the hearing and deaf communities by offering an interactive and empathetic VR experience. Users will navigate various scenarios, each highlighting the impact of deafness on everyday life. From hospital environments to sign language tutorials, this VR experience will challenge users to see the world through the eyes of someone who is deaf, encouraging greater empathy and understanding.

Features
Deafness Simulation: Experience the world from the perspective of someone who is deaf, with auditory cues and environmental sounds replaced with visual representations.
Hospital Scenes: Interactive pre- and post-hospital scenarios showcasing the challenges faced by the deaf in medical settings.
NPC Interactions: Engage with NPCs (non-playable characters) using sign language and assistive communication methods.
Sign Language Tutorial: Learn the basics of sign language through interactive lessons within the VR environment.
Hearing Aid Device: A functional, virtual hearing aid that simulates the auditory experience of hearing-impaired individuals.
Deafness Support Dog: A supportive companion to guide users through their journey and offer assistance in key scenarios.
Real-Time Gesture Recognition (Future): Plans for integrating real-time gesture recognition to enable users to learn sign language interactively.
Project Setup
To run this project on your local machine, follow the setup instructions below.

Requirements
Unity 2023.1 or later
XR Hands 1.5 (for hand tracking integration)
FinalIK (for embodied character interaction)
Oculus or Meta Quest device for immersive experience
Compatible VR setup (e.g., Oculus Rift, HTC Vive, or other XR-enabled devices)
Setup Instructions
Clone the repository:

bash
Copy
Edit
git clone https://github.com/yourusername/The-Silent-Awakening.git
Open the project in Unity:

Open Unity Hub and click "Add" to add the project folder.
Open the project.
Install dependencies:

Import necessary Unity packages like XR Toolkit, FinalIK, and XR Hands 1.5 through the Unity Package Manager.
Set up the XR environment:

Go to Edit > Project Settings > XR Plug-in Management and ensure that the XR settings are properly configured for your target VR device.
Build and deploy:

Select your target platform (Oculus/Meta Quest, etc.) and build the project.
Scene Descriptions
1. Introduction Scene
The user enters a dark, immersive environment where they begin to experience the world without hearing. Sound is replaced with visual indicators (vibrations, lighting, etc.) to simulate deafness.
2. Hospital Scene
Pre-Scenario: The user finds themselves in a hospital waiting room. Challenges include reading lips and understanding medical staff without hearing.
Post-Scenario: The user experiences a medical procedure where communication is key, highlighting the barriers to effective healthcare for the deaf.
3. Sign Language Tutorial
A guided interactive tutorial teaches basic sign language, offering real-time feedback to help users improve their skills.
4. Hearing Aid Simulation
The user can interact with a virtual hearing aid device to simulate how sound might be experienced by someone with hearing loss.
5. Support Dog Interaction
A virtual support dog accompanies the user, offering emotional and practical support in the environment.
Technologies Used
Unity 2023.1: The primary development environment used to build the VR experience.
XR Hands 1.5: Hand tracking integration for realistic user interaction.
FinalIK: Full-body inverse kinematics to provide natural character movement and interaction.
Oculus Integration: For seamless interaction with Meta Quest and other Oculus-based VR devices.
C#: Used for scripting gameplay logic, interactions, and NPC behaviors.
Blender: For 3D model creation and animation.
Adobe Photoshop & Illustrator: For UI/UX design and assets.
SteamVR: For cross-platform VR device compatibility.
Installation
Clone the repository:

bash
Copy
Edit
git clone https://github.com/yourusername/The-Silent-Awakening.git
Open the project in Unity (Unity 2023.1 or later required).

Ensure all dependencies (XR Toolkit, XR Hands, FinalIK, etc.) are installed via Unity Package Manager.

Set up your VR environment and device in Unity (e.g., Oculus Quest, HTC Vive, or other supported VR hardware).

Build the project for your target VR device:

Go to File > Build Settings, select your platform, and click Build and Run.
Usage
Once the project is set up and running, use the following controls to interact with the environment:

Hand Tracking: Use your hands to interact with objects and NPCs.
Button Press: Use the VR controller's buttons for menu navigation and basic interactions.
Sign Language Tutorial: Follow the on-screen prompts to learn sign language gestures.
Future Plans
Real-Time Gesture Recognition: Develop a system for real-time sign language recognition to allow users to practice and communicate using their own gestures.
Additional Scenarios: Expand the project with more scenarios showcasing various aspects of life for the deaf community.
Multiplayer Support: Enable collaboration with other users to learn sign language and experience scenarios together.
Contributing
We welcome contributions to The Silent Awakening. To contribute:

Fork the repository.
Create a new branch (git checkout -b feature-name).
Commit your changes (git commit -am 'Add new feature').
Push to the branch (git push origin feature-name).
Open a pull request to merge your changes.
License
This project is licensed under the MIT License - see the LICENSE file for details.
