using System;

namespace lb5_1_8
{
    struct MyTime
    {
        public int hour, minute, second;

        public MyTime(int h, int m, int s)
        {
            hour = h;
            minute = m;
            second = s;
        }

        public override string ToString()
        {
            return $"{hour}:{minute:D2}:{second:D2}";
        }
    }
}
