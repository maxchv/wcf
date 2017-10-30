using System.ServiceModel;

namespace CalculatorService
{
    [ServiceContract(CallbackContract = typeof(ICalculationEvent))]
    public interface ICalculatorService
    {
        [OperationContract(IsOneWay = true)]
        void Sum(decimal a, decimal b);
        
        [OperationContract(IsOneWay = true)]
        void Sub(decimal a, decimal b);
        
        [OperationContract(IsOneWay = true)]
        void Mult(decimal a, decimal b);
        
        [OperationContract(IsOneWay = true)]
        void Div(decimal a, decimal b);
    }

    [ServiceContract]
    public interface ICalculationEvent
    {
        [OperationContract(IsOneWay = true)]
        void Calculation(decimal resultCalculation);
    }
}