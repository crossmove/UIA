using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationTestProject.Common;

namespace UIAutomationTestProject.Validators
{
    public class CalcMouseInputTestValidator : UiAutomationValidator
    {
        public CalcMouseInputTestValidator(ProcessStartInfo processStartInfo)
            : base(processStartInfo)
        {
        }

        public override void Run()
        {
            const string calcTextBoxAutomationId = "150";
            const string button9AutomationId = "139";
            const string button1AutomationId = "131";
            const string subtractButtonAutomationId = "94";
            const string equalButtonAutomationId = "121";

            // 9 - 1 = 8
            ClickButton(button9AutomationId);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            ClickButton(subtractButtonAutomationId);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            ClickButton(button1AutomationId);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            ClickButton(equalButtonAutomationId);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            // Verify it is equal to 8
            AutomationElement servicer = RootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, calcTextBoxAutomationId));
            Assert.IsNotNull(servicer, calcTextBoxAutomationId);
            Assert.AreEqual("8", servicer.Current.Name);
        }

        private void ClickButton(string buttonAutomationId)
        {
            AutomationElement automationElement = RootElement.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, buttonAutomationId));
            Assert.IsNotNull(automationElement, buttonAutomationId);
            MouseInput.Click(automationElement);
        }
    }
}
