﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSender : MonoBehaviour
{

	public int port;
    public string IP;
    private IPEndPoint remoteEndPoint;
	private UdpClient client;
    private UINetworkWindow networkWindow; 

	void Start ()
	{
        remoteEndPoint = new IPEndPoint (IPAddress.Parse (IP), port);
        client = new UdpClient();

        if(GameObject.FindGameObjectWithTag("UIManager"))
        {
            networkWindow = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UINetworkWindow>();
            networkWindow.AddMessage("Sending to " + IP + " with " + port);
        } else
        {
            Debug.LogWarning("No networkwindow detected for displaying messages."); 
        }

	}

	public void SendString (string message)
	{
		try {
			// encode string to UTF8-coded bytes
			byte[] data = Encoding.UTF8.GetBytes (message);
			
			// send the data
			client.Send (data, data.Length, remoteEndPoint);
            Debug.Log("Message sent to GH: " + message);
            networkWindow.AddMessage("Message sent to GH: " + message);

        } catch (Exception err) {
			print (err.ToString ());
		}
	}
}
