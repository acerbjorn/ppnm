using static matrix;
using static EVD;
using static hamiltonian;

class evd_test {
    static void Main(string[] args) {
        int max_dim = 10;
        int tests = 1;
        int seed = 1;
        foreach(string arg in args) {
            string[] words = arg.Split(":");
            if (words[0] == "-tests") tests = (int)double.Parse(words[1]);
            if (words[0] == "-seed") seed = (int)double.Parse(words[1]);
            if (words[0] == "-max_dim") max_dim = (int)double.Parse(words[1]);
        }
        decomp_tests(tests, max_dim, seed);
        hamiltonian_test(tests, max_dim, seed);
    }
    static void decomp_tests(int tests, int max_dim, int seed) {
        var rnd = new System.Random(seed);
        int n;
        int failed_VTAVeqD = 0;
        int failed_VDVTeqA = 0;
        int failed_VTVeqI = 0;
        int failed_VVTeqI = 0;
        System.Console.WriteLine($"Testing decomposition of {tests} n×n symmetric matrices with 2<=n<={max_dim}");
        for (int test_num=0; test_num<tests; test_num++) {
            n = rnd.Next(2,max_dim);
        
            matrix A_p = matrix.rnd(n,n, rng:rnd);
            matrix A = A_p.T + A_p;
            EVD eigs = new EVD(A, convergence_limit:0);
            matrix V = eigs.V;
            vector w = eigs.w;
            matrix I = matrix.id(n);
            matrix D = new matrix(w);

            matrix VTAV = V.T*A*V;
            if (!VTAV.approx(D)) {
                failed_VTAVeqD += 1;
                System.Console.Write("V'AV != D\n");
                VTAV.print(intro: "V'AV:", row_sep: "\t");
                System.Console.Write("\n\n");
            }
            
            matrix VDVT = V*D*V.T;
            if (!VDVT.approx(A)) {
                failed_VDVTeqA += 1;
                System.Console.Write("VDV' != A\n");
                VTAV.print(intro: "VDV':", row_sep: "\t");
                System.Console.Write("\n\n");
            }
            
            matrix VTV = V.T*V;
            if (!VTV.approx(I)) {
                failed_VTVeqI += 1;
                System.Console.Write("V'V != I\n");
                VTV.print(intro: "V'V:", row_sep: "\t");
                System.Console.Write("\n\n");
            }
            
            matrix VVT = V*V.T;
            if (!VVT.approx(I)) {
                failed_VVTeqI += 1;
                System.Console.Write("VV' != I\n");
                VVT.print(intro: "VV':", row_sep: "\t");
                System.Console.Write("\n\n");
            }
        }
        System.Console.Write(
            "Finished tests:\n" +
            $"\tFor {tests-failed_VTAVeqD}/{tests} V'AV = D\n"+
            $"\tFor {tests-failed_VDVTeqA}/{tests} VDV' = A\n" +
            $"\tFor {tests-failed_VTVeqI}/{tests} V'V = I \n" +
            $"\tFor {tests-failed_VVTeqI}/{tests} VV' = I\n" 
        );
    }
    static void hamiltonian_test(int tests, int max_length, int seed) {
        var rnd = new System.Random(seed);
        int n;
        double a, b, x_min, x_max;
        int failed_diff = 0;
        System.Console.WriteLine($"Testing second-order point differentiation of {tests} parabolas of length n with 3<=n<={max_length} and spanning x_min, x_max € [-10,10]");
        for (int test_num=0; test_num<tests; test_num++) {
            n = rnd.Next(3,max_length);
            a = rnd.NextDouble();
            b = rnd.NextDouble();
            x_min = 20*rnd.NextDouble()-10;
            x_max = 20*rnd.NextDouble()-10;
            if (x_min>x_max) {
                (x_min, x_max) = (x_max, x_min);
            }
            vector rs = vector.linspace(x_min, x_max, n) ; 
            vector f = rs.map(
                (double x) => a*x*x + b*x
            );
            vector dfdx = rs.map(
                (double x) => a
            );
            matrix D = construct_hamiltonian(rs, (double r) => 0);
            // D.print();
            vector Df = -D*f;

            if (!dfdx.slice(1,n-1).approx(Df.slice(1,n-1))) {
                failed_diff += 1;
                System.Console.Write("dfdx != Df\n");
                dfdx.print("dfdx: ");
                Df.print("Df: ");
                Df.slice(1,n-1).print("Df_inner:");
                System.Console.Write("\n\n");
            }
            
        }
        System.Console.Write(
            "Finished tests:\n" +
            $"\tFor {tests-failed_diff}/{tests} Df =~ dfdx\n" 
        );
    }}
