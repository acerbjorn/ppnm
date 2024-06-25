using System;
using System.Collections.Generic;
using static System.Math;
using static matrix; 
using static root_finder;

class min_test {
    static void Main() {
        vector hb_start = new vector(new double[] {0,0});
        Console.WriteLine($"Testing minimization of the rosenbrock function, starting guess {hb_start}");
        Console.WriteLine("\tWe expect minima at (1,1).");
        vector rosenbrock_min = root_finder.newton(rosenbrock_gradient, hb_start );
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
            vector himmelblau_min = root_finder.newton(himmelblau_gradient, start );
            foreach (vector found_point in found_points) {
                // Console.WriteLine($"Testing against {found_point}");
                if ((himmelblau_min - found_point).norm() < 2e-2) not_found = false ; // assume points are further than 1e-2 from each other
            }
            if (not_found) {
                Console.WriteLine($"\tFound extrema at {himmelblau_min}, f(x) = {himmelblau(himmelblau_min):F3}");
                found_points.Add(himmelblau_min);
            }
            not_found = true;
        }
        
    }
    static vector himmelblau_gradient(vector input) {
        double x = input[0], y = input[1];
        return new vector(new double[] {
            4*x*(x*x + y - 11) + 2*(x + y*y -7),
            2*(x*x + y - 11) + 4*y*(x + y*y -7),
        });
    }
    static double himmelblau(vector input) {
        double x = input[0], y = input[1];
        return Pow(x*x + y - 11, 2) + Pow(x + y*y -7, 2);
    }
    static vector rosenbrock_gradient(vector input) {
        double x = input[0], y = input[1];
        return new vector(new double[] {
            2*x - 2 + 400*x*x*x - 400*y*x,
            200*y - 200*x*x
        });
    }
}
