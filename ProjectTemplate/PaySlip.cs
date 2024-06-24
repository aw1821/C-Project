using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTemplate
{
    public class PaySlip
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public bool TaxFreeThreshold { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal Superannuation { get; set; }
        public decimal Tax { get; set; }

        public PaySlip(int employeeID, string firstName, string lastName, int hoursWorked, decimal hourlyRate, bool taxFreeThreshold)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
            TaxFreeThreshold = taxFreeThreshold;
            CalculatePays();
        }

        private void CalculatePays()
        {
            GrossPay = HoursWorked * HourlyRate;
            Tax = CalculateTax(GrossPay, TaxFreeThreshold);
            NetPay = GrossPay - Tax;
            Superannuation = GrossPay * 0.1m; // Example superannuation calculation
        }

        private decimal CalculateTax(decimal grossPay, bool taxFreeThreshold)
        {
            decimal tax = 0;

            if (taxFreeThreshold)
            {
                if (grossPay < 359)
                {
                    tax = 0;
                }
                else if (grossPay < 438)
                {
                    tax = 0.1900m * grossPay - 68.3462m;
                }
                else if (grossPay < 548)
                {
                    tax = 0.2900m * grossPay - 112.1942m;
                }
                else if (grossPay < 721)
                {
                    tax = 0.2100m * grossPay - 68.3465m;
                }
                else if (grossPay < 865)
                {
                    tax = 0.2190m * grossPay - 74.8369m;
                }
                else if (grossPay < 1282)
                {
                    tax = 0.3477m * grossPay - 186.2119m;
                }
                else if (grossPay < 2307)
                {
                    tax = 0.3450m * grossPay - 182.7504m;
                }
                else if (grossPay < 3461)
                {
                    tax = 0.3900m * grossPay - 286.5965m;
                }
                else
                {
                    tax = 0.4700m * grossPay - 563.5196m;
                }
            }
            else
            {
                if (grossPay < 88)
                {
                    tax = 0.1900m * grossPay;
                }
                else if (grossPay < 371)
                {
                    tax = 0.2348m * grossPay - 3.9639m;
                }
                else if (grossPay < 515)
                {
                    tax = 0.2190m * grossPay + 1.9003m;
                }
                else if (grossPay < 932)
                {
                    tax = 0.3477m * grossPay - 64.4297m;
                }
                else if (grossPay < 1957)
                {
                    tax = 0.3450m * grossPay - 61.9132m;
                }
                else if (grossPay < 3111)
                {
                    tax = 0.3900m * grossPay - 150.0093m;
                }
                else
                {
                    tax = 0.4700m * grossPay - 398.9324m;
                }
            }

            return tax;
        }
    }



}
