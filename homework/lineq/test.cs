using static matrix;
using static QRGS;

class qrgs_test {
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
        solve_tests(tests, max_dim, seed);
        invert_tests(tests, max_dim, seed);
    }
    static void decomp_tests(int tests, int max_dim, int seed) {
        var rnd = new System.Random(seed);
        int n, m;
        int failed_R_triang= 0;
        int failed_QTQ = 0;
        int failed_QReqA = 0;
        System.Console.WriteLine($"Testing decomposition of {tests} n×m matrices with 2<=m<=n<={max_dim}");
        for (int test_num=0; test_num<tests; test_num++) {
            do {
                m = rnd.Next(2,max_dim);
                n = rnd.Next(2,max_dim);
            } while (m>n);
        
            matrix A = matrix.rnd(n,m, rng:rnd);
            QRGS QR = new QRGS(A);
            
            if (!QR.R.is_upper_triangular()) {
                failed_R_triang += 1;
                System.Console.Write("R is not upper triangular\n");
                QR.print();
                System.Console.Write("\n\n");
            }
            
            matrix ident_m = matrix.id(m);
            matrix QTQ = QR.Q.T*QR.Q;
            if (!QTQ.approx(ident_m)) {
                failed_QTQ += 1;
                System.Console.Write("Q'Q != I\n");
                QTQ.print(intro: "Q'Q:", row_sep: "\t");
                System.Console.Write("\n\n");
            }
            
            matrix A_app = QR.Q*QR.R;
            if (!A.approx(A_app)) {
                failed_QReqA += 1;
                System.Console.Write("QR != A \n");
                QR.print();
                System.Console.Write("\n\n");
                A_app.print(intro: "QR:", row_sep: "\t");
                System.Console.Write("\n\n");
                A.print(intro: "A:", row_sep: "\t");
                System.Console.Write("\n\n");
            }
        }
        System.Console.Write(
            "Finished tests:\n" +
            $"\tFor {tests-failed_R_triang}/{tests} R was upper triangular\n" +
            $"\tFor {tests-failed_QTQ}/{tests} Q'Q = I\n" +
            $"\tFor {tests-failed_QReqA}/{tests} QR = A\n" 
        );
    }
    static void solve_tests(int tests, int max_dim, int seed){
        var rnd = new System.Random(seed);
        int n;
        int failed_solves = 0;
        System.Console.WriteLine($"Testing solving of {tests} n×n matrices with 2<=n<={max_dim}");
        for (int test_num=0; test_num<tests; test_num++) {
            n = rnd.Next(2,max_dim);
        
            matrix A = matrix.rnd(n,n, rng:rnd);
            vector b = vector.rnd(n, rng:rnd);
            QRGS QR = new QRGS(A);
            vector x = QR.solve(b);
            vector Ax = A*x;
            if (!Ax.approx(b)) {
                failed_solves++;
                System.Console.Write($"Failed solving Ax=b using QR decomposition at n={n}.\n");
                Ax.print(intro: "\tAx:");
                b.print(intro: "\tb:");
            }
        }
        System.Console.Write(
            "Finished tests:\n" +
            $"\tFor {tests-failed_solves}/{tests} found x such that Ax=b\n" 
        );
    }
    static void invert_tests(int tests, int max_dim, int seed){
        var rnd = new System.Random(seed);
        int n;
        int failed_inverts = 0;
        System.Console.WriteLine($"Testing inversion of {tests} n×n matrices with 2<=n<={max_dim}");
        for (int test_num=0; test_num<tests; test_num++) {
            n = rnd.Next(2,max_dim);
        
            matrix A = matrix.rnd(n,n, rng:rnd);
            QRGS QR = new QRGS(A);
            matrix Ainv = QR.inverse();
            matrix AinvA = Ainv*A;
            matrix ident_n = matrix.id(n);
            if (!AinvA.approx(ident_n)) {
                failed_inverts++;
                System.Console.Write($"Failed solving A⁻¹A=I using QR decomposition at n={n}.\n");
                AinvA.print(intro: "\tA⁻¹A=I:");
            }
        }
        System.Console.Write(
            "Finished tests:\n" +
            $"\tFor {tests-failed_inverts}/{tests} found A⁻¹ such that A⁻¹A=I\n" 
        );
    }
}
