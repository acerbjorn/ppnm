using static vec;
using static System.Console;
using static System.Math;
using static test_collection;

class main {
    static int Main() {
        test_collection tests = new test_collection();
        double EPS = Pow(2,-52);
        vec eps_x = new vec(EPS, 0, 0);
        vec eps_y = new vec(0, EPS, 0);
        vec eps_z = new vec(0, 0, EPS);
        vec ones = new vec(1,1,1);
        vec twos = new vec(2,2,2);
        vec one_x = new vec(1,0,0);
        vec neg_ones = new vec(-1,-1,-1);
        tests.assert_eq(
            (2*ones),(twos),
            "multiplication"
        );
        tests.assert_eq(
            (ones*2),(twos),
            "rh multiplication"
        );
        tests.assert_eq(
            ones,twos/2,
            "division"
        );
        tests.assert_eq(
            ones+ones,twos,
            "addition"
        );
        tests.assert_eq(
            ones,twos-ones,
            "subtraction"
        );
        tests.assert_eq(
            -ones,neg_ones,
            "negation"
        );

        ones.print("ones="); 
        ones.print();
        
        tests.assert(
            ones.dot(ones)==3,
            "dot product"
        );
        tests.assert(
            (ones+eps_x).approx(ones) && 
            (ones+eps_y).approx(ones) && 
            (ones+eps_z).approx(ones), 
            "approx()"
        );
        return tests.fin();
    }
}
