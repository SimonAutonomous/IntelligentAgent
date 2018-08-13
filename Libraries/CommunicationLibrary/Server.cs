using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CommunicationLibrary
{
    public class Server
    {
        #region Constants
        private const int DEFAULT_BACKLOG = 20;
        private const int DEFAULT_BUFFER_SIZE = 1024;
        #endregion

        #region Fields
        private string name = "";
        private int backLog;
        private int bufferSize;
        private Socket serverSocket;
        private Boolean connected = false;
        private List<ClientState> clientStateList = null;
        private static object clientListLockObject = new object();
        private int clientIndex = 0;
        #endregion

        #region Event handlers
        public event EventHandler<CommunicationProgressEventArgs> Progress = null;
        public event EventHandler<CommunicationErrorEventArgs> Error = null;
        public event EventHandler<DataPacketEventArgs> Received = null;
        public event EventHandler<ClientConnectedEventArgs> ClientConnected = null; 
        #endregion

        #region Constructors
        public Server()
        {
            clientStateList = new List<ClientState>();
            clientIndex = 0;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            backLog = DEFAULT_BACKLOG;
            bufferSize = DEFAULT_BUFFER_SIZE;
        }
        #endregion

        #region Private methods
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

        private void OnClientConnected(string clientName, string clientID)
        {
            if (ClientConnected != null)
            {
                EventHandler<ClientConnectedEventArgs> handler = ClientConnected;
                ClientConnectedEventArgs e = new ClientConnectedEventArgs(clientName, clientID);
                handler(this, e);
            }
        }

        private Boolean Bind(string ipAddressString, int serverPort)
        {
            Boolean ok = true;
            IPAddress ipAddress = IPAddress.Parse(ipAddressString);
            string[] ipAddressStringSplit = ipAddressString.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                serverSocket.Bind(new IPEndPoint(ipAddress, serverPort));
            }
            catch (SocketException ex)
            {
                ok = false;
                OnError(ex.Message);
            }
            return ok;
        }

        private void Listen()
        {
            serverSocket.Listen(backLog);
         //   OnProgress(CommunicationAction.Connect, null);
        }

        private void AcceptClientsCallBack(IAsyncResult asyncResult)
        {
            if (!connected) { return; }
            Socket clientSocket = null;
            try
            {
                clientSocket = serverSocket.EndAccept(asyncResult);
                ClientState clientState = new ClientState(bufferSize, clientSocket);
                OnProgress(CommunicationAction.Connect, "Client detected");
                Receive(clientState);
                AcceptClients();
            }
            catch (SocketException ex)
            {
                OnError(ex.Message);
                if (clientSocket != null) { clientSocket.Close(); }
            }
        }

        private void ProcessConnectionRequest(DataPacket dataPacket, ClientState clientState)
        {
          //  string clientName = dataPacket.SenderID;  // The client name is set here (same as the SenderID)
            Monitor.Enter(clientListLockObject);
            if (clientStateList == null) { clientStateList = new List<ClientState>(); }
            string clientID = "Client" + clientIndex.ToString();
            clientIndex++; // Cannot be taken as clientStateList.Count-1, since clients might be removed, then added again etc.
            clientState.ClientName = dataPacket.SenderName;
            clientState.ClientID = clientID;
            clientState.Connected = true;
            clientStateList.Add(clientState);
            Monitor.Exit(clientListLockObject);
            OnClientConnected(clientState.ClientName, clientState.ClientID); 
            OnProgress(CommunicationAction.Connect, clientState.ClientName + " (" + clientID + ")" + " connected");
        }

        private void ProcessReceivedMessage(DataPacket dataPacket, ClientState clientState)
        {
            if (!clientState.Connected)
            {
               ProcessConnectionRequest(dataPacket, clientState);
            }
            else
            {
                OnReceived(dataPacket, clientState.ClientID);
                OnProgress(CommunicationAction.Receive, dataPacket.AsBytes().Length.ToString() + " bytes received from " + clientState.ClientName
                     + " ("+clientState.ClientID+")");
            }
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            ClientState clientState = (ClientState)asyncResult.AsyncState;
            if (clientState == null) { return; }
            if (!connected) { return; } // Might have disconnected asynchronously
            try
            {
                Socket clientSocket = clientState.ClientSocket;
                int receivedMessageSize = clientSocket.EndReceive(asyncResult);
                if (receivedMessageSize == 0)
                {
                    OnError(clientState.ClientName + " (" + clientState.ClientID + ")" + " disconnected");
                    Monitor.Enter(clientStateList);
                    int clientStateIndex = clientStateList.FindIndex(s => s.ClientID == clientState.ClientID);
                    if (clientStateIndex >= 0)  // Should always be the case!
                    {
                        clientState.ClientSocket.Close();
                        clientStateList.RemoveAt(clientStateIndex);
                    }
                    Monitor.Exit(clientStateList);
                }
                else
                {
                    byte[] messageAsBytes = new byte[receivedMessageSize];
                    Array.Copy(clientState.ReceiveBuffer, messageAsBytes, receivedMessageSize);
                    DataPacket dataPacket = new DataPacket();
                    Boolean ok = dataPacket.Generate(messageAsBytes);
                    if (ok) { ProcessReceivedMessage(dataPacket, clientState); }
                    else
                    {
                        OnError("Invalid message received");
                    }
                    Receive(clientState);
                }
            }
            catch (SocketException ex)
            {

                // To do: Perhaps add code here. Add a check to see whether or not the
                // connection is valid? I think that if 0 bytes are received, the
                // connection has failed. Should check this! (but that would not
                // be a SocketException - that check (0 bytes received) should perhaps
                // be included above instead!

                if (clientState.ClientSocket != null)
                {
                    Monitor.Enter(clientListLockObject); // Monitor.Enter(clientStateList);
                    if (clientStateList != null)
                    {
                        int clientStateIndex = clientStateList.FindIndex(s => s.ClientID == clientState.ClientID);
                        if (clientStateIndex >= 0)  // Should always be the case!
                        {
                            clientState.ClientSocket.Close();
                            clientStateList.RemoveAt(clientStateIndex);
                        }
                    }
                    Monitor.Exit(clientListLockObject); //  Monitor.Exit(clientStateList);
                }
                OnError(ex.Message);
            }
        }

        private void SendCallBack(IAsyncResult asyncResult)
        {
            try
            {
                if (connected) // Might have been disconnected asynchronously
                {
                    ClientState clientState = (ClientState)asyncResult.AsyncState;
                    int bytesSent = clientState.ClientSocket.EndSend(asyncResult);
                    OnProgress(CommunicationAction.Send, "Sent " + bytesSent.ToString() + " bytes to " + clientState.ClientName + " (" + clientState.ClientID + ")");
                }
            }
            catch
            {
                OnError("Failed to send data to client");
            }
        }
        #endregion

        #region Public methods
        public void Connect(string ipAddressString, int serverPort)
        {
            Boolean ok = Bind(ipAddressString, serverPort);
            if (ok)
            {
                if (serverSocket.IsBound) // Should always be true, if ok = True (check!)
                {
                    connected = true;
                    OnProgress(CommunicationAction.Connect, name + " connected");
                    Listen();
                }
                else { connected = false; }
            }
            else { connected = false; }
        }

        public void Disconnect()
        {
            connected = false;
            Monitor.Enter(clientListLockObject);
            if (clientStateList != null)
            {
                while (clientStateList.Count > 0)
                {
                    clientStateList[0].ClientSocket.Close();
                    clientStateList.RemoveAt(0);
                }
                clientStateList = null;
            }
            Monitor.Exit(clientListLockObject);
            serverSocket.Close();
            OnProgress(CommunicationAction.Disconnect, name + " disconnected");
        }

        public void Receive(ClientState clientState)
        {
            Socket clientSocket = clientState.ClientSocket;
            clientSocket.BeginReceive(clientState.ReceiveBuffer, 0, clientState.ReceiveBuffer.Length, SocketFlags.None,
                    new AsyncCallback(ReceiveCallBack), clientState);
        }

        public void Send(string clientID, string message)
        {
            ClientState clientState = clientStateList.Find(s => s.ClientID == clientID);
            if (clientState != null)
            {
                if (clientState.ClientSocket.Connected)
                {
                    DataPacket dataPacket = new DataPacket();
                    dataPacket.SenderName = name;
                    dataPacket.Message = message;
                    byte[] dataPacketAsBytes = dataPacket.AsBytes();
                    clientState.SendBuffer = dataPacketAsBytes;
                    Socket clientSocket = clientState.ClientSocket;
                    try
                    {
                        clientSocket.BeginSend(clientState.SendBuffer, 0, clientState.SendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), clientState);
                    }
                    catch
                    {
                        OnError("Unable to send message to " + clientState.ClientName);
                    }
                }
                else // Socket no long connected: Remove
                {
                    OnError("Unable to send message to " + clientState.ClientName);
                    Monitor.Enter(clientStateList);
                    int clientStateIndex = clientStateList.FindIndex(s => s.ClientID == clientID);
                    clientState.ClientSocket.Close();
                    clientStateList.RemoveAt(clientStateIndex);
                    Monitor.Exit(clientStateList);
                }
            }
        }  

        public void AcceptClients()
        {
            serverSocket.BeginAccept(new AsyncCallback(AcceptClientsCallBack), null);
        }

        public List<string> GetClientNameList()
        {
            List<string> clientNameList = new List<string>();
            Monitor.Enter(clientListLockObject);
            foreach (ClientState clientState in clientStateList)
            {
                clientNameList.Add(clientState.ClientName);
            }
            Monitor.Exit(clientListLockObject);
            return clientNameList;
        }

        public List<string> GetClientIDList()
        {
            List<string> clientIDList = new List<string>();
            Monitor.Enter(clientListLockObject);
            foreach (ClientState clientState in clientStateList)
            {
                clientIDList.Add(clientState.ClientID);
            }
            Monitor.Exit(clientListLockObject);
            return clientIDList;
        }

        public string GetFirstClientID(string clientName)
        {
            Monitor.Enter(clientListLockObject);
            ClientState clientState = clientStateList.Find(c => c.ClientName == clientName);
            string clientID = null;
            if (clientState != null)
            {
                clientID = clientState.ClientID;
            }
            Monitor.Exit(clientListLockObject);
            return clientID;
        }

        public string GetClientName(string clientID)
        {
            Monitor.Enter(clientListLockObject);
            ClientState clientState = clientStateList.Find(c => c.ClientID == clientID);
            string clientName = null;
            if (clientState != null)
            {
                clientName = clientState.ClientName;
            }
            Monitor.Exit(clientListLockObject);
            return clientName;
        }
        #endregion

        #region Properties
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

        public int BackLog
        {
            get { return backLog; }
            set { backLog = value; }
        }

        public int BufferSize
        {
            get { return bufferSize; }
            set { bufferSize = value; }
        }
        #endregion
    }
}
