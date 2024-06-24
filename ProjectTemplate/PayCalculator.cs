using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTemplate
{
    public static class PayCalculator
    {
        public static decimal CalculateGrossPay(int hoursWorked, decimal hourlyRate)
        {
            return hoursWorked * hourlyRate;
        }

        public static decimal CalculateTax(decimal grossPay, decimal taxThreshold)
        {
            return (grossPay - taxThreshold) * 0.2m;
        }

        public static decimal CalculateNetPay(decimal grossPay, decimal tax)
        {
            return grossPay - tax;
        }

        public static decimal CalculateSuperannuation(decimal grossPay)
        {
            return grossPay * 0.1m; 
        }
    }

}
