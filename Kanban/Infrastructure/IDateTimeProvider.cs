using System;

namespace Infrastructure
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrent();
    }
}