using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationTestProject.Validators;

namespace UIAutomationTestProject
{
    [TestClass]
    public class CalcTest
    {
        private const string AppName = "calc.exe";

        [TestMethod, Timeout(60000)]
        public void TestKeyboardInput()
        {
            using (var validator = new CalcKeyboardInputTestValidator(new ProcessStartInfo(AppName)))
            {
                validator.Run();
            }
        }

        [TestMethod, Timeout(60000)]
        public void TestMouseInput()
        {
            using (var validator = new CalcMouseInputTestValidator(new ProcessStartInfo(AppName)))
            {
                validator.Run();
            }
        }
    }
}
