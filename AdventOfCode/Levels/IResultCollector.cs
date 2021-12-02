namespace AdventOfCode.Levels;

public interface IResultCollector<T>
{
    T Collect(T result);

    T Retrieve();
}