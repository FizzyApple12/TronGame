using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor.PackageManager;

public class ServerConnection : MonoBehaviour
{
    public static bool connected = false;

    static List<Disc> kinematicDiscs = new List<Disc>();

    static IPEndPoint serverEndpoint;
    static UdpClient serverConnection;

    static Protocol serverProtocol;

    public static void Connect(string ip, int port)
    {
        serverEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);
        serverConnection = new UdpClient();

        try
        {
            serverConnection.Connect(ip, port);
            serverConnection.BeginReceive(new AsyncCallback(ServerData), null);

            connected = true;
        }
        catch (Exception e)
        {
            // we can do something later :3
        }

        serverProtocol = new Protocol();

    }
    public static void Disonnect()
    {
        if (!connected)
            return;

        serverConnection.Close();
        serverConnection.Dispose();
        serverConnection = null;

        serverEndpoint = null;
    }

    //CallBack
    static void ServerData(IAsyncResult result)
    {
        byte[] received = serverConnection.EndReceive(result, ref serverEndpoint);
        
        //process data here
        
        serverConnection.BeginReceive(new AsyncCallback(ServerData), null);
    }

    public static void SendDiscThrow(Disc disc)
    {

    }
}
