using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPressUWP.Helpers
{
    public class SwipedEventArgs : EventArgs
    {
        public SwipeDirection Direction { get; set; }

        public SwipedEventArgs(SwipeDirection direction)
        {
            Direction = direction;
        }
    }

    public enum SwipeDirection
    {
        Left,
        Right,
        Up,
        Down
    }
}
