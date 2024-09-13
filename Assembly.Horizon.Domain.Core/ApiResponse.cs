﻿namespace Assembly.Horizon.Domain.Core;

public class ApiResponse<T>
{
    public required bool Successed { get; init; }
    public string? Message { get; set; }
    public T? Data { get; init; }
}
