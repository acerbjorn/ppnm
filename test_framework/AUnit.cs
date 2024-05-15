using System;
using static System.Console;
using System.Collections.Generic;

public class test_collection<T> {
    public List<int> succesful_tests;
    public int total_tests;
    List<ITestable<T>> tests;
    public test_collection(){
        succesful_tests = new List<int>();
        total_tests = 0;
        tests = new List<ITestable<T>>();
    }

    public void add(ITestable<T> test) {
        tests.Add(test);
        succesful_tests.Add(0); 
    }

    public void add_assert(
        Func<T,bool> assertion,
        string description="unnamed test"
    ){
        tests.Add(new Assert<T>(assertion, description));
        succesful_tests.Add(0);
    }
    public void add_eq<O>(
        Func<T,O> lhs, Func<T,O> rhs,
        string description="unnamed test"
    ) where O: IEquatable<O> {
        tests.Add(new Equality<T,O>(lhs, rhs, description));
        succesful_tests.Add(0);
    }
    public void try_case(T test_case) {
        for (int i = 0; i<tests.Count; i++) {
            if (tests[i].test(test_case)) succesful_tests[i]++;
        } 
        total_tests++;
    }
    public void fin(){
        Console.Write("Ran testing\n");
        for (int i = 0; i<tests.Count; i++) {
            Console.Write($"\tFor {succesful_tests[i]}/{total_tests} {tests[i].describe()}\n");
        }
    }
}

public interface ITestable<T> {
    string describe();
    bool test(T test_case);
}

class Assert<T>: ITestable<T> {
    public Func<T,bool> assertion;
    public String description;

    public Assert() {
        assertion = (T testcase) => true;
        description = "Empty test";
    }
    
    public Assert(Func<T,bool> new_assertion, String new_description) {
        assertion = new_assertion;
        description = new_description;
    }
    
    public bool test(T testcase) {
        if (!assertion(testcase)) {
            Console.Write($"Assertion that {description} failed for case: \n{testcase}\n\n");
            return false;
        } else {
            return true;
        }
    }
    public String describe() {
        return description;
    }

}


class Equality<T,O>: ITestable<T> 
    where O: IEquatable<O> {
    public Func<T,bool> assertion;
    public String description;
    Func<T,O> lhs;
    Func<T,O> rhs;

    public Equality(Func<T,O> lhs_i, Func<T,O> rhs_i, String new_description) {
        lhs = lhs_i;
        rhs = rhs_i;
        assertion = (T test_case) => lhs(test_case).Equals(rhs(test_case));
        description = new_description;
    }
    
    new public bool test(T testcase) {
        if (!assertion(testcase)) {
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
