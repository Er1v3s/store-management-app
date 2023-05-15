using System;
using System.Windows.Input;

namespace StoreManagementApp
{
    public interface IWindowManipulationMethods
    {
        private void DragWindow(object sender, MouseButtonEventArgs e) { }
        private void Window_StateChanged(object sender, EventArgs e) { }

    }
}
