using static sfuns;
using static System.Console;

class main {
    static void Main() {
            for (double x = -3; x<=3 ; x+=1.0/16) {
                WriteLine($"{x}\t{erf(x)}");
        };
    }
}
