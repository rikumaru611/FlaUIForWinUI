using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using FluentAssertions;

namespace TestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("John Doe", 2, true)]
        public void Test1(String name, int age, bool isAgreed)
        {
            FlaUI.Core.Application application = null;
            try
            {
                // Arrange                
                application = FlaUI.Core.Application.Launch(@"Your app path\App1.exe");
                var mainWindow = application.GetMainWindow(new UIA3Automation());
                var cf = new ConditionFactory(new UIA3PropertyLibrary());

                // Act
                mainWindow.FindFirstDescendant(cf.ByAutomationId("InName")).AsTextBox().Enter(name);
                mainWindow.FindFirstDescendant(cf.ByAutomationId("InAge")).AsComboBox().Select(age).Click();
                if (isAgreed)
                {
                    mainWindow.FindFirstDescendant(cf.ByAutomationId("ChkAgree")).AsCheckBox().Click();
                }
                else
                {
                    // Uncheck the checkbox
                }
                mainWindow.FindFirstDescendant(cf.ByAutomationId("btnSubmit")).AsButton().Click();
                var successDialog = mainWindow.FindFirstDescendant(cf.ByAutomationId("SuccessDialog"))?.AsWindow();
                var successDialogTitle = successDialog.FindFirstDescendant(cf.ByAutomationId("ContentScrollViewer")).FindChildAt(0);
                var successDialogMessage = successDialog.FindFirstDescendant(cf.ByAutomationId("ContentScrollViewer")).FindChildAt(1);

                // Assert
                successDialogTitle.Name.Should().Be("Success");
                successDialogMessage.Name.Should().Be($"✅ Submission Successful!\n\n📌 Name: {name}\n📌 Age: 31-40\n📌 Agreed: {isAgreed}");
            }
            catch (Exception ex) { }
            finally
            {
                // Cleanup
                application?.Close();
            }            
        }        
    }
}