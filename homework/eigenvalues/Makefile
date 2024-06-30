TIMES = $(filter time.%.txt, $^)
GPVARS = "datafiles = '$(TIMES)'; n_files = $(words $(TIMES))"

.PHONEY: default
default: Out.txt wavefunction.svg converge_dr.svg converge_r_max.svg
#	mono hydrogen_atom.exe -w

include ../preamble.mk

scenic: default
	imv *.svg	

wavefunction.svg: wavefunction.gp wavefunction.txt
	gnuplot wavefunction.gp

wavefunction.txt: hydrogen_atom.exe Makefile
	mono $< -w -n:3 -r_max:30 | column -t > $@

converge_dr.svg: converge_dr.txt convergence.gp
	gnuplot -e "in_file='$<'; out_file='$@'; xvar='dr'" convergence.gp

converge_r_max.svg: converge_r_max.txt convergence.gp
	gnuplot -e "in_file='$<'; out_file='$@'; xvar='r_max'" convergence.gp

converge_dr.txt: hydrogen_atom.exe
	rm -f $@
	for dr in 0.05 0.10 0.15 0.20 0.25 0.30 0.35 0.40 0.45 0.50 0.55 0.60 0.65 0.70 0.75 0.80 0.85 0.90 0.95 1.00 1.05 1.10 1.15 1.20 1.25 1.30 1.35 1.40 1.45 1.50 1.55 1.60 1.65 1.70 1.75 1.80 1.85 1.90 1.95 2.00  ; do\
		 printf "$$dr\t%s\n" $$(mono $< -dr:$$dr) >> $@;\
	done
	
converge_r_max.txt: hydrogen_atom.exe
	rm -f $@
	for r_max in 3.00 3.25 3.50 3.75 4.00 4.25 4.50 4.75 5.00 5.25 5.50 5.75 6.00 6.25 6.50 6.75 7.00 7.25 7.50 7.75 8.00 8.25 8.50 8.75 9.00 9.25 9.50 9.75 10.00 10.25 10.50 10.75 11.00 11.25 11.50 11.75 12.00 12.25 12.50 12.75 13.00 13.25 13.50 13.75 14.00 14.25 14.50 14.75 15.00   ; do\
		 printf "$$r_max\t%s\n" $$(mono $< -r_max:$$r_max) >> $@;\
	done
	
Out.txt: test.exe
	mono test.exe -tests:1000 -max_dim:10 > $@

test.exe : test.cs evd.dll matrix.dll hamiltonian.dll; $(MKEXE)
	
hydrogen_atom.exe : hydrogen_atom.cs evd.dll matrix.dll hamiltonian.dll; $(MKEXE)

evd.dll: evd.cs matrix.dll;	$(MKLIB)

hamiltonian.dll: hamiltonian.cs matrix.dll;	$(MKLIB)

.PHONEY: clean
clean: 
	$(RM) matrix.dll hamiltonian.dll evd.dll hydrogen_atom.exe test.exe Out.txt converge_dr.svg converge_r_max.svg wavefunction.txt wavefunction.svg
