using System;

namespace Infrastucture
{
    public class StandardDateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrent() => DateTime.Now;
    }
}