CODE = $(filter %.cs, $^) # Expanded when called. Can use := to expand immediately
LIBS = $(addprefix -reference:, $(filter %.dll, $^))
EXES = $(filter %.exe, $^)
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)
TIME = time --output=$@ --append --format "$$timed_var\t%e\t%U"
# I wanted to include the program output in some of the time outputs and this snippet does that.
TIMEWOUT = { time --format "$$timed_var\t%e\t%U" mono $< -nthreads:4 -nterms:$$timed_var; } 2>&1 | { read line2; read line1; echo -e "$$line1\t$$line2"; } >> $@
TIME_METHOD = { time --format "$$timed_var\t%e\t%U" mono $< -nthreads:4 -nterms:2e9 -method:$$timed_var; } 2>&1 | { read line2; read line1; echo -e "$$line1\t$$line2"; } >> $@

.PHONY: default
default: Out.cores.txt Out.txt Out.terms.main.txt Out.method.txt

Out.cores.txt: main.exe Makefile
	which time
	rm -f $@
	echo "#cores	real	user" > $@
	for timed_var in 1 2 3 4; do\
		$(TIME) mono main.exe -nthreads:$$timed_var -nterms:2e9;\
	done
		
Out.terms.%.txt: %.exe Makefile
	rm -f $@
	echo "#terms	real	user	stdout" > $@
	for timed_var in 1e8 2e8 3e8 4e8 5e8 6e8 7e8 8e8 9e8  ; do\
		$(TIMEWOUT);\
	done

Out.method.txt: main.exe Makefile
	rm -f $@
	echo "#terms	real	user	stdout" > $@
	for timed_var in "thread" "parfor" "linq"; do\
		$(TIME_METHOD);\
	done
		
Out.txt: main.exe
	mono $< -threads:4 -iterations:1000 -v >$@

%.exe: %.cs; $(MKEXE)

.PHONY: clean
clean: 
	rm Out*.txt main.exe
