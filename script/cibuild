#!/bin/bash

MDTOOL="/Applications/Xamarin Studio.app/Contents/MacOS/mdtool"

if [ ! -f "$MDTOOL" ]; then
	echo "*** You need to install Xamarin Studio ***"	
	exit 1
fi

scriptdir=`dirname $BASH_SOURCE`

find "$scriptdir/.." -type f -name "*.sln" -exec /usr/bin/env mono .nuget/NuGet.exe restore "{}" \;
find "$scriptdir/.." -type f -name "*.sln" -exec "$MDTOOL" build "$@" "{}" \;
