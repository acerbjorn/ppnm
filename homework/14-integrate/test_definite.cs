using System;
using static System.Math;

class definite_integral_tests {
    static void Main() {
        Console.WriteLine("Testing numeric integration of well known integrals.");
        
        Console.WriteLine( "∫_0^1 dx √(x) = 2/3 ,");
        double I = integrate.adaptive(Sqrt,0,1);
        Console.WriteLine($"I_num         = {I}\n");
        
        Console.WriteLine( "∫_0^1 dx 1/√(x) = 2");
        I = integrate.adaptive((double x) => 1/Sqrt(x), 0,1);
        Console.WriteLine($"I_num           = {I}\n");
        
        Console.WriteLine( "∫_0^1 dx 4√(1-x²) = π");
        I = integrate.adaptive((double x) => 4*Sqrt(1-x*x), 0,1);
        Console.WriteLine($"I_num             = {I}\n");
        
        Console.WriteLine( "∫_0^1 dx ln(x)/√(x) = -4");
        I = integrate.adaptive((double x) => Log(x)/Sqrt(x), 0,1);
        Console.WriteLine($"I_num               = {I}\n");
    }
}
