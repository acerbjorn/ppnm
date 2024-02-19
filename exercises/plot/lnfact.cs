using static sfuns;
using static System.Math;
using static System.Console;

class main {
    static void Main() {
            for (int x = 1; x<=30 ; x++) {
                WriteLine($"{x}\t{lnfact(x-1)}");
        };
    }
}
