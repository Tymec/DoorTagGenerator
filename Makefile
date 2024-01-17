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

publish: build linux windows

demo:
	@cd Neumorphism.Avalonia/Neumorphism.Avalonia.Demo && dotnet run

dev:
	@dotnet run --project App -r win-x64 -c Debug

.PHONY: clean build linux windows publish dev
