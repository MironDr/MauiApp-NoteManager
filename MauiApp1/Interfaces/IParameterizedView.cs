namespace MauiApp1.Interfaces;

public interface IParameterizedView<in T>
{
    void SetData(T data);
}