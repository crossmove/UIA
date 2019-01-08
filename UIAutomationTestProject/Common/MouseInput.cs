using System;
using System.Threading;
using System.Windows.Automation;
using Microsoft.Test.Input;
using Point = System.Drawing.Point;

namespace UIAutomationTestProject.Common
{
    public enum GetLocationMode
    {
        ByClickablePoint,
        ByBoundingRectangle
    }
    public static class MouseInput
    {
        public static void Click(AutomationElement target, MouseButton mouseButton = MouseButton.Left,
            GetLocationMode getLocationMode = GetLocationMode.ByClickablePoint)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            Point location;
            switch (getLocationMode)
            {
                case GetLocationMode.ByClickablePoint:
                    var point = target.GetClickablePoint();
                    location = new Point((int)point.X, (int)point.Y);
                    break;
                case GetLocationMode.ByBoundingRectangle:
                    var rect = target.Current.BoundingRectangle;
                    location = new Point((int)(rect.X + rect.Width / 2), (int)(rect.Y + rect.Height / 2));
                    break;
                default:
                    throw new NotSupportedException(getLocationMode.ToString());
            }

            Mouse.MoveTo(location);
            
            Mouse.Click(mouseButton);
        }

        public static void DoubleClick(AutomationElement target, MouseButton mouseButton = MouseButton.Left, 
            GetLocationMode getLocationMode = GetLocationMode.ByClickablePoint)
        {
            Click(target, mouseButton, getLocationMode);
            Mouse.Click(mouseButton);
        }

        public static void MoveToTopLeft()
        {
            Mouse.MoveTo(new Point(0, 0));
            Thread.Sleep(TimeSpan.FromMilliseconds(1));
        }
    }
}
