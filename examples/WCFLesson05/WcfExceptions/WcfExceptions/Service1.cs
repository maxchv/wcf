
using System.ServiceModel;


namespace WcfExceptions
{   
    public class ServiceCalculator : IServiceCalculator
    {
        public int Div(int a, int b)
        {            
            try
            {
                return a / b;
            }
            catch(System.DivideByZeroException ex)
            {
                DevideByZeroExeption myex = new DevideByZeroExeption();
                myex.Error = ex.Message;
                myex.Details = "Denominator should be non zero";
                throw new FaultException<DevideByZeroExeption>(myex);
            }            
        }
    }
}
