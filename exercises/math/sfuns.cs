using static System.Math;
public static class sfuns {
    public static double fgamma(double x) {
        ///single precision gamma function (formula from Wikipedia)
        if(x<0) return PI/Sin(PI*x)/fgamma(1-x); // Euler's reflection formula
        if(x<9) return fgamma(x+1)/x; // Recurrence relation
        double lnfgamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
        return Exp(lnfgamma);
    }
    public static int Fact(int x) {
        if(x<=1) return 1;
        return Fact(x-1)*x;
    }
}
