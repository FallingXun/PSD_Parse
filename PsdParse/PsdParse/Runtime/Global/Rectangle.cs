
namespace PsdParse
{
    /// <summary>
    /// 矩形边界坐标信息
    /// </summary>
    public struct Rectangle
    {
        public int Top;
        public int Left;
        public int Bottom;
        public int Right;

        public Rectangle(int top, int left ,int bottom, int right)
        {
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
        }

        public int Width
        {
            get
            {
                return Right - Left;
            }
        }

        public int Height
        {
            get
            {
                return Bottom - Top;
            }
        }
    }
}
