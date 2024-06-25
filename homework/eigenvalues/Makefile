TIMES = $(filter time.%.txt, $^)
GPVARS = "datafiles = '$(TIMES)'; n_files = $(words $(TIMES))"
GPIO = "input_file = $<; output_file = $@"

.PHONEY: default
default: Out.txt wavefunction.svg
#	mono hydrogen_atom.exe -w

include ../preamble.mk

scenic: default
	imv *.svg	

wavefunction.svg: wavefunction.gp wavefunction.txt
	gnuplot wavefunction.gp

wavefunction.txt: hydrogen_atom.exe Makefile
	mono $< -w -n:3 -r_max:30 | column -t > $@

converge_dr.svg: converge_dr.txt convergence.gp
	gnuplot -e $(GPIO) convergence.gp

converge_dr.svg: converge_dr.txt convergence.gp
	gnuplot -e $(GPIO) convergence.gp

Out.txt: test.exe
	mono test.exe -tests:1000 -max_dim:10 > $@

time.%.txt: time.exe
	for timed_var in $$(seq 5 5 400); do\
		$(TIME) mono time.exe $* $$timed_var;\
	done

test.exe : test.cs evd.dll matrix.dll hamiltonian.dll; $(MKEXE)
	
hydrogen_atom.exe : hydrogen_atom.cs evd.dll matrix.dll hamiltonian.dll; $(MKEXE)

evd.dll: evd.cs matrix.dll;	$(MKLIB)

hamiltonian.dll: hamiltonian.cs matrix.dll;	$(MKLIB)

.PHONEY: clean
clean: 
	rm matrix.dll Out.txt time.*.txt log.gpi time.svg
