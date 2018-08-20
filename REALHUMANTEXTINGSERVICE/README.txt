Welcome to the REALHUMANTEXTINGSERVICE program this is definitely not a bot, friend! 

Simply starting this program in Visual Studio should allow you to run it, however the file 
paths located in the repositories are set to my local paths and will have to be updated in 
order to run it's read/write functions properly. 

This program is designed with expansion in mind and allows for implementation of new 
repositories through the use of interfaces, and a simple change to the config file will 
switch the repository utilized. Errors are handled through Responses built by the manager 
and relayed back to the user. 

I chose to build the program in 4 layers to keep the different functions as separate as 
possible. The UI layer is responsible for UI and workflow. The BLL layer works to take 
data from the Data layer and build responses to return to the UI. The Data layer interacts 
directly with the files, and the Models layer contains only the data types to be used by 
the program. 

I chose C# because it is my strongest language and wanted to focus on presenting my 
best work in the time given. 

With more time I would have liked to implement a full in-memory test repository to run 
NUnit tests against as I was building the program. I would have also liked to include 
full CRUD options for Guests, Companies, and Messages. 

Enjoy!