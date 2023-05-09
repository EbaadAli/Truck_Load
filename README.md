# Truck_Load

Welcome to the Truck Load application. Below you will find the requirements for this task. To run the application download this repo as a zip and run the file
"truck_load.application"  and it should begin the installation process. 

Enjoy!


REQUIREMENTS:

The application will be called Load Truck.
It will:
Create a new truck or load an existing Truck
Allow selecting one or more Box items to go on the truck from those Box items in inventory
 
A Truck item has the following attributes:
TruckID (32 numeric)
Name (20 alphanumeric)
Number (6 numeric)
Status (char 1; "O" Open, "C" Closed)
 
A Box item has the following attributes:
BoxID (32 numeric)
PLU (15 alphanumeric)
Number (6 numeric)
Weight (4.2 decimal) UOM is assumed kg
Status (char 1; "I" Inventory, "S" Shipped on Truck)
TruckID if assigned to a Truck
 
The user will load the app
The top section of the app will allow:
Select an existing Truck from a list or Create a new Truck
New Trucks will have Status = "O" with blank Name and Number
The TruckID will be set to a unique value upon creation and will not be editable.
Once the user enters a Name and Number they may save the new truck
If it is status O they can CLOSE the truck after they have assigned at least one Box
If it status C they may REOPEN the Truck, changing the Status to O
If they load an existing Truck they can change any of the attributes except TruckID and save the record
To load they will select the Truck from a list of existing trucks.  The list will show Truck Name and Number only
 
The bottom section of the application will allow
Seeing all boxes currently in Inventory (status "I"),
Seeing all boxes currently on the selected Truck from the top section
Both lists of Boxes will show all attributes for each Box
Assigning a Box from Inventory (Status I) to a Truck of status "O" (changing Box status to S)
Assigning a Box from the selected Truck (if the truck is status "O") to Inventory (changing Box status to I)
 
You will need to start with inventory
Please create 5-10 Box records for use by the application
Adding, removing, or editing Box records is beyond the scope of this application.
