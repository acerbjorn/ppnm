CODE = $(filter %.cs, $^) # Expanded when called. Can use := to expand immediately
LIBS = $(addprefix -reference:, $(filter %.dll, $^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

Out.txt: main.exe 
		mono $< > $@

main.exe: main.cs cmath.dll; $(MKEXE)
		
cmath.dll: cmath.cs complex.cs; $(MKLIB)
