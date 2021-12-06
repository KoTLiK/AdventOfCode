namespace AdventOfCode.Levels;

public class ResultCollector<T> : IResultCollector<T>
{
    private T _result = default!;

    public T Collect(T result) => _result = result;

    public T Retrieve() => _result;
}