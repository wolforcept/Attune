using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AttuneLib
{

    static class Util
    {

        public static void MoveMouse(int dx, int dy)
        {
            var cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X + dx, Cursor.Position.Y + dy);
        }
    }
}

//
// EXTENSION METHODS
//

namespace AttuneLib
{
    public static class EventHandlerExtensions
    {
        public static void SafeInvoke(this EventHandler handler, object sender)
        {
            if (handler != null) handler(sender, EventArgs.Empty);
        }
        public static void SafeInvoke<T>(this EventHandler<T> handler,
            object sender, T args) where T : EventArgs
        {
            if (handler != null) handler(sender, args);
        }
    }
}