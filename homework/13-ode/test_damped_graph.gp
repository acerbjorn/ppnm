set terminal svg background "white"
set out "test_damped_graph.svg"

plot \
	"test_damped_out.txt" with lines
