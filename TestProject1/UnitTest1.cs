using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Diagnostics;
using FluentAssertions;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var application = FlaUI.Core.Application.Launch(@"C:\Users\r_tanaka\source\repos\TestProject1\App1\bin\x64\Debug\net8.0-windows10.0.19041.0\App1.exe");
            var mainWindow = application.GetMainWindow(new UIA3Automation());
            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByAutomationId("InName")).AsTextBox().Enter("John Doe");
            mainWindow.FindFirstDescendant(cf.ByAutomationId("InAge")).AsComboBox().Select(2).Click();
            mainWindow.FindFirstDescendant(cf.ByAutomationId("ChkAgree")).AsCheckBox().Click();
            mainWindow.FindFirstDescendant(cf.ByAutomationId("btnSubmit")).AsButton().Click();

            // ✅ Wait for the success dialog
            var successDialog = RetryFind(() => mainWindow.FindFirstDescendant(cf.ByAutomationId("SuccessDialog"))?.AsWindow(), 5000);
            successDialog.Should().NotBeNull();
            //Assert.IsNotNull(successDialog);

            // ✅ Click OK in the dialog
            successDialog.FindFirstDescendant(cf.ByName("OK")).AsButton().Click();

            /// Helper function to retry finding an element
        }
        private static T RetryFind<T>(Func<T> findElement, int timeoutMs) where T : class
        {
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds < timeoutMs)
            {
                var element = findElement();
                if (element != null) return element;
                Thread.Sleep(200); // Wait and retry
            }
            return null;
        }
    }
}