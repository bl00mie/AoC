#![feature(
    array_chunks,
    array_windows,
    bool_to_option,
    control_flow_enum,
    drain_filter,
    iter_partition_in_place,
    let_else,
    mixed_integer_ops,
    test
)]
#![warn(
    clippy::all,
    clippy::pedantic,
    clippy::nursery,
    clippy::cargo,
    rust_2018_idioms
)]
#![allow(
    clippy::cast_possible_truncation,
    clippy::cast_possible_wrap,
    clippy::cast_precision_loss,
    clippy::cast_sign_loss,
    clippy::missing_const_for_fn,
    clippy::redundant_pub_crate,
    unused_variables
)]

#[macro_use]
extern crate aoc;
#[allow(unused_extern_crates)]
extern crate test;

aoc_main!(
    1 => day01,
);
