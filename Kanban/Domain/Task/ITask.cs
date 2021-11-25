﻿using System;
using Infrastructure;

namespace Domain
{
    public interface ITask : IEntity
    {
        string Name { get; set; }
        Executor? Executor { get; set; }
        string Description { get; set; }
        State State { get; set; }
        DateTime CreationTime { get; }
    }
}