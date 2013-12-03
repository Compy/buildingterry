buildingterry
=============

Building Terry mobile application for MIT program project at UGA.


# Introduction
This project is intent to develop a mobile application for the Building Terry campaign: http://building.terry.uga.edu/
Besides offer information about the campaign, the app will provide features to attract donators. One of the main features will be provide the users to interact with a virtual model of the building.

To archive our goal we started evaluating some of the solutions available to incorporate virtual elements of Augmented Reality to our project. The following document aims to instruct about how to reproduce the development environment used  and explore the source code available in this repository. 
All the code generated within this project and part of the tools needed are opensource. The other tools that requires a license can be used free of charge for our purpose.

The goal of this tutorial is not to teach about concepts of Computer Graphics or Virtual Reality, but instruct people how to reproduce the development environment used in the project. You don't need to be advanced in Computer Science to follow this tutorial, however it helps to know some theory. Throughout  this document there will be links that will point you to the theory behind some principies. 

We encorage people to use, try and have fun!


# How to Install
The main platform used in this project is an IDE called Unity3d. It is very popular in game development for providing a set of tools that allows developers to quickly deploy virtual environments. In our case we will use Unity3d to model the virtual building and other objecs that will interact with the user. Besides the fact of Unity3d be one of the best IDE for this type of development, it's cross platform and multiportable; which means that can be developed in different Operational Systems (Windows, Mac, Linux,...) and run in several different devices (Android, iOS, webBrowser, Xbox, Wii...). You can have a better overview about it here: 

Therefore, the first step in preparing the development environment is to download and install Unity3D:
http://unity3d.com/unity/download 

Once that you have it installed is important to understand some of the core concepts of the IDE. The modern 3d Computer Graphic Software relies in three basic elements and it's attributes:

- Object: Position, Rotation, Polygonal Mesh, Texture, Scale, Behavior (scripting)...
- Light: Position, Intensity, Color, Direction,...
- Camera: ViewPoint, Projection, Field of View, Depth,...

A short Introduction to Computer Graphics, focused on 3d render, can be find here: 
http://people.csail.mit.edu/fredo/Depiction/1_Introduction/reviewGraphics.pdf

After you learn the basics of 3D Computer Graphics, it's important to understant how Unity3d handle these elements. This is what makes Unity so useful! One that programmed 3D Graphics using OpenGL, knows how hard can be implemment what we has been disccussed so far. This is a good incentive for the beginners learn how to use them, becouse not long ago, people had to code such abstract concepts. Now, with Unity3d, we have a drag-and-drop interface that make things way easier to develop, but you still need a clear understand about how it manage projects, views, objects,... before we move on.

A good reference to get familiar with Unity is the following video and the official documentation:

http://www.youtube.com/watch?v=g5QFW12utdU

http://unity3d.com/learn/tutorials/modules/beginner/editor
http://docs.unity3d.com/Documentation/Manual/UnityBasics.html


On the top of Unity 3D we will be using an SDK to handle the Augmented Reallity features, called Vuforia. It will basically replace the ordinary Camera Object with a special type of Camera Object that will allows us to project Virtual Objects (the Building) in images from the real world collected by a Real Camera. This is another advanced concept that involves a lot of Math and heavy coding that we have a wonderful piece of software to deal with. Once more, we just need to understand how to use it. 
You do not need to install the Vuforia SDK, because it's libraries are integraded with this repository. The next session will discuss about how to use it, but it's good to check some references to get used to it.

Take a look at the Developers web site and check what we can archive using Vuforia:
https://www.vuforia.com/platform

After you have Unity3d installed and you are familiar with it's interface is time to download the project source files.

You can download the files directly on the address:
https://github.com/Compy/buildingterry/archive/master.zip

Or you can clone/fork the project using git:
https://github.com/Compy/buildingterry.git

If you download the zipped file, extract it. If you have it cloned, just keep track of the folder location.

### Android Users
You will need to download and install the AndroidSDK. If you already have it installed, make sure you have the path to it. Otherwise, download the latest version and have it available to the next steps:
https://developer.android.com/sdk/index.html#download

### iOS Users
If you are running an Mac, you already have the Xcode installed. Just make sure you are running the latest update of it.
If you are a PC user, but would like to run this app in a iOS device (iphone, iPad,...) will need to install Xcode:
https://itunes.apple.com/us/app/xcode/


# How to Use

To open the project files, go to Unity3d, click on File -> Open Project. It will launch the Project Wizard, where you can click on the "Open Other..." buttom and select a folder called "First Example" (../buildingterry-master/Unity/First Example)
You will noticed that Unity will load few Assets and will open the a Scene Called "Vuforia-VirtualButtons". This is the first scenario we have deployed, but this document will be updated once we have more Scenes available.

Now that you are familiar with Unity, you realize that by clicking in "play" you should see the app running. However, if you are running on free license, you will not have access to a physical camera until you had the scene built. If you want to see some action, go ahead and run the app in your target device.

## Building and Run

`note: make sure you have your device plugged on the computer.`

Because our focus will be on mobile devices, this session will focus on how to run the app in the main mobile devices (Android and iOS).

The beginning of this process is very similar: On Unity, you need to click on File-> Building Settings. On the Building Settings Window, you have to make sure the current scene is checked (Vuforia-VirtualButtons.unity in our case) and right Platform is selected (Android or iOS). After had selected the right parameters you can click on "Build and Run". Independent the platform you choose, after hit tell the IDE to Build the app, it will ask you where to save the target files. This step is not really relevant, but would be good you define a pattern for not have files all over the place.

### Android

When the IDE finish to build the new app it will ask you to point the Android SDK folder. There is no much complexybility here and basically everything will be done behind the scenes for you by the SDK. At this point the application should be running in your android device.

### iOS 

If you choose iOS as your target the system will open Xcode and it should finish to load the app in your device. Should be very straigh forward if you are running the latest version, however I had problems liking some libraries. If that happens to you, go to the project folder, building PATH.






# References:

http://en.wikipedia.org/wiki/Computer_graphics_(computer_science)
http://en.wikipedia.org/wiki/Unity_(game_engine)
http://en.wikipedia.org/wiki/Utah_Teapot
