using System;
using static qspline;
using static System.Math;
using static System.Console;
using static matrix;

class linterp_test {
    static int Main(string[] args) {
        double start = 0;
        double end = 10;
        double input_dx = 1;
        double output_dx = 1.0/8.0;
        // WriteLine($"{output_dx}");

        vector xhat = vector.linspace(start, end+1, spacing:input_dx);
        vector yhat = xhat.map(Cos);
        // vector.linspace(start, end, spacing:output_dx).print();        
        double[] x_prime = vector.linspace(start, end, spacing:output_dx).to_array();
        double[] x = xhat.to_array(); 
        double[] y = yhat.to_array(); 
        WriteLine("#x\tcos(x)");
        foreach (double x_i in x) {
            WriteLine($"{x_i}\t{Cos(x_i):N4}");
        }
        Write("\n\n");

        
        WriteLine("#x\tf_prime(x)\tF_prime(x)");
        foreach (double x_i in x_prime) {
            WriteLine($"{x_i:N4}\t{qspline.linterp(x,y,x_i):N4}\t{qspline.linteg(x,y,x_i):N4}"); 
        }
        return 0;
    }
}
