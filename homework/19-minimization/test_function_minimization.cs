using System;
using System.Collections.Generic;
using static System.Math;
using static matrix; 
using static min;

class min_test {
    static void Main() {
        vector parabolic_start = new vector(new double[] {13, 20, 16, 0, 19, 6});
        Console.WriteLine($"Testing minimization of a parabolic function f(x) = |x|², starting guess {parabolic_start}");
        Console.WriteLine("\tWe expect minima at (0).");
        vector parabolic_min= min.newton(parabolic, parabolic_start, max_iter:100000);
        Console.WriteLine($"\tFound minima at {parabolic_min}");
        
        
        vector hb_start = new vector(new double[] {0,0});
        Console.WriteLine($"Testing minimization of the rosenbrock function, starting guess {hb_start}");
        Console.WriteLine("\tWe expect minima at (1,1).");
        vector rosenbrock_min = min.newton(rosenbrock, hb_start );
        Console.WriteLine($"\tFound minima at {rosenbrock_min}");
        
        int iterations = 100;
        Console.WriteLine($"Testing minimization of the himmelblau function, {iterations} randomized starting points in [-6,6]x[-6,6]");
        Console.WriteLine("\tWe expect four distinct minima.");
        var random_ng = new Random(1); 
        List<vector> found_points = new List<vector>();
        bool not_found = true;
        for (int i = 0; i < iterations; i++) {
            // Console.WriteLine($"i={i}");
            vector start = vector.rnd(2, min:-6, max:6, rng:random_ng);
            vector himmelblau_min = min.newton(himmelblau, start);
            foreach (vector found_point in found_points) {
                // Console.WriteLine($"Testing against {found_point}");
                if ((himmelblau_min - found_point).norm() < 2e-3) not_found = false ; // assume points are further than 1e-2 from each other
            }
            if (not_found) {
                Console.WriteLine($"\tFound extrema at {himmelblau_min}, f(x) = {himmelblau(himmelblau_min):F3}");
                found_points.Add(himmelblau_min);
            }
            not_found = true;
        }
        
    }
    static double himmelblau(vector input) {
        double x = input[0], y = input[1];
        return Pow(x*x + y - 11, 2) + Pow(x + y*y -7, 2);
    }
    static double rosenbrock(vector input) {
        double x = input[0], y = input[1];
        return Pow(1-x, 2) + 100*Pow(y-x*x, 2);
    }
    static double parabolic(vector input) {
        return Pow(input.norm(),2);
    }
}
