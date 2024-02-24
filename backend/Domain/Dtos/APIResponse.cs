namespace Domain.Dtos;

public class APIResponse
{
    public bool Result { get; set; }
    public string? Msg { get; set; }
    public object? Data { get; set; }
}

public class APIResponse<T>
{
    public bool Result { get; set; }
    public string? Msg { get; set; }
    public T? Data { get; set; }
}
