using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CommunicationLibrary
{
    public class Client
    {
        private const int DEFAULT_MILLISECOND_CONNECTION_TIMEOUT = 5000;
        private const int DEFAULT_BUFFER_SIZE = 8192; // 1024;

        private string name = "DefaultClient"; // name field cannot be null or "" !
        private Socket clientSocket;
        private int millisecondConnectionTimeOut = DEFAULT_MILLISECOND_CONNECTION_TIMEOUT;
        private Boolean connected = false;
        private byte[] receiveBuffer;

        #region Event handlers
        public event EventHandler<CommunicationProgressEventArgs> Progress = null;
        public event EventHandler<CommunicationErrorEventArgs> Error = null;
        public event EventHandler<DataPacketEventArgs> Received = null;
        public event EventHandler ConnectionEstablished = null;
        public event EventHandler ConnectionClosed = null;
        #endregion

        public Client()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            receiveBuffer = new byte[DEFAULT_BUFFER_SIZE];
        }

        private void OnProgress(CommunicationAction action, string message)
        {
            if (Progress != null)
            {
                EventHandler<CommunicationProgressEventArgs> handler = Progress;
                CommunicationProgressEventArgs e = new CommunicationProgressEventArgs(action, message);
                handler(this, e);
            }
        }

        private void OnError(string message)
        {
            if (Error != null)
            {
                EventHandler<CommunicationErrorEventArgs> handler = Error;
                CommunicationErrorEventArgs e = new CommunicationErrorEventArgs(this.name, message);
                handler(this, e);
            }
        }

        private void OnReceived(DataPacket dataPacket, string senderID)
        {
            if (Received != null)
            {
                EventHandler<DataPacketEventArgs> handler = Received;
                DataPacketEventArgs e = new DataPacketEventArgs(dataPacket, senderID);
                handler(this, e);
            }
        }

        private void OnConnectionEstablished()
        {
            if (ConnectionEstablished != null)
            {
                EventHandler handler = ConnectionEstablished;
                handler(this, EventArgs.Empty);
            }
        }

        private void OnConnectionClosed()
        {
            if (ConnectionClosed != null)
            {
                EventHandler handler = ConnectionClosed;
                handler(this, EventArgs.Empty);
            }
        }

        private void SendCallBack(IAsyncResult asyncResult)
        {
            try
            {
                Socket clientSocket = (Socket)asyncResult.AsyncState;
                int bytesSent = clientSocket.EndSend(asyncResult);
                OnProgress(CommunicationAction.Send, "Sent " + bytesSent.ToString() + " bytes to server.");
                if (!connected) // See Stop()
                {
                    clientSocket.Disconnect(false);
                    OnConnectionClosed();
                }
            }
            catch
            {
                connected = false;
                OnError("Failed to send data to server");
                OnConnectionClosed();
            }
        }

        public void Send(string message)
        {
            try
            {
                if (connected)
                {
                    DataPacket dataPacket = new DataPacket();
                    dataPacket.Message = message;
                    dataPacket.TimeStamp = DateTime.Now;
                    dataPacket.SenderName = this.name;
                    byte[] dataPacketAsBytes = dataPacket.AsBytes();
                    clientSocket.BeginSend(dataPacketAsBytes, 0, dataPacketAsBytes.Length, SocketFlags.None, new AsyncCallback(SendCallBack), clientSocket);
                }
            }
            catch
            {
                connected = false;
                OnError("Failed to send data to server");
                OnConnectionClosed();
            }
        }

        public void Send(byte[] dataBuffer)
        {
            try
            {
                if (connected)
                {
                    clientSocket.BeginSend(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), clientSocket);
                }
            }
            catch
            {
                connected = false;
                OnError("Failed to send data to server");
                OnConnectionClosed();
            }
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            try
            {
                if (connected)
                {
                    int receivedMessageSize = clientSocket.EndReceive(asyncResult);
                    byte[] messageAsBytes = new byte[receivedMessageSize];
                    Array.Copy(receiveBuffer, messageAsBytes, receivedMessageSize);
                    DataPacket dataPacket = new DataPacket();
                    Boolean ok = dataPacket.Generate(messageAsBytes);
                    if (ok)
                    {
                        OnReceived(dataPacket, "Server");
                        OnProgress(CommunicationAction.Receive, "Received " + receivedMessageSize.ToString() + " bytes from server");
                    }
                    else
                    {
                        OnError("Corrupted message received");
                    }
                    Receive();
                }
            }
            catch (SocketException ex)
            {
                connected = false;
                OnConnectionClosed();
                OnError(ex.Message);
            }
        }

        private void Receive()
        {
            clientSocket.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None,
                                        new AsyncCallback(ReceiveCallBack), null);
        }

        private void ConnectCallBack(IAsyncResult asyncResult)
        {
            if (clientSocket.Connected)
            {
                OnConnectionEstablished();
                OnProgress(CommunicationAction.Connect, "Connected to server");
                connected = true;
                DataPacket dataPacket = new DataPacket();
                dataPacket.TimeStamp = DateTime.Now;
                dataPacket.SenderName = name;
                dataPacket.Message = "Connect";
                byte[] dataPacketAsBytes = dataPacket.AsBytes();
                Send(dataPacketAsBytes);
                Receive();
            }
            else
            {
                connected = false;
                clientSocket.Close();
                OnConnectionClosed();
                OnError("Failed to connect to server");
            }
        }

        public void Connect(string ipAddressString, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ipAddressString);
            clientSocket.BeginConnect(new IPEndPoint(ipAddress, port), new AsyncCallback(ConnectCallBack), null);
        }

        public void Disconnect()
        {
            connected = false;
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
            OnProgress(CommunicationAction.Disconnect, "Disconnected from server");
            OnConnectionClosed();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Boolean Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        public int MillisecondConnectionTimeOut
        {
            get { return millisecondConnectionTimeOut; }
            set { millisecondConnectionTimeOut = value; }
        }
    }
}
