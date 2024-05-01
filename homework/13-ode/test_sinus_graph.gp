set terminal svg background "white"
set out "test_sinus_graph.svg"

plot \
	"test_sinus_out.txt" with lines
