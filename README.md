# Morph

Morph is a device agnostic framework to develop applications on Unity without caring about the device it will run on. The framework is focused on AR and VR applications.

The development has only begun so there are many missing features, but I can list what I plan to do with this framework:

- Interactions (Focus, Selection, Grab, Throw, ...)
- Controllers abstraction
- Steam VR-like input system
- Display abstraction (Monoscopic, Stereoscopic, Opaque, See-Through, ...)
- Spatial scanning (Room scanning, ...)
- Other things that I didn't think of

## Devices

### Current supported devices

- Desktop (non VR)
- Mobile (non VR)
- Google VR
- Oculus Go

### Planned devices support

- Oculus Rift
- Daydream
- Steam VR
- Universal Windows Platform (Hololens, Mixed Reality Headsets, Desktop)
- WebVR

## Controllers

To write agnostic applications, you need to have access to controllers in an agnostic way. Morph allows you to do that by accessing controllers through a common interface between all of them.

This interface describes what is available for each controller, as a list of features.

### Features

- None : _no features available_
- Position: _position in world space_
- Rotation: _rotation in world space_
- Touchpad: _touchpad data (horizontal and vertical values, ...)_
- Buttons: _buttons (mouse buttons, game controller buttons, ...)_
- Gestures: _swipe, shake, ..._
- Haptics: _controller vibration etc..._
- 3DOF: _the controller has 3 degrees of freedom_
- 6DOF: _the controller has 6 degrees of freedom_