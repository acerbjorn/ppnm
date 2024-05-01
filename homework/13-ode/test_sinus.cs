using static matrix;
using static rk12;
using System;


class sinus_ode {
    static void Main() {
        var (xs, ys) = rk12.driver(
            f_sinus, // driving function 
            (0.0, 10.0), // interval
            new vector("1,0") // initial value
        );
        int n = xs.Count;
        Console.WriteLine("#x\ty");
        for (int i = 0; i<n; i++) {
            Console.WriteLine($"{xs[i]}\t{ys[i][0]}");
        }
    }
    static vector f_sinus(double x, vector y) {
        vector ans = new vector(2);
        ans[0] = y[1]; ans[1] = -y[0];
        return ans;
    }
}
