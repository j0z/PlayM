 .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
| |   ______     | || |   _____      | || |      __      | || |  ____  ____  | || | ____    ____ | |
| |  |_   __ \   | || |  |_   _|     | || |     /  \     | || | |_  _||_  _| | || ||_   \  /   _|| |
| |    | |__) |  | || |    | |       | || |    / /\ \    | || |   \ \  / /   | || |  |   \/   |  | |
| |    |  ___/   | || |    | |   _   | || |   / ____ \   | || |    \ \/ /    | || |  | |\  /| |  | |
| |   _| |_      | || |   _| |__/ |  | || | _/ /    \ \_ | || |    _|  |_    | || | _| |_\/_| |_ | |
| |  |_____|     | || |  |________|  | || ||____|  |____|| || |   |______|   | || ||_____||_____|| |
| |              | || |              | || |              | || |              | || |              | |
| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
 '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 

 ----------------------------------------------by---------------------------------------------------
 ----------------------------------------------j0z--------------------------------------------------

 What is it?
	 PlayM is a program that allows quick, easy, customizable remote control of PCs from a Windows tablet. 

 What is it NOT?
	 A remote desktop program. There is no video streaming support, and it is designed for local use only.

 Why would I want to use this?
	 It was originally designed to allow the control of games and other software on a HTPC from the comfort of your couch. 
	 It could also prove useful in allowing you to use a tablet as a drawing/graphics tablet. NOTE: right now, pressure 
	 sensitivity (for example, from the Wacom digitizer on the Surface Pro) is not supported, although that is something 
	 I'm looking into.

 ------------------------------------------------------------------------------------------------------

 Right now, this is more of a proof-of-concept than anything else. Basic functionality is missing, and the GUI on both the 
 server and client are something I threw together just to see if it would work. 

 To-Do/Planned features:
 Proper mouse support. 
 A customizable button layout, probably in the form of several prebuilt arrangements for specific uses (FPS games, media playback, etc)
 Proper server GUI
 Win8 "Metro" support
 Support for modifier keys
 Pressure sensitivity support

 Current Issues:
 * The trackpad included in the Legacy controller produces bad jitters in the camera of FPS games,
   rendering them all but unplayable. However, it works for normal mouse usage. 
 * Buttons on the Legacy controller can "stick" if your finger leaves the button while it is depressed
 * The server GUI is completely worthless. 
 * There is no protection against connection of multiple clients (or proper handling at all).
 * The server cannot handle mouse button presses. 
 * Client crashes if disconnected from the server.

 -------------------------------------------------------------------------------------------------------

 The following is an explaination for how this thing works, and hopefully will allow developers to easily communicate with the server.
 My hope is that clients can be built for all major platforms, allowing you to control your PC from any device. 

 Developers:
 The PlayM solution contains 2 projects: PlayM_GUI and PlayM_Controller_Legacy. PlayM_GUI is the (poorly-named) server project, the other is an
 example controller, made using WPF. My ultimate goal was to have the main client be a metro app, with this program being a fallback (hence, 'legacy').

 -----PlayM_GUI------
 The actual interface portion of the server doesn't do anything at the moment, it is merely a placeholder. When you start it up it listens on port 3000 for clients.
 There is currently no protection from multiple client connections, this needs to be fixed.  
 When a client connects, it is spun off onto a seperate thread, and the server begins listening for commands. 
 See the "PLAYM PROTOCOL" section for an explaination of the protocol used.
 The server parses the commands in the InputSynth class, and then uses the InputManager library (see link at the bottom of this document) to actually send the commands 
 to the system. InputManager works at a very low level, and so the keys and mouse will be registered system-wide (including in DirectX games). 

 -----PlayM_Controller_Legacy------
 This is a simple proof-of-concept client controller, and so is of limited usefulness. Button presses are sent to Client.send(), where they are formatted properly
 and then sent to the server. Be sure to change the IP address in PlayM_Controller_Legacy.Client if you're going to use it.
 
 ----------PlayM Protocol------------
 PlayM uses a very simple protocol to communicate between the client and server. Currently, there is no registering of clients, and the server has no idea which client
 is which if multiple clients are connected.
 
 A message is composed of three pieces of data:
	[type];[message];[hold]
 Messages are terminated by a newline.
 [type] is either 'KB' or 'M'
	'KB' is sent for a keyboard button press.
	'M' is sent for either mouse movement or a mouse button press.
 [message] specifies which key was pressed (as a single, capital character) or as a comma-deliminated tuple of type <str,str> in the case of a mouse, giving its x and y coordinates.  
 [hold] is only used for keyboard messages, and can only have the following values:
	* 0 = (default) the button press was a click.
	* 1 = the user has pushed the button down, and has not let up.
	* 2 = the user has released the button.

Examples:

The user clicked the key "A"
KB;"A";0 \n

The user pressed "A"
KB;"A";1 \n

The user released "A"
KB;"A";2 \n

The user moved their thumb to position 345,230
M;345,230;0 \n 

Yes, the trailing '0' is still needed for mouse movement.


You'll need to include the following to be able to compile PlayM:

3rd Party Libraries: 
 http://www.codeproject.com/Articles/117657/InputManager-library-Track-user-input-and-simulate 

 .NET 3.5 (Server)
 .NET 4.5 (Legacy Controller)

 Developed using VS2013