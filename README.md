# Velib Gateway Web Service

An Intermediate Web Server that exposes SOAP API to access to the Velib Web Service (JC Decault : http://www.jcdecaux.com/ )
Some clients using the gateway are available too.

# Structure

- VelibGatewayWS is the main project that exposes SOAP API.

- ConsoleClient is a console client to check available bikes on a station

- WpfApp1 correspond to a graphic interface

- AsyncClient implement an asynchrone client
# Development Extensions

- Graphical User Interface for the client 

- Asynchronous version. Use  MTObservableCollection to dynamically add stations and contracts to one thread to another.

- Cached api : Each api call have useCache parameter, to let the client decide if he want to use cache.


# Import and test project

Steps to import and use project
- Import the project from VelibGatewayWS.sln. The gateway and clients should come to solution explorater.
- Each client are independent and have the gateway service reference. 
- You can execute ConsoleClient, VelibClient and AsyncClient.



@author Francois Melkonian