# Crayon Physics 2D — Unity 6 Starter

Original physics-drawing puzzle prototype, inspired by draw-to-solve mechanics. No third-party game assets included.

## Run
1. Create a Unity 6 **2D Core** project.
2. Copy `Assets/Scripts` from this folder to your project's `Assets/Scripts`.
3. In Player settings, set Active Input Handling to **Both** or **Input Manager (Old)**.
4. Create five scenes named Level01 through Level05; add each to Build Profiles.
5. Add an empty GameObject with `CrayonSceneBootstrap` to each scene and set its level to 1–5.
6. Press Play and draw with the left mouse button.

## Status
**Starter scaffold only: not tested or built in Unity Editor.**
Includes runtime generated primitive art and level variations, drawing strokes with 2D physics, a rolling ball, goal detection, and optional level controller. The goal currently logs success; progression, HUD, balancing, bespoke scene assets, audio, and mobile input remain to be implemented.
