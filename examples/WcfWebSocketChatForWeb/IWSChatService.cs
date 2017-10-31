using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfWSChatForWeb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IWSChatCallback))]
    public interface IWSChatService
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        Task SendMessageToServer(Message msg);
    }
}
