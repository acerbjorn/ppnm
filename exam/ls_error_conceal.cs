using System;
using System.Collections.Generic;
using static System.Math;

public class lsec {
    public vector x;
    
    public lsec(vector y, Func<double,bool> error_condition) {
        List<(int,int)> error_loc = new List<(int,int)>();
        int errors = 0;
        int n = y.size;
        for (int i=0; i<n; i++) {
            if (error_condition(y[i])) {
                error_loc.Add((i,errors));
                errors++;
            }
        }
        matrix D = second_order_difference(n);
        matrix M = new matrix(n,errors);
        foreach (var error in error_loc) {
            var (i, j) = error; // Can C# not destruct tuples in foreach?
            M[i,j] = 1;
        }
        matrix DM = D*M; vector nDy = -D*y;
        QRGS QRDM = new QRGS(DM);
        vector z = QRDM.solve(nDy);
        x = y + M*z;
    }
    public lsec(signal_splitter yl, Func<double,bool> error_condition) {
        x = yl.original.copy();
        int chunk_start = 0;
        foreach (signal_chunk yi in yl.chunks) {
            if (yi.errored) {
                lsec chunk_lsec = new lsec(yi,error_condition);
                for (int i = chunk_start; i<chunk_start+yi.size; i++) x[i] = chunk_lsec.x[i-chunk_start];
            } 
            chunk_start += yi.size;
        }
    }
    public static matrix second_order_difference(int n) {
        // Contrary to the eigenvalues second order difference, 
        // we don't want to assume zero at the ends
        matrix D = new matrix(n);
        D[0,0] = 1; D[0,1] = -2; D[0,2] = 1;
        for(int i = 1; i<n-1; i++) {
            D[i,i-1] = 1; D[i,i] = -2; D[i,i+1] = 1;
        }
        D[n-1,n-3] = 1; D[n-1,n-2] = -2; D[n-1,n-1] = 1;
        return D;
    }
}

public class signal_chunk: vector {
    public bool errored;
    public static signal_chunk good(double[] y_chunk) {
        return new signal_chunk(y_chunk, false);
    }
    public static signal_chunk bad(double[] y_chunk) {
        return new signal_chunk(y_chunk, true);
    }
    public signal_chunk(double[] data, bool err_state): base(data) {
        errored = err_state;
    }
}

public class signal_splitter {
    public List<signal_chunk> chunks;
    public vector original;
    public Func<double, bool> is_err;    
    public signal_splitter(vector y, Func<double, bool> error_condition) {
        original = y;
        is_err = error_condition;
        chunks = new List<signal_chunk>();
        start(0,0);
    }
    void add_good(int start, int end) {
        chunks.Add(signal_chunk.good(original.slice(start, end)));
    }
    void add_bad(int start, int end) {
        chunks.Add(signal_chunk.bad(original.slice(start, end)));
    }
    void start(int chunk_start, int i) {
        if (original.size <= i) {
            add_good(chunk_start, i);
            return;
        }; 
        if (is_err(original[i])) {
            if (i-chunk_start>2) {
                add_good(chunk_start, i-2);
                err_start(i-2, i+1);
            } else {
                err_start(chunk_start, i+1);
            }
        } else {
            start(chunk_start, i+1);
        }
    }
    void err_start(int chunk_start, int i) {
        if (original.size <= i) {
            add_bad(chunk_start, i);
            return;
        }; 
        if (is_err(original[i])) {
            err_start(chunk_start, i+1);
        } else {
            err_p1(chunk_start, i+1);
        }
    }
    void err_p1(int chunk_start, int i) {
        if (original.size <= i) {
            add_bad(chunk_start, i);
            return;
        }; 
        if (is_err(original[i])) {
            err_start(chunk_start, i+1);
        } else {
            err_p2(chunk_start, i+1);
        }
    }
    void err_p2(int chunk_start, int i) {
        if (original.size <= i) {
            add_bad(chunk_start, i);
            return;
        }; 
        if (is_err(original[i])) {
            err_start(chunk_start, i+1);
        } else {
            err_p3(chunk_start, i+1);
        }
    }
    void err_p3(int chunk_start, int i) {
        if (original.size <= i) {
            add_bad(chunk_start, i);
            return;
        }; 
        if (is_err(original[i])) {
            err_start(chunk_start, i+1);
        } else {
            add_bad(chunk_start, i-1);
            start(i-1, i+1);
        }
    }
}
