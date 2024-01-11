#!/usr/bin/env bash

# Helper for creating Window, UserControl, Style list and Resource dictionaries
# AvaloniaHelper.sh <type> <name>

PROJECT_NAME="App"

# Exit if no arguments
if [ $# -eq 0 ]; then
    echo "No arguments provided"
    exit 1
fi

# Enter project directory
cd $PROJECT_NAME

if [ $1 = "window" ]; then
    # Create Window
    echo "Creating Window $2"
    dotnet new avalonia.window -n $2
elif [ $1 = "control" ]; then
    # Create UserControl
    echo "Creating UserControl $2"
    dotnet new avalonia.usercontrol -n $2
elif [ $1 = "style" ]; then
    # Create Style
    echo "Creating Styles list $2"
    dotnet new avalonia.styles -n $2
elif [ $1 = "resource" ]; then
    # Create ResourceDictionary
    echo "Creating ResourceDictionary $2"
    dotnet new avalonia.resource -n $2
else
    echo "Invalid argument $1"
    exit 1
fi

# Exit project directory
cd ..

# Exit with success
exit 0