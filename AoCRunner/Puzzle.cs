using AoCRunner.Config;

namespace AoCRunner
{
    public abstract class Puzzle<TInput, TOutput>
    {
        public virtual IEnumerable<Func<TInput, TOutput>> Solutions { get; set; } = Enumerable.Empty<Func<TInput, TOutput>>();

        public abstract TInput ParseInput(IEnumerable<string> lines);

        public abstract IEnumerable<string> RetrieveInputAsync(SettingsSection settings);
    }
}
