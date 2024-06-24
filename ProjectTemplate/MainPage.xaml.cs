using CsvHelper;
using ProjectTemplate.ViewModel;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ProjectTemplate;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        viewModel.ImportSomeRecords();
        
    }

    private void PersonDataGrid_ItemSelected(object sender, SelectionChangedEventArgs e)
    {
        /*var selection = PersonDataGrid.SelectedItem as Person;

        DisplayAlert("Selected", $"FirstName {selection.firstName} lastname{selection.lastName} taxThreshold? {selection.taxthreshold}", "OK");*/
        var selection = PersonDataGrid.SelectedItem as Person;
        if (selection != null)
        {
            Console.WriteLine($"Selected Employee: {selection.firstName} {selection.lastName}");
            (BindingContext as MainPageViewModel).SelectedEmployee = selection;
        }
        else
        {
            Console.WriteLine("No employee selected.");
        }
    }

    //BUTTON EVENT
    //var selection = PersonDataGrid.SelectedItem as Person;
    //PAYSLIP.id = PERSON.id DATA
    private void OnCalculateClicked(object sender, EventArgs e)
    {
        try
        {
            var viewModel = BindingContext as MainPageViewModel;
            if (viewModel.SelectedEmployee != null)
            {
                if (int.TryParse(hoursWorkedEntry.Text, out int hours))
                {
                    Console.WriteLine($"Valid hours entered: {hours}");
                    bool taxFreeThreshold = viewModel.SelectedEmployee.taxthreshold.Equals("Y", StringComparison.OrdinalIgnoreCase);

                    try
                    {
                        var paySlip = new PaySlip(viewModel.SelectedEmployee.employeeID, viewModel.SelectedEmployee.firstName, viewModel.SelectedEmployee.lastName, hours, viewModel.SelectedEmployee.hourlyRate, taxFreeThreshold);

                        lblId.Text = $"ID: {paySlip.EmployeeID}";
                        lblName.Text = $"Name: {paySlip.FirstName} {paySlip.LastName}";
                        lblRole.Text = $"Role: {viewModel.SelectedEmployee.typeEmployee}";
                        lblTaxThreshold.Text = $"Tax Threshold: {(taxFreeThreshold ? "Yes" : "No")}";
                        lblHoursWorked.Text = $"Hours Worked: {hours}";
                        lblHourlyRate.Text = $"Hourly Rate: {paySlip.HourlyRate}";
                        lblGrossPay.Text = $"Gross Pay: {paySlip.GrossPay:C}";
                        lblTax.Text = $"Tax: {paySlip.Tax:C}";
                        lblNetPay.Text = $"Net Pay: {paySlip.NetPay:C}";
                        lblSuperannuation.Text = $"Superannuation: {paySlip.Superannuation:C}";
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error parsing tax threshold or other values: {ex.Message}");
                        DisplayAlert("Error", "Invalid number format in employee data. Please check the tax threshold value.", "OK");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid hours worked value.");
                    DisplayAlert("Error", "Please enter a valid number for hours worked.", "OK");
                }
            }
            else
            {
                Console.WriteLine("Error: No employee selected.");
                DisplayAlert("Error", "Please select an employee.", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            DisplayAlert("Error", "An unexpected error occurred. Please try again.", "OK");
        }
    }






}

