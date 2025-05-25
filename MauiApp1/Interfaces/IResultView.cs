namespace MauiApp1.Interfaces;

public interface IResultView<T>
{
    Task<T?> WaitForResultAsync();
}