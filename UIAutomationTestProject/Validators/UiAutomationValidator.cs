using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using UIAutomationTestProject.Common;

namespace UIAutomationTestProject.Validators
{
    /// <summary>
    /// It enables out of process testing by using AutomationElement to remote access element in
    /// application, so we can perform testing for different scenarios.
    /// Example, mouse click on a button.
    /// </summary>
    public abstract class UiAutomationValidator : IDisposable
    {
        protected UiAutomationValidator(ProcessStartInfo processStartInfo)
        {
            AutoResetEvent syncEvent = new AutoResetEvent(false);
            AutomationEventHandler onWindowOpened = null;
            onWindowOpened = delegate(object sender, AutomationEventArgs e)
            {
                if (_process != null)
                {
                    if (_process.MainWindowHandle != IntPtr.Zero)
                    {
                        Automation.RemoveAutomationEventHandler(WindowPatternIdentifiers.WindowOpenedEvent, AutomationElement.RootElement, onWindowOpened);
                        RootElement = AutomationElement.FromHandle(_process.MainWindowHandle);

                        NativeMethods.SetForegroundWindow(_process.MainWindowHandle);

                        syncEvent.Set();
                    }
                }
            };

            Automation.AddAutomationEventHandler(WindowPatternIdentifiers.WindowOpenedEvent, AutomationElement.RootElement, TreeScope.Subtree, onWindowOpened);

            _process = Process.Start(processStartInfo);

            // Wait unit the RootElement is set
            syncEvent.WaitOne();
        }

        private readonly Process _process = null;

        public AutomationElement RootElement { get; private set; }

        public abstract void Run();

        public void Dispose()
        {
            _process.CloseMainWindow();
            _process.Kill();
        }
    }
}
