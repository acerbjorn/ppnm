using static System.Console;

public class test_collection{
    public int failed_tests;
    public test_collection(){
        failed_tests=0;
    }

    public void assert(
        bool assertion,
        string description="unnamed test"
    ){
        Write($"Testing {description}\n");
        if(assertion){
            Write("\t  Test passed\n");
        } else {
            this.failed_tests += 1;
            Write("\t! TEST FAILED !\n");
        };
    }
    public void assert_eq<T>(
        T lhs, T rhs,
        string description="unnamed test"
    ) where T: System.IEquatable<T> {
        Write($"Testing {description}\n");
        if(lhs.Equals(rhs)){
            Write("\t  Test passed\n");
        } else {
            this.failed_tests += 1;
            Write("\t! TEST FAILED !\n\t lhs: {lhs}\n\t rhs: {rhs}\n");
        };
    }
    public int fin(){
        if(this.failed_tests==0){
            Write("\nAll tests passed.");
        } else {
            Write("\nSome tests failed.");
        };
        return this.failed_tests;
    }
}
