using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Automation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {         
            // Get user inputs
            string name = txtName.Text.Trim();
            string age = (cmbAge.SelectedItem as ComboBoxItem)?.Content.ToString();
            bool isAgreed = chkAgree.IsChecked ?? false;

            // Validation
            if (string.IsNullOrWhiteSpace(name) || name.Length > 30)
            {
                txtValidationMessage.Text = "Name must be between 1-30 characters.";
                txtValidationMessage.Visibility = Visibility.Visible;
                return;
            }

            if (age == null)
            {
                txtValidationMessage.Text = "Please select an age.";
                txtValidationMessage.Visibility = Visibility.Visible;
                return;
            }

            if (!isAgreed)
            {
                txtValidationMessage.Text = "You must agree to continue.";
                txtValidationMessage.Visibility = Visibility.Visible;
                return;
            }

            // Clear validation message if all checks pass
            txtValidationMessage.Visibility = Visibility.Collapsed;


            var successDialog = new ContentDialog
            {
                Title = "Success",
                CloseButtonText = "OK",
                Content = new TextBlock
                {
                    Text = $"Submitted successfully!\n\nName: {name}\nAge: {age}\nAgreed: {isAgreed}",
                    TextWrapping = TextWrapping.Wrap
                }
            };

            // Show Success Dialog            
            await ShowSuccessDialog(name, age, isAgreed);
        }

        private async Task ShowSuccessDialog(string name, string age, bool isAgreed)
        {
            var successDialog = new ContentDialog
            {
                Title = "Success",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot, // Ensure it's centered in the main window
                MinWidth = 300, // Set minimum width
                MinHeight = 200 // Set minimum height
            };

            AutomationProperties.SetAutomationId(successDialog, "SuccessDialog");

            var stackPanel = new StackPanel();
            var textBlock = new TextBlock
            {
                Text = $"✅ Submission Successful!\n\n📌 Name: {name}\n📌 Age: {age}\n📌 Agreed: {isAgreed}",
                TextWrapping = TextWrapping.Wrap,
                FontSize = 16, // Increase font size
                Padding = new Thickness(10)
            };

            stackPanel.Children.Add(textBlock);
            successDialog.Content = stackPanel;
            await successDialog.ShowAsync();
        }

    }
}
