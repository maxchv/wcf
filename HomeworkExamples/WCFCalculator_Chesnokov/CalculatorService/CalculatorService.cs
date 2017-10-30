using System.ServiceModel;

namespace CalculatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculatorService" in both code and config file together.
    public class CalculatorService : ICalculatorService
    {
        public ICalculationEvent Callback
        {
            get => OperationContext.Current.GetCallbackChannel<ICalculationEvent>();
        }

        public void Sum(decimal a, decimal b)
        {
            decimal result = a + b;
            try
            {
                Callback.Calculation(result);
            }
            catch 
            {
                // ignored
            }
        }

        public void Sub(decimal a, decimal b)
        {
            try
            {
                decimal result = a - b;
                Callback.Calculation(result);
            }
            catch
            {
                // ignored
            }
        }

        public void Mult(decimal a, decimal b)
        {
            try
            {
                decimal result = a * b;
                Callback.Calculation(result);
            }
            catch
            {
                // ignored
            }
        }

        public void Div(decimal a, decimal b)
        {
            try
            {
                decimal result = a / b;

                Callback.Calculation(result);
            }
            catch
            {
                // ignored
            }
        }
    }
}