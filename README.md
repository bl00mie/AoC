# [Advent of Code](http://adventofcode.com) solutions in C#
My first experiences with C# came long ago in the early 00s, but I've only started using it seriously in the last few months. I have done a handful of the AoC challenges thru the years in various languages, but in an attempt to get more comfortable with some of the exciting C# features, I'm going to try to solve all of the previous years challenges and store the results here. In the first few weeks I've already started cobbling a set of utilites and tools to make doing these challenges a bit easier.

# .Net Core AoC Project Template
I got tired of performing the same boring work to prep a new C# project for the next day's contest, so I took the time to learn how to make a dotnet project template. The template(s) I've created are stored in this repository in the dotnetTemplates folder. My base AoC project template creates a new project with some common imports and a little bit of hepler-class infrastructure.

To install this template for your own use, simply add it to your `dotnet new` list
```
dotnet new -i /path/to/dotnetTemplates/working/templates/aoc
```

To remove the template, just uninstall with a very similar path. Note: for uninstall, the target path must be absolute, not relative
```
dotnet new -u /path/to/dotnetTemplates/working/templates/aoc
```
