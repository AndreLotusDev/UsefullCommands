using System.Collections.Generic;

namespace Helpers
{
    public class ResultWrapper<T>
    {
        public ResultWrapper(T data, bool status, List<string> messages, string uiMessage)
        {
            Messages = messages;
            Status = status;
            Data = data;
            UiMessage = uiMessage;
        }
        public List<string> Messages { get; set; }
        public string UiMessage { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
