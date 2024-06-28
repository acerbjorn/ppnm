using System;
using static System.Math;

class integral_erf {
    static double Pi = 3.14159265359;
    static void Main() {
        int entries = 100;
        vector zs = vector.linspace(-3,3,entries);
        vector erfs1 = zs.map(erf);
        vector erfs2 = zs.map(erf_simple);
        vector erfs3 = zs.map(sfuns.erf);
        for(int i=0; i<entries; i++) {
            Console.WriteLine($"{zs[i]}\t{erfs1[i]}\t{erfs2[i]}\t{erfs3[i]}");
        }
    }
    public static double erf(double z) {
        if (z<0) return -erf(-z);
        else if (z<1) return integrate.adaptive(
            (double x) => 2/Sqrt(Pi)*Exp(-x*x),
            0, z,
            h_min: 1e-7
        );
        else return 1-2/Sqrt(Pi)*integrate.adaptive(
            (double t) => Exp(-Pow((z+(1-t)/t),2))/t/t,
            0,1,
            h_min: 1e-7
        );
    }
    public static double erf_simple(double z) {
        return integrate.adaptive(
            (double x) => 2/Sqrt(Pi)*Exp(-x*x),
            0, z,
            h_min: 1e-7
        );
    }
}
