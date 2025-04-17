namespace Fu.Rnd;

public static partial class Rnd
{
    public static class Distributed
    {
        
        public static double Next(double mean, double stdDev, Func<double, double, double> transform)
        {
            var transformed = double.NaN;
    
            // this loop just allows us to adapt to any transform function which 
            // may provide invalid results based on random inputs.
            while (double.IsNaN(transformed))
            {
                // generate uniform randoms and pass to the supplied distribution
                // transform.
                var uniform1 = 1.0 - _random.NextDouble();
                var uniform2 = 1.0 - _random.NextDouble();
                transformed = transform(uniform1, uniform2);
            }
    
            // scale/shift by mean/std dev
            return mean + stdDev * transformed;
        }
    

        // Box-muller transform - produces normal dist val based on 2
        // uniformly distributed randoms
        public static double BoxMuller(double u1, double u2) =>
            System.Math.Sqrt(-2.0 * System.Math.Log(u1)) * System.Math.Sin(2.0 * System.Math.PI * u2);


        // Marsaglia polar transform - actually generates 2 normals
        public static double MarsagliaPolar(double u1, double u2)
        {
            // map from [0, 1] -> [-1, 1]
            var v1 = 2.0 * u1 - 1.0;
            var v2 = 2.0 * u2 - 1.0;
    
            var s = v1 * v1 + v2 * v2;

            // reject numbers outside the unit circle
            if (s is >= 1.0 or 0)
                return double.NaN;
    
            return v1 * System.Math.Sqrt(-2.0 * System.Math.Log(s) / s);
        }
    }
}