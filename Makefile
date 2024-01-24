#!/usr/bin/make -f

default: 
	@echo "No default target"

clean:
	@rm -rf build/*
	@dotnet clean /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary

build:
	@dotnet build

linux:
	@dotnet publish App -r linux-x64 -c Release --output build/linux-x64
	@mv build/linux-x64/App build/App
	@rm -rf build/linux-x64

windows:
	@dotnet publish App -r win-x64 -c Release --output build/win-x64
	@mv build/win-x64/App.exe build/App.exe
	@rm -rf build/win-x64

package:
	@tar -czvf build/linux-x64.tar.gz build/App
	@zip -j build/win-x64.zip build/App.exe

publish: build linux windows package

demo:
	@cd Neumorphism.Avalonia/Neumorphism.Avalonia.Demo && dotnet run

dev:
	@dotnet run --project App -r win-x64 -c Debug

.PHONY: clean build linux windows publish package demo dev
