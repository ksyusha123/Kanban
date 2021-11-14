using System;

namespace Infrastucture
{
    public interface IDateTimeProvider
    {
        public DateTime GetCurrent();
    }
}