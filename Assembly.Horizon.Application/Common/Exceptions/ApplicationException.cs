﻿namespace Assembly.Horizon.Application.Common.Exceptions;

public abstract class ApplicationException : Exception
{
    public ApplicationException(string msg) : base(msg) { }
}
