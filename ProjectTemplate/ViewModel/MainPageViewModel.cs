using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ProjectTemplate.ViewModel
{

    public class Person : ObservableObject
    {
        private int id;
        public int employeeID
        {
            get => id;
            set
            {
                if (SetProperty(ref id, value))
                    OnPropertyChanged(nameof(employeeID));
            }
        }

        private string firstname;
        public string firstName
        {
            get => firstname;
            set
            {
                if (SetProperty(ref firstname, value))
                    OnPropertyChanged(nameof(firstName));
            }
        }

        private string lastname;
        public string lastName
        {
            get => lastname;
            set
            {
                if (SetProperty(ref lastname, value))
                    OnPropertyChanged(nameof(lastName));
            }
        }

        private string tEmp;
        public string typeEmployee
        {
            get => tEmp;
            set
            {
                if (SetProperty(ref tEmp, value))
                    OnPropertyChanged(nameof(typeEmployee));
            }
        }

        private int hrlyRate;
        public int hourlyRate
        {
            get => hrlyRate;
            set
            {
                if (SetProperty(ref hrlyRate, value))
                    OnPropertyChanged(nameof(hourlyRate)); ;
            }
        }

        private string txThr;
        public string taxthreshold
        {
            get => txThr;
            set
            {
                if (SetProperty(ref txThr, value))
                    OnPropertyChanged(nameof(taxthreshold));
            }
        }
    }
    public sealed class CsvMap : ClassMap<Person>
    {
        public CsvMap()
        {
            Map(m => m.employeeID).Index(0);
            Map(m => m.firstName).Index(1);
            Map(m => m.lastName).Index(2);
            Map(m => m.typeEmployee).Index(3);
            Map(m => m.hourlyRate).Index(4);
            Map(m => m.taxthreshold).Index(5);
        }
    }


    public partial class MainPageViewModel: ObservableObject
    {
        public MainPageViewModel()
        {
            Employees = new ObservableCollection<Person>();
        }

        [ObservableProperty]
        ObservableCollection<Person> employees = new ObservableCollection<Person>();


        [RelayCommand]
        public async void ImportSomeRecords()
        {
            string fileName = "employee.csv";
            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);

            using (var reader = new StreamReader(fileStream))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CsvMap>();


                    int employeeID;
                    string firstName;
                    string lastName;
                    string typeEmployee;
                    int hourlyRate;
                    string taxthreshold;


                    while (csv.Read())
                    {
                        employeeID = csv.GetField<int>(0);
                        firstName = csv.GetField<string>(1);
                        lastName = csv.GetField<string>(2);
                        typeEmployee = csv.GetField<string>(3);
                        hourlyRate = csv.GetField<int>(4);
                        taxthreshold = csv.GetField<string>(5);
                        employees.Add(CreateRecord(employeeID, firstName, lastName, typeEmployee, hourlyRate, taxthreshold));

                    }

                }

            }
        }

        public static Person CreateRecord(int employeeID, string firstName, string lastName, string typeEmployee, int hourlyRate, string taxthreshold)
        {
            Person record = new Person();

            record.employeeID = employeeID;
            record.firstName = firstName;
            record.lastName = lastName;
            record.typeEmployee = typeEmployee;
            record.hourlyRate = hourlyRate;
            record.taxthreshold = taxthreshold;

            return record;
        }
        /*
        [RelayCommand]
        public void ShowPaySlip(Person selectedEmployee)
        {
            if (selectedEmployee != null)
            {
                var paySlip = new PaySlip(selectedEmployee.employeeID, selectedEmployee.firstName, selectedEmployee.lastName, 40, selectedEmployee.hourlyRate, decimal.Parse(selectedEmployee.taxthreshold));
                // Display PaySlip details in the UI (e.g., using data binding)
            }
        }
        */
        [RelayCommand]
        public void ShowPaySlip(Person selectedEmployee)
        {
            if (selectedEmployee != null)
            {
                bool taxFreeThreshold = selectedEmployee.taxthreshold.Equals("Y", StringComparison.OrdinalIgnoreCase);
                var paySlip = new PaySlip(selectedEmployee.employeeID, selectedEmployee.firstName, selectedEmployee.lastName, 0, selectedEmployee.hourlyRate, taxFreeThreshold);
                // Display PaySlip details in the UI (e.g., using data binding)
            }
        }

        [ObservableProperty]
        Person selectedEmployee;

        [ObservableProperty]
        string hoursWorked;

        [ObservableProperty]
        string id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string role;

        [ObservableProperty]
        string taxThreshold;

        [ObservableProperty]
        string hourlyRate;

        [ObservableProperty]
        string grossPay;

        [ObservableProperty]
        string tax;

        [ObservableProperty]
        string netPay;

        [ObservableProperty]
        string superannuation;

        [RelayCommand]
        public void CalculatePay()
        {
            if (SelectedEmployee != null && int.TryParse(hoursWorked, out int hours))
            {
                bool taxFreeThreshold = SelectedEmployee.taxthreshold.Equals("Y", StringComparison.OrdinalIgnoreCase);

                var paySlip = new PaySlip(SelectedEmployee.employeeID, SelectedEmployee.firstName, SelectedEmployee.lastName, hours, SelectedEmployee.hourlyRate, taxFreeThreshold);

                Id = paySlip.EmployeeID.ToString();
                Name = $"{paySlip.FirstName} {paySlip.LastName}";
                Role = SelectedEmployee.typeEmployee;
                TaxThreshold = paySlip.TaxFreeThreshold ? "Yes" : "No";
                HoursWorked = hours.ToString();
                HourlyRate = paySlip.HourlyRate.ToString("C");
                GrossPay = paySlip.GrossPay.ToString("C");
                Tax = paySlip.Tax.ToString("C");
                NetPay = paySlip.NetPay.ToString("C");
                Superannuation = paySlip.Superannuation.ToString("C");
            }
        }

        [RelayCommand]
        public async void ExportPaySlip()
        {
            /*
            if (SelectedEmployee != null)
            {
                var paySlip = new PaySlip(SelectedEmployee.employeeID, SelectedEmployee.firstName, SelectedEmployee.lastName, int.Parse(hoursWorkedEntry.Text), SelectedEmployee.hourlyRate, decimal.Parse(SelectedEmployee.taxthreshold));
                string fileName = $"Pay-{SelectedEmployee.employeeID}-{SelectedEmployee.firstName}-{DateTime.Now:yyyyMMddHHmmss}.csv";
                using (var writer = new StreamWriter(fileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecord(paySlip);
                    await writer.FlushAsync();
                }
                // Notify user of successful export
            }*/
        }
    }
}


