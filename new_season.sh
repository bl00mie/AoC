#!/bin/bash

set -e

if [ $# -eq 0 ]; then
    echo "Usage: new_season.sh year"
    exit -1;
fi
year=$1

if [ ! -d "$year" ]; then
    mkdir $year
fi

cd $year
for day in {1..24}
do
    dotnet new aoc -o $day --day $day --year $year
    dotnet restore $day
done
