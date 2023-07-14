.PHONY: scale-generator.exe

all: scale-generator.exe

scale-generator.exe: src/*.cs
	mcs -out:$@ -pkg:dotnet $^
	
clear:
	rm bin/*
