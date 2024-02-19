using static sfuns;
using static System.Console;

class main {
    static void Main() {
            for (double x = 1.0/8; x<=20 ; x+=1.0/8) {
                WriteLine($"{x}\t{lngamma(x)}");
        };
    }
}
