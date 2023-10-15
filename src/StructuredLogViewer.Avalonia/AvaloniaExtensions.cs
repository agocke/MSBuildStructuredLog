using Avalonia.Controls;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace StructuredLogViewer.Avalonia
{
    public static class AvaloniaExtensions
    {
        public static void AddItem(this ItemsControl itemsControl, object o)
        {
            (itemsControl.Items as IList)?.Add(o);
        }

        public static void RemoveItem(this ItemsControl itemsControl, object o)
        {
            (itemsControl.Items as IList)?.Remove(o);
        }

        public static void ClearItems(this ItemsControl itemsControl)
        {
            (itemsControl.Items as IList)?.Clear();
        }

        public static void RegisterControl<TControl>(this Control parent, out TControl control, string name)
            where TControl : Control
        {
            control = parent.FindControl<TControl>(name);
        }

        public static async Task<string> ShowAndGetFileAsync(this OpenFileDialog dialog, Window window = null)
        {
            var result = await dialog.ShowAsync(window);
            return result?.FirstOrDefault();
        }
    }
}
