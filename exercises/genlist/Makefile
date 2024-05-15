CODE = $(filter %.cs, $^) # Expanded when called. Can use := to expand immediately
LIBS = $(addprefix -reference:, $(filter %.dll, $^))
EXES = $(filter %.exe, $^)
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

.PHONY: default
default: Out.txt

Out.txt: main.exe In.txt
	cat In.txt | mono $< > $@
		
main.exe: main.cs genlist.dll; $(MKEXE)

genlist.dll: genlist.cs; $(MKLIB)

.PHONY: clean
clean: 
	rm Out*.txt main.exe
