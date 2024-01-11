#!/usr/bin/make -f

default: 
	@echo "No default target"

clean:
	@echo "Cleaning up"
	@rm -rf build/*
	@dotnet clean

linux:
	@echo "Building for Linux"
	@dotnet clean
	@dotnet publish App -r linux-x64 -c Release --output build/linux-x64 -p:PublishAot=true
	@mv build/linux-x64/App build/App
	@rm -rf build/linux-x64

windows:
	@echo "Building for Windows"
	@dotnet clean
	@dotnet publish App -r win-x64 -c Release --output build/win-x64 -p:PublishAot=true
	@mv build/win-x64/App.exe build/App.exe
	@rm -rf build/win-x64

publish: linux windows

dev:
	@echo "Running for Windows"
	@dotnet run --project App -r win-x64 -c Debug

.PHONY: clean linux windows publish dev
