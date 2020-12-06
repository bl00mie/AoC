#!/bin/bash

set -e

if [ $# -eq 0 ]; then
    echo "Usage: new.sh day"
    exit -1;
fi
day=$1
if [ $# -gt 1 ]; then 
    year=$2
else
    year=2020
fi

cd $year
dotnet new aoc -o $day -d $day -y $year
dotnet restore $day
