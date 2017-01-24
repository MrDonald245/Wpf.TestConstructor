using System.Windows.Input;

namespace Wpf.TestBuilder.Commands
{
    public static class ActionCommands
    {
        public static readonly RoutedUICommand Cancel = new RoutedUICommand(
            "Cancel",
            "Cancel",
            typeof(ActionCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Escape)
            });
    }
}