using static matrix;
using static QRGS;

class time {
    static int Main(string[] args){
        string operation = args[0];
        int n = (int)double.Parse(args[1]);
        if (operation=="decomp") return decomp(n);
        if (operation=="abs_det") return abs_det(n);
        if (operation=="solve") return solve(n);
        if (operation=="inverse") return inverse(n);
        System.Console.Write("No recognized option provided (decomp, abs_det, solve, inverse)");
        return 1;
    }
    static int decomp(int n){
        matrix A = matrix.rnd(n,n);
        QRGS QR = new QRGS(A);
        return 0;
    }
    static int abs_det(int n){
        matrix A = matrix.rnd(n,n);
        QRGS QR = new QRGS(A);
        double det = QR.abs_det();
        return 0;
    }
    static int solve(int n){
        matrix A = matrix.rnd(n,n);
        vector b = vector.rnd(n);
        QRGS QR = new QRGS(A);
        vector x = QR.solve(b);
        return 0;
    }
    static int inverse(int n){
        matrix A = matrix.rnd(n,n);
        QRGS QR = new QRGS(A);
        matrix B = QR.inverse();
        return 0;
    }
}
