# https://tech.davis-hansson.com/p/make/
SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules

ifeq ($(origin .RECIPEPREFIX), undefined)
  $(error This Make does not support .RECIPEPREFIX. Please use GNU Make 4.0 or later)
endif
.RECIPEPREFIX = >

YEAR := 2016
APP := aoc$(YEAR)
INPUTPATH := ../inputs/
COOKIEFILE := ../session_cookie
SESSIONCOOKIE :=$(file < $(COOKIEFILE))

CARGOFLAGS ?=

# generate release build
all: build
build: target/release/$(APP)

# clean build output
clean: .cargoinstalled
> cargo clean

# update the readme
readme: README.md

.PHONY: all build clean readme

### build targets

target/release/%: .cargoinstalled Cargo.toml Cargo.lock src/lib.rs $(shell find src/bin/$* -type f)
> RUSTFLAGS="-C link-arg=-s -C opt-level=3 -C target-cpu=native --emit=asm" cargo build $(CARGOFLAGS) --bin $* --release

%_bench.md: target/release/%
> env AOC_NO_OUTPUT=1 hyperfine --export-markdown $@ --warmup 10 --runs 50 --parameter-scan day 1 25 --command-name 'day {day}' './$< {day}'

README.md: README.tpl.md aoc2016_bench.md
> m4 $< > $@
> markdown-table-formatter $@

.cargoinstalled:
> @if ! command -v cargo 2> /dev/null
> @then
>   @echo "Cargo is not installed. Please visit 'https://rustup.rs/' and follow their instructions, or try to run 'curl --proto \"=https\" --tlsv1.2 -sSf https://sh.rustup.rs | sh'"
>   @exit 1
> @fi
> touch .cargoinstalled

# Download inputs

i%:
> curl --cookie "session=$(SESSIONCOOKIE)" "https://adventofcode.com/$(YEAR)/day/$*/input" > $(INPUTPATH)$(YEAR)_$*
> bat $(INPUTPATH)$(YEAR)_$*

# Generate source file
d%:
> C:\GnuWin\bin\m4.EXE -D DAY=$(YEAR)_$* day.rs.tpl > src/$(APP)/day$*.rs
> code src/$(APP)/day$*.rs
