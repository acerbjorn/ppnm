using static vec;
using System;
using static System.Console;
using static System.Math;
using static test_collection<(vec,vec)>;

class vec_tests {
    static void Main(string[] args) {
        int test_count = 1;
        int seed = 1;
        foreach(string arg in args) {
            string[] words = arg.Split(":");
            if (words[0] == "-test_count") test_count = (int)double.Parse(words[1]);
            if (words[0] == "-seed") seed = (int)double.Parse(words[1]);
        }
        run_tests(test_count, seed);
    }
    
    static void run_tests(int test_count, int seed) {
        var rnd = new System.Random(seed);
        test_collection<(vec,vec)> tests = new test_collection<(vec,vec)>();

        tests.add_eq<double>(
            vs => vs.Item2.dot(vs.Item1),
            vs => vs.Item1.dot(vs.Item2),
            "dot product commutes"
        );
        
        tests.add_eq<vec>(
            vs => vs.Item1 + vs.Item2,
            vs => vs.Item2 + vs.Item1,
            "addition commutes"
        );
        
        tests.add_eq<vec>(
            vs => vs.Item1 - vs.Item2,
            vs => -(vs.Item2 - vs.Item1),
            "subtraction works"
        );
        
        tests.add(new Approx(
            vs => vs.Item1,
            vs => vs.Item1+1e-10*(vs.Item2),
            "approximation works"
        ));
        
        tests.add(new Approx(
            vs => vs.Item1*2,
            vs => vs.Item1+vs.Item1,
            "doubling works"
        ));

        tests.add(new Approx(
            vs => vs.Item1/2 + vs.Item1/2,
            vs => vs.Item1,
            "halving works"
        ));

        tests.add(new ApproxDouble(
            vs => vs.Item1.dot(vs.Item2 - (vs.Item1.dot(vs.Item2))*vs.Item1/vs.Item1.squared()),
            vs => 0.0,
            "orthogonalization works"
        ));


        for (int i = 0; i<test_count; i++) {
            vec v = new vec(num(rnd), num(rnd), num(rnd));
            vec u = new vec(num(rnd), num(rnd), num(rnd));
            tests.try_case((v,u));            
        }
        tests.fin();
    }
    static double num(Random rnd) {
        return 2*rnd.NextDouble()-1.0;
    }
}



class Approx: ITestable<(vec,vec)> {
    public String description;
    Func<(vec,vec),vec> lhs;
    Func<(vec,vec),vec> rhs;

    public Approx(
        Func<(vec,vec),vec> lhs_i, 
        Func<(vec,vec),vec> rhs_i, 
        String new_description
    ) {
        lhs = lhs_i;
        rhs = rhs_i;
        description = new_description;
    }
    
    public bool test((vec,vec) testcase) {
        bool assertion = lhs(testcase).approx(rhs(testcase));
        if (!assertion) {
            Console.Write($"Assertion that {description} failed for case: \n{testcase}\nlhs: {lhs(testcase)}\nrhs: {rhs(testcase)}\n\n");
            return false;
        } else {
            return true;
        }
    }
    public String describe() {
        return description;
    }
}

class ApproxDouble: ITestable<(vec,vec)> {
    public String description;
    Func<(vec,vec),double> lhs;
    Func<(vec,vec),double> rhs;

    public ApproxDouble(
        Func<(vec,vec),double> lhs_i, 
        Func<(vec,vec),double> rhs_i, 
        String new_description
    ) {
        lhs = lhs_i;
        rhs = rhs_i;
        description = new_description;
    }
    
    public bool test((vec,vec) testcase) {
        bool assertion = vec.approx(lhs(testcase),rhs(testcase));
        if (!assertion) {
            Console.Write($"Assertion that {description} failed for case: \n{testcase}\nlhs: {lhs(testcase)}\nrhs: {rhs(testcase)}\n\n");
            return false;
        } else {
            return true;
        }
    }
    public String describe() {
        return description;
    }
}

