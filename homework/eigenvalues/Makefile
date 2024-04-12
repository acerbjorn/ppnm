TIMES = $(filter time.%.txt, $^)
GPVARS = "datafiles = '$(TIMES)'; n_files = $(words $(TIMES))"

.PHONEY: default
default: hydrogen_atom.exe Out.txt
	mono hydrogen_atom.exe -w

include ../preamble.mk

# time.svg: time.gp time.decomp.txt time.abs_det.txt time.solve.txt time.inverse.txt 

# 	gnuplot -e $(GPVARS) $<

scenic: default
	imv *.svg	

wavefunction.txt: hydrogen_atom.exe
	mono $< -w > $@

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
