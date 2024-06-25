using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;
using static matrix; 
using static root_finder;
using static rk12;

class wavefunction {
    static void Main() {
        Console.WriteLine($"#r_min\tE");
        double x_min = 0.01;
        double x_max = 1;
        vector E_start = new vector("-2"); // Set energy very low to find lowest state first.
        foreach (double x in vector.linspace(x_min, x_max, 100)) {
            vector found_E = root_finder.newton(
                (vector E) => M_E(E, r_min: x),
                E_start
            );
            Console.WriteLine($"{x}\t{found_E[0]}");
        }
    }
    static vector M_E(
        vector E, 
        double r_min = 0.01, 
        double r_max = 10, 
        double eps = 0.01, 
        double acc = 0.01
    ) {
        // Console.WriteLine($"E={E}, r_min = {r_min}");
        vector initial_value = new vector(new double[] {
            r_min-r_min*r_min,
            1-2*r_min 
        });
       
        Func<double,vector,vector> rad_schroed_eq = (double r, vector y) => {
            return new vector(new double[] {
            y[1], // df/dr = f'
            -2*( 1/r + E[0] )*y[0] // df'/dr
            });
        };        
        var (_, densities) = rk12.driver(
            rad_schroed_eq,
            (r_min, r_max),
            initial_value,
            eps: eps,
            acc: acc
        );
        return new vector(new double[]{
            densities.Last()[0] 
        });
    }
}
