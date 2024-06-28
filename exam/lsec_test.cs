using System;
using static System.Math;

class lsec_test{
    public static void Main() {
        vector x = vector.linspace(-10,10,100);
        test(x,(double xi) => Cos(xi)+Sin(2*xi),0.05);
        x = vector.linspace(-PI,PI,20);
        Console.Write($"\n\n");
        test(x,(double xi) => Cos(5*xi)*Exp(-xi*xi), 0.1);
        x = vector.linspace(-PI,PI,40);
        Console.Write($"\n\n");
        test(x,(double xi) => Cos(5*xi)*Exp(-xi*xi), 0.1);
    }
    public static void test(vector x, Func<double,double> f, double error_rate, int seed=1) {
        Random rng = new Random(seed);
        vector y_orig = x.map(f);
        vector y_copy = deteriorate(y_orig, error_rate, 0, rng);
        lsec sys_conc = new lsec(
            y_copy,
            (double yi) => (yi==0)            
        );
        vector y_conc = sys_conc.x;
        Console.WriteLine("x\tOriginal\tDeteriorated\tReconstructed");
        for (int i=0; i<x.size; i++) Console.WriteLine($"{x[i]:G6}\t{y_orig[i]:G6}\t{y_copy[i]:G6}\t{y_conc[i]:G6}");
    }
    public static vector deteriorate(
        vector original, 
        double error_rate, 
        double error_substitute = 0,
        Random rng=null
    ) {
        if (rng==null) {
            rng = new Random();
        }
        vector copy = original.copy();
        for (int i = 0; i<copy.size; i++) {
            if (rng.NextDouble()<error_rate) copy[i] = error_substitute;
        } 
        return copy;
    }
}
