using System.Linq;
public class sum_chunk { public int a,b; public double sum; }

class main {
    public static int Main(string[] args) {
        int nterms=(int)1e7, nthreads=1;
        bool verbose = false;
        string sum_method = "thread";
        foreach(string arg in args){
            var words = arg.Split(":");
            if (words[0]=="-nterms") nterms = (int)double.Parse(words[1]);
            if (words[0]=="-nthreads") nthreads = (int)double.Parse(words[1]);
            if (words[0]=="-v") verbose = true; 
            if (words[0]=="-method") sum_method = words[1]; 
        }
    
        if (verbose) {
            System.Console.Write($"Main: nterms={nterms} nthreads={nthreads}\n");
            System.Console.WriteLine($"\tCalculation Method: {sum_method}");
        }
        switch (sum_method) {
            case "thread":
                return run_harm_on_threads(nterms, nthreads, verbose);
            case "parfor":
                return run_harm_parfor(nterms, verbose);
            case "linq":
                return run_harm_linq(nterms, verbose);
            default:
                return 1;
        }
    }
    
    public static int run_harm_on_threads(int nterms, int nthreads, bool verbose) {
        sum_chunk[] data = new sum_chunk[nthreads];
        int chunk_length = nterms/nthreads;
        for(int i=0;i<nthreads;i++){
        	data[i] = new sum_chunk();
        	data[i].a=i*chunk_length+1;
        	data[i].b=data[i].a+chunk_length;
        	}
        data[nthreads-1].b=nterms;
        var threads = new System.Threading.Thread[nthreads];
        if (verbose) System.Console.Write($"Main: starting threads...\n");
        for(int i=0;i<nthreads;i++){
        	threads[i] = new  System.Threading.Thread(harm);
        	threads[i].Start(data[i]);
        	}
        if (verbose) System.Console.Write($"Main: waiting for threads to finish...\n");
        foreach(var thread in threads)thread.Join();
        double total=0;
        foreach(sum_chunk datum in data)total+=datum.sum;
        if (verbose) System.Console.Write($"Main: total sum = ");
        System.Console.Write($"{total}");
        System.Console.Write("\n");               
        return 0;
    }

    public static int run_harm_parfor(int N, bool verbose) {
        double sum=0;
        System.Threading.Tasks.Parallel.For( 1, N+1, (int i) => sum+=1.0/i );
        if (verbose) System.Console.Write($"Main: total sum = ");
        System.Console.Write($"{sum}\n");
        return 0;
    }
    
    public static int run_harm_linq(int N, bool verbose) {
        var sum = new System.Threading.ThreadLocal<double>( ()=>0, trackAllValues:true);
        System.Threading.Tasks.Parallel.For( 1, N+1, (int i)=>sum.Value+=1.0/i );
        double totalsum=sum.Values.Sum();
        if (verbose) System.Console.Write($"Main: total sum = ");
        System.Console.Write($"{totalsum}\n");
        return 0;
    }
    
    static void harm(object obj) {
        sum_chunk d = (sum_chunk)obj;
        d.sum=0;
        for(int i=d.a; i<=d.b; i++)d.sum+=1.0/i;
    }
}
