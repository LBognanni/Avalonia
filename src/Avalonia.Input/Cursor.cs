using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

#nullable enable

namespace Avalonia.Input
{
    /*
    =========================================================================================
        NOTE: Cursors are NOT disposable and are cached in platform implementation.
        To support loading custom cursors some measures about that should be taken beforehand
    =========================================================================================
    */

    public enum StandardCursorType
    {
        Arrow,
        Ibeam,
        Wait,
        Cross,
        UpArrow,
        SizeWestEast,
        SizeNorthSouth,
        SizeAll,
        No,
        Hand,
        AppStarting,
        Help,
        TopSide,
        BottomSide,
        LeftSide,
        RightSide,
        TopLeftCorner,
        TopRightCorner,
        BottomLeftCorner,
        BottomRightCorner,
        DragMove,
        DragCopy,
        DragLink,
        None,

        [Obsolete("Use BottomSide")]
        BottomSize = BottomSide

        // Not available in GTK directly, see http://www.pixelbeat.org/programming/x_cursors/ 
        // We might enable them later, preferably, by loading pixmax direclty from theme with fallback image
        // SizeNorthWestSouthEast,
        // SizeNorthEastSouthWest,
    }

    public class Cursor
    {
        public static readonly Cursor Default = new Cursor(StandardCursorType.Arrow);

        internal Cursor(ICursorImpl platformImpl)
        {
            PlatformImpl = platformImpl;
        }

        public Cursor(StandardCursorType cursorType)
            : this(GetCursorFactory().GetCursor(cursorType))
        {
        }

        public Cursor(IBitmap cursor, PixelPoint hotSpot)
            : this(GetCursorFactory().CreateCursor(cursor.PlatformImpl.Item, hotSpot))
        {
        }

        public ICursorImpl PlatformImpl { get; }

        public static Cursor Parse(string s)
        {
            return Enum.TryParse<StandardCursorType>(s, true, out var t) ?
                new Cursor(t) :
                throw new ArgumentException($"Unrecognized cursor type '{s}'.");
        }

        private static ICursorFactory GetCursorFactory()
        {
            return AvaloniaLocator.Current.GetService<ICursorFactory>() ??
                throw new Exception("Could not create Cursor: ICursorFactory not registered.");
        }
    }
}