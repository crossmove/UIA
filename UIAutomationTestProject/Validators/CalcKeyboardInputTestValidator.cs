using System.Diagnostics;
using System.Windows.Automation;
using Microsoft.Test.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UIAutomationTestProject.Validators
{
    public class CalcKeyboardInputTestValidator : UiAutomationValidator
    {
        public CalcKeyboardInputTestValidator(ProcessStartInfo processStartInfo)
            : base(processStartInfo)
        {
        }

        public override void Run()
        {
            const string calcTextBoxAutomationId = "150";

            // 9 - 1 = 8
            Keyboard.Type(Key.D9);
            Keyboard.Type(Key.Subtract);
            Keyboard.Type(Key.D1);
            Keyboard.Type(Key.Enter);

            // Verify it is equal to 8
            AutomationElement servicer = RootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, calcTextBoxAutomationId));
            Assert.IsNotNull(servicer, calcTextBoxAutomationId);
            Assert.AreEqual("8", servicer.Current.Name);
        }
    }
}
