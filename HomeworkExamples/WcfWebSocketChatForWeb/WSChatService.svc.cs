using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WcfWSChatForWeb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    
    
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WSChatService : IWSChatService
    {
        List<IWSChatCallback> clients = new List<IWSChatCallback>();
        public async Task SendMessageToServer(Message msg)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IWSChatCallback>();
            if (msg.IsEmpty || ((IChannel)callback).State != CommunicationState.Opened)
            {
                clients.Add(callback);
                return;
            }

            byte[] body = msg.GetBody<byte[]>();
            string msgTextFromClient = Encoding.UTF8.GetString(body);

            List<IWSChatCallback> dissconnectedClients = new List<IWSChatCallback>();
            foreach (var client in clients)
            {
                try
                {
                    if (!client.Equals(callback))
                    {
                        await client.SendMessageToClient(
                        CreateMessage(msgTextFromClient));
                    }
                }
                catch (Exception)
                {
                    dissconnectedClients.Add(client);
                }
            }
            foreach (var client in dissconnectedClients)
            {
                clients.Remove(client);
            }
        }

        private Message CreateMessage(string msgText)
        {
            Message msg = ByteStreamMessage.CreateMessage(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(msgText)));
            msg.Properties["WebSocketMessageProperty"] =
                new WebSocketMessageProperty
                { MessageType = WebSocketMessageType.Text };
            return msg;
        }
    }
}
