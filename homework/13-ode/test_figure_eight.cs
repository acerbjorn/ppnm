using static matrix;
using static rk12;
using System;
using static System.Math;

class test_figure_eight{
    static void Main() {
        vector S = new vector(12);
        // x'_i
        S[4] = -0.93240737; S[5] = -0.86473146;
        S[0] = -S[4]/2; S[1] = -S[5]/2;
        S[2] = S[4]/2; S[3] = S[5]/2;

        // x_i
        S[6] = 0.97000436; S[7] = -0.24308753;
        S[8] = -S[6]; S[9] = -S[7];
        print_orbit(S, 6.5);
    }
    static void print_orbit(vector u_0, double t_max) {
        var F = rk12.make_ode_ivp_interpolant(
            f_gravity, // driving function
            (0.0, t_max), // interval
            u_0, // initial value
            acc: 1,
            eps: 1      
        );
        Console.WriteLine("#x1\ty1\tx2\ty2\tx3\ty3");
        // foreach (vector z in zs) {
        vector ts = vector.linspace(0,6.5,200);
        foreach (double t in ts) {
            vector z = F(t);
            for (int i=6; i<12; i++) {
                Console.Write($"{z[i]}\t");         
            }
            Console.Write("\n");
        }
    }
    static vector f_gravity(double t, vector y) {
        // Console.Error.Write($"{t:G3}\n");
        int bodies = 3;
        vector ans = new vector(4*bodies);
        for (int i = 0; i< bodies*2; i++) {
            ans[i] = y[i+2*bodies];
        }
        for (int b_i = 0; b_i < bodies; b_i++) {
            for (int b_j = b_i+1; b_j<bodies; b_j++) {
                double x1 = y[2*bodies+2*b_i], y1 = y[2*bodies+2*b_i+1]; 
                double x2 = y[2*bodies+2*b_j], y2 = y[2*bodies+2*b_j+1]; 
                var F_Gij = F_G(x1,y1,x2,y2);
                ans[2*bodies+2*b_i]   += F_Gij.Item1;
                ans[2*bodies+2*b_i+1] += F_Gij.Item2;
                ans[2*bodies+2*b_j]   -= F_Gij.Item1;
                ans[2*bodies+2*b_j+1] -= F_Gij.Item2;
            }
        }
        return ans;
    }
    static (double, double) F_G(
        double x1, double y1,
        double x2, double y2
    ) {
        double dx = x2-x1, dy = y2-y1;
        double dist = Pow(dx*dx+dy*dy, 2.0/3.0);
        return (dx/dist, dy/dist);
    }
}
