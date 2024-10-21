using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;

public class ClientTCP : MonoBehaviour
{
    string clientText;
    Socket server;
    public string serverIp;
    Thread receiveThread;
    bool isConnected = false;

    public GameObject functionalities;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartClient()
    {
        if (isConnected)
        {

        }

        Thread connect = new Thread(Connect);
        connect.Start();
    }
    void Connect()
    {
        //TO DO 2
        //Create the server endpoint so we can try to connect to it.
        //You'll need the server's IP and the port we binded it to before
        //Also, initialize our server socket.
        //When calling connect and succeeding, our server socket will create a
        //connection between this endpoint and the server's endpoint

        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(serverIp), 9050);
        server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        server.Connect(ipep);
        isConnected = true;
        //TO DO 4
        //With an established connection, we want to send a message so the server aacknowledges us
        //Start the Send Thread
        //Thread sendThread = new Thread(Send);
        //sendThread.Start();

        //TO DO 7
        //If the client wants to receive messages, it will have to start another thread. Call Receive()
        receiveThread = new Thread(Receive);
        receiveThread.Start();

    }
    public void Send(string message)
    {
        //TO DO 4
        //Using the socket that stores the connection between the 2 endpoints, call the TCP send function with
        //an encoded message

        byte[] buffer = Encoding.ASCII.GetBytes(message);        
        server.Send(buffer);
    }

    //TO DO 7
    //Similar to what we already did with the server, we have to call the Receive() method from the socket.
    void Receive()
    {
        while (isConnected)
        {
            byte[] data = new byte[1024];
            int recv = server.Receive(data);

            if (recv == 0)
                break;

            clientText = clientText += "\n" + Encoding.ASCII.GetString(data, 0, recv);
        }
    }

    public void Disconnect()
    {
        isConnected = false;
        if (server != null)
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
