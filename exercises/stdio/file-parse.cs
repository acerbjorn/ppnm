using static System.Console;
using static System.Math;
class main {
    static int Main(string[] args) {
        string infile=null,outfile=null;
        foreach(var arg in args){
        	var words=arg.Split(':');
        	if(words[0]=="-input")infile=words[1];
        	if(words[0]=="-output")outfile=words[1];
        	}
        if( infile==null || outfile==null) {
        	Error.WriteLine(
                $"Missing file argument\n input: {infile}\noutput: {outfile}"
                );
        	return 1;
        	}
        // Could be rewritten to using statements
        var instream =new System.IO.StreamReader(infile);
        var outstream=new System.IO.StreamWriter(outfile,append:false);
		outstream.WriteLine("x\tSin(x)\tCos(x)");
        for(string line=instream.ReadLine();line!=null;line=instream.ReadLine()){
        	double x=double.Parse(line);
				outstream.WriteLine($"{x:N4}\t{Sin(x):N4}\t{Cos(x):N4}");
                }
        instream.Close();
        outstream.Close();

        return 0;
    }
}
