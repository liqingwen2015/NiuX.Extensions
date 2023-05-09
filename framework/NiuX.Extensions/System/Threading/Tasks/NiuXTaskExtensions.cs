namespace System.Threading.Tasks;

/// <summary>
/// Task Extensions
/// </summary>
public static class NiuXTaskExtensions
{
    /// <summary>
    /// Converts to taskresult.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns></returns>
    public static Task<TResult> ToTaskResult<TResult>(this TResult result)
    {
        return Task.FromResult(result);
    }
}