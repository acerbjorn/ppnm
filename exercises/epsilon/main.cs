using static System.Math;
using static System.Console;
class main {
    static void Main() {
        Write("-------Part 1--------\n");
        int i=1; while(i+1>i) {i++;};
        Write("max int       = {0}\n",i);
        Write("2^31-1        = {0}\n",Pow(2,31)-1);
        Write("int.MaxValue  = {0}\n\n",int.MaxValue);

        while(i-1<i) {i--;};
        Write("min int       = {0}\n",i);
        Write("-2^31         = {0}\n",-Pow(2,31));
        Write("int.MinValue  = {0}\n",int.MinValue);
        Write("\n------Part 2--------\n");
        double x=1; while(1+x!=1){x/=2;} x*=2;
        Write("double epsilon = {0}\n",x);
        Write("2^-52          = {0}\n",Pow(2,-52));
        float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F;
        Write("float epsilon  = {0}\n",y);
        Write("2^-23          = {0}\n",Pow(2,-23));
        Write("\n------Part 3--------\n");
        Write("a=1+tiny+tiny;\n");
        Write("b=tiny+tiny+1;\n")
        double epsilon=Pow(2,-52);
        double tiny=epsilon/2;
        double a=1+tiny+tiny;
        double b=tiny+tiny+1;
        Write($"a==b => {a==b}\n");
        Write($"a>1  => {a>1}\n");
        Write($"b>1  => {b>1}\n");
        Write($"   a =  {a}\n");
        Write($"   b =  {b}\n");
        Write(@"The operations are done from the left and thus 1+tiny
is swallowed by the 1 while tiny + tiny + 1 = eps + 1.
");
        Write("\n------Part 4--------\n");
        double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
        double d2 = 8*0.1; 
        WriteLine($"d1={d1:e15}");
        WriteLine($"d2={d2:e15}");
        WriteLine($"d1==d2 => {d1==d2}"); 
        WriteLine($"d1=~d2 => {approx(d1,d2)}"); 
        
    }
    static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
        double d = Abs(a-b);
        return (d <= acc) || (d <= Max(Abs(a),Abs(b))*eps);
    }
}
