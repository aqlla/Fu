using  Fu.Core.Seq;

namespace Fu.Rnd;

public static partial class Rnd
{
    public static class Time
    {
        public static DateTime Date(DateTime min, DateTime max) =>
            min.AddMinutes(Int((int) (max - min).TotalMinutes));
    
        public static TimeSpan Duration(TimeSpan min, TimeSpan max) => 
            min + TimeSpan.FromMinutes(Int((int) (max - min).TotalMinutes));
    
        public static TimeSpan Duration(TimeSpan max) => 
            Duration(TimeSpan.FromMinutes(1), max);
    
    
        public static TimeSpan Days(int max) => 
            Duration(TimeSpan.FromDays(max));
        
    
        
        // Incrementors
        
        public static DateTime Add(DateTime value, TimeSpan atMost, TimeSpan? atLeast = null) =>
            value + Duration(min: atLeast ?? TimeSpan.Zero, max: atMost);
    
    
        public static Func<DateTime, DateTime> Add(TimeSpan atMost, TimeSpan? atLeast = null) =>
            value => Add(value, atMost, atLeast);


        /// <summary>
        /// Creates a stateful function that generates an increasing sequence of random dates.
        /// </summary>
        /// <param name="maxStep">
        /// The maximum time span by which the date can increase.
        /// </param>
        /// <param name="start">
        /// The optional initial minimum date. Defaults to the current date and time if
        /// not specified.
        /// </param>
        /// <returns>
        /// A function that, when invoked, returns the next date in the increasing sequence.
        /// </returns>
        // public static Func<DateTime> Ascending(TimeSpan maxStep, DateTime? start = null) =>
        // Sequence.Seq.Generate(Add(atMost: maxStep), start ?? DateTime.Now);
        public static Func<DateTime> Ascending(TimeSpan maxStep, DateTime? start = null) =>
            Seq.Generate.Stateful(
                initial: start ?? DateTime.Now, 
                getNext: Add(atMost: maxStep)
            );
    }
    
}