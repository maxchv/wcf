using System.Runtime.Serialization;

namespace CalculatorService
{
    [DataContract]
    class ExceptionExType
    {
        [DataMemberAttribute]
        public string Message { get; set; }

        [DataMemberAttribute]
        public string MethodName { get; set; }

        [DataMemberAttribute]
        public string Description { get; set; }

        [DataMemberAttribute]
        public int Line { get; set; }
    }
}