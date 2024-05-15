using static test_collection<int>;

class TestTest {
    static void Main() {
        test_collection<int> test_tests = new test_collection<int>();
        test_tests.add_assert(
            (int x) => x==x
        );
        test_tests.add_eq(
            (int x) => x+(x-1),
            (int x) => (x-1)+x
        );
        test_tests.add_assert(
            (int x) => false,
            "Should fail"
        );
        test_tests.add_eq(
            (int x) => x+1,
            (int x) => x-1,
            "x-1=x+1"
        );
        for (int i=0; i<10; i++) {
            test_tests.try_case(i);
        }
        test_tests.fin();
    }
}
