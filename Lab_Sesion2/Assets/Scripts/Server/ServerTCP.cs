using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using System.Text;
using UnityEditor;
using System.Collections.Generic;
using static ServerTCP;

public class ServerTCP : MonoBehaviour
{
    Socket socket;
    Thread mainThread = null;
    string serverText;

    List<User> connectedUsers = new List<User>();

    public GameObject functionalities;

    public class User
    {
        public string name;
        public Socket socket;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    public void startServer()
    {
        serverText = "Starting TCP Server...";

        //TO DO 1
        //Create and bind the socket
        //Any IP that wants to connect to the port 9050 with TCP, will communicate with this socket
        //Don't forget to set the socket in listening mode
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 9050);
        socket.Bind(endPoint);

        socket.Listen(10);
 

        //TO DO 3
        //TIme to check for connections, start a thread using CheckNewConnections
        mainThread = new Thread(CheckNewConnections);
        mainThread.Start();
    }

    void CheckNewConnections()
    {
        serverText = serverText + "\n" + "Waiting for new Client...";

        while (true)
        {
            User newUser = new User();
            newUser.name = "";
            //TO DO 3
            //TCP makes it so easy to manage conections, so we are going
            //to put it to use
            //Accept any incoming clients and store them in this user.
            //When accepting, we can now store a copy of our server socket
            //who has established a communication between a
            //local endpoint (server) and the remote endpoint(client)
            //If you want to check their ports and adresses, you can acces
            //the socket's RemoteEndpoint and LocalEndPoint
            //try printing them on the console

            newUser.socket = socket.Accept();//accept the socket

            lock (connectedUsers)
            {
                connectedUsers.Add(newUser);
            }

            BroadcastMessageServer($"{newUser.name} is now connected.", newUser);

            IPEndPoint clientep = (IPEndPoint)newUser.socket.RemoteEndPoint;
            serverText = serverText + "\n" + "Connected with " + clientep.Address.ToString() + " at port " + clientep.Port.ToString();

            //TO DO 5
            //For every client, we call a new thread to receive their messages. 
            //Here we have to send our user as a parameter so we can use it's socket.
            Thread newConnection = new Thread(() => Receive(newUser));
            newConnection.Start();
        }
        //This users could be stored in the future on a list
        //in case you want to manage your connections

    }

    void Receive(User user)
    {
        //TO DO 5
        //Create an infinite loop to start receiving messages for this user
        //You'll have to use the socket function receive to be able to get them.
        byte[] data = new byte[1024];
        int recv = 0;

        while (true)
        {
            data = new byte[1024];
            recv = user.socket.Receive(data);
            
            if (recv == 0)
                break;

            string recievedMessage = Encoding.ASCII.GetString(data, 0, recv);
            recievedMessage = $"{user.name} - " + recievedMessage;
            serverText = serverText + "\n" + recievedMessage;

            BroadcastMessageServer(recievedMessage, user);

            //TO DO 6
            //We'll send a ping back every time a message is received
            //Start another thread to send a message, same parameters as this one.
            //Thread answer = new Thread(() => Send(user));
            //answer.Start();
        }

        lock (connectedUsers)
        {
            BroadcastMessageServer($"{user.name} has disconnected.", user);
            connectedUsers.Remove(user);
        }
        user.socket.Close();
    }

    public void BroadcastMessageServer(string message, User sender)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(message);

        lock (connectedUsers)
        {
            foreach (User user in connectedUsers)
            {
                if (user.socket != sender.socket)  // Don't send to the sender
                {
                    user.socket.Send(buffer);
                }
            }
        }
    }

    //TO DO 6
    //Now, we'll use this user socket to send a "ping".
    //Just call the socket's send function and encode the string.
    void Send(User user)
    {
        byte[] buffer = new byte[1024];
        buffer = Encoding.ASCII.GetBytes("PINGA");

        user.socket.Send(buffer);
    }
}
