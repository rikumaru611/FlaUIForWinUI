using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var application = FlaUI.Core.Application.Launch(@"C:\Users\rikut\source\repos\App1\App1\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\App1.exe");
            var mainWindow = application.GetMainWindow(new UIA3Automation());
            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByAutomationId("InName")).AsTextBox().Enter("John Doe");
            mainWindow.FindFirstDescendant(cf.ByAutomationId("InAge")).AsComboBox().Select(2).Click();
            mainWindow.FindFirstDescendant(cf.ByAutomationId("ChkAgree")).AsCheckBox().Click();
            mainWindow.FindFirstDescendant(cf.ByAutomationId("btnSubmit")).AsButton().Click();
        }
    }
}