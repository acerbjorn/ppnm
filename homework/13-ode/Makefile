.PHONEY: default
default: test_sinus_graph.svg 

.NOTINTERMEDIATE: 

include ../preamble.mk

scenic: test_sinus_graph.svg test_damped_graph.svg test_eq_graph.svg
	imv *.svg

test_%_graph.svg: test_%_graph.gp test_%_out.txt 
	gnuplot $< 

test_%_out.txt: test_%.exe
	mono $< >$@

test_%.exe: test_%.cs rk.dll matrix.dll
	$(MKEXE)
	
# test_sinus_graph.svg: test_sinus_graph.gp test_sinus_out.txt 
# 	gnuplot $< 

# test_sinus_out.txt: test_sinus_ode.exe
# 	mono $< >$@

# test_sinus_ode.exe: test_sinus_ode.cs rk.dll matrix.dll
# 	$(MKEXE)

rk.dll: rk.cs matrix.dll
	$(MKLIB)


.PHONEY: clean
clean: 
