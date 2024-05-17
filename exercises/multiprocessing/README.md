# Why are the other methods worse?
Both the other methods use the parallel for construction. This spawns a number of threads, which grab values from the list we iterate over (1..N), calculate the harmonic part for that value, and add it to the sum. The speed is largely an issue with having to go back to retrieve a new value everytime they finish the (quite fast) calculation. However the naive parfor method creates a data race as all the threads attempt to read and write to the same memory. This is fixed by the linq variation, which keeps the sums in the threads and joins them when finished.


