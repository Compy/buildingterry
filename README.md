buildingterry
=============

Building Terry mobile application for MIT program project at UGA.


### Introduction
This project is intent to develop a mobile application for the Building Terry campaign: http://building.terry.uga.edu/
Besides offer information about the campaign, the app will provide features to attract donators. One of the main features will be provide the users to interact with a virtual model of the building.

To archive our goal we started evaluating some of the solutions available to incorporate virtual elements of Augmented Reality to our project. The following document aims to instruct about how to reproduce the development environment used  and explore the source code available in this repository. 
All the code generated within this project and part of the tools needed are opensource. The other tools that requires a license can be used free of charge for our purpose.

The goal of this tutorial is not to teach about concepts of Computer Graphics or Virtual Reality, but instruct people how to reproduce the development environment used in the project. You don't need to be advanced in Computer Science to follow this tutorial, however it helps to know some theory. Throughout  this document there will be links that will point you to the theory behind some principies. 

We encorage people to use, try and have fun!


### How to Install
The main platform used in this project is an IDE called Unity3d. It is very popular in game development for providing a set of tools that allows developers to quickly deploy virtual environments. In our case we will use Unity3d to model the virtual building and other objecs that will interact with the user. Besides the fact of Unity3d be one of the best IDE for this type of development, it's cross platform and multiportable; which means that can be developed in different Operational Systems (Windows, Mac, Linux,...) and run in several different devices (Android, iOS, webBrowser, Xbox, Wii...). You can have a better overview about it here: 

Therefore, the first step in preparing the development environment is to download and install Unity3D:
http://unity3d.com/unity/download 

Once that you have it installed is important to understand some of the core concepts of the IDE. The modern 3d Computer Graphic Software relies in three basic elements and it's attributes:

- Object: Position, Rotation, Polygonal Mesh, Texture, Scale, Behavior (scripting)...
- Light: Position, Intensity, Color, Direction,...
- Camera: ViewPoint, Projection, Field of View, Depth,...

A short Introduction to Computer Graphics, focused on 3d render, can be find here: 
http://people.csail.mit.edu/fredo/Depiction/1_Introduction/reviewGraphics.pdf

Once that you get used to the basics of 3D Computer Graphics, it's important to understant how unity handle these elements. This is what makes Unity be so useful! One that programmed 3D Graphics using OpenGL, knows how hard can be implemment what we has been disccussed so far. This is a good incentive for the beginners learn how to use them, becouse not long ago, people had to code such abstract concepts. Instead this painfull process, Unity provides a drag-and-drop interface that make things way easier to develop, but you still need a clear understand about how it manage projects, views, objects,... before we move on.

A good reference to get familiar with Unity is the following video and the official documentation:

http://www.youtube.com/watch?v=g5QFW12utdU

http://unity3d.com/learn/tutorials/modules/beginner/editor
http://docs.unity3d.com/Documentation/Manual/UnityBasics.html


On the top of Unity 3D we will be using an SDK to handle the Augmented Reallity features, called Vuforia. It will basically replace the ordinary Camera Object with a special type of Camera Object that will allows us to project Virtual Objects (the Building) in images from the real world collected by a Real Camera. This is another advanced concept that involves a lot of Math and heavy coding that we have a wonderful piece of software to deal with. Once more, we just need to understand how to use it. 
You do not need to install the Vuforia SDK, because it's libraries are integraded with this repository. The next session will discuss about how to use it, but it's good to check some references to get used to it.

Take a look at the Developers web site and check what we can archive using Vuforia:
https://www.vuforia.com/platform

### How to Use






### References:

http://en.wikipedia.org/wiki/Computer_graphics_(computer_science)
http://en.wikipedia.org/wiki/Unity_(game_engine)
http://en.wikipedia.org/wiki/Utah_Teapot
