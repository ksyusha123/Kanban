using System;

namespace Infrastructure
{
    public class StandardDateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrent() => DateTime.Now;
    }
}