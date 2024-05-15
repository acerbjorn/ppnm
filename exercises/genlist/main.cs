using System;
using static System.Console;

using static genlist<double[]>;

class genlisttest {
    static void Main() {
        var list = new genlist<double[]>();
        char[] delims = {' ', '\t'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        for(string line = ReadLine(); line!=null; line = ReadLine()){
        	var words = line.Split(delims,options);
        	int n = words.Length;
        	var numbers = new double[n];
        	for(int i=0;i<n;i++) numbers[i] = double.Parse(words[i]);
        	list.add(numbers);
       	}
        for(int i=0;i<list.size;i++){
        	var numbers = list[i];
        	foreach(var number in numbers)Write($"{number : 0.00e+00;-0.00e+00} ");
        	WriteLine();
        }
        Write("\n\n Let's attempt to remove a line from this table.\n");
        WriteLine("Here's the list without the third entry.(rem(2))");
        list.rem(2);
        for(int i=0;i<list.size;i++){
        	var numbers = list[i];
        	foreach(var number in numbers)Write($"{number : 0.00e+00;-0.00e+00} ");
        	WriteLine();
        } 

        
        Write("\n\nNow we'll attempt to pop the last element and print it alone.\n");
        var popped_numbers = list.pop();
        Write($"{popped_numbers.Length}");
        foreach(var number in popped_numbers) Write($"{number : 0.00e+00;-0.00e+00} ");
        WriteLine("\nAnd here's the remaining list");
        for(int i=0;i<list.size;i++){
        	var numbers = list[i];
        	foreach(var number in numbers)Write($"{number : 0.00e+00;-0.00e+00} ");
        	WriteLine();
        } 
    }
}
