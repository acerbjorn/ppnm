using static matrix;
using static rk12;
using System;

class damped_spring {
    static double b = 0.25;
    static double c = 5.0;
    static void Main() {
        print_orbit(
            new vector("1.0,0"),
            0
        );
        Console.Write("\n\n");
        
        print_orbit(
            new vector("1.0,-0.5"),
            0
        );
        Console.Write("\n\n");
        
        print_orbit(
            new vector("1.0,-0.5"),
            0.01
        );
        Console.Write("\n\n");
    }
    static void print_orbit(vector u_0, double eps) {
    
        double phi_max = 10.0;
        var F = rk12.make_ode_ivp_interpolant(
            (double x, vector y) => f_eq(x,y,eps), // driving function
            (0.0, 10.0), // interval
            u_0 // initial value
        );
        Console.WriteLine("#x\ty");
        for (double phi = 0; phi<phi_max; phi+=1.0/64) {
            Console.WriteLine($"{phi}\t{F(phi)[0]}");
        }
    }
    static vector f_eq(double x, vector y, double eps) {
        vector ans = new vector(2);
        ans[0] = y[1]; ans[1] = 1-y[0]+eps*y[0]*y[0];
        return ans;
    }
}
