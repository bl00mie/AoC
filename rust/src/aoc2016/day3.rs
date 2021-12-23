use std::{num::ParseIntError, str::FromStr};

register!(
    "../../../inputs/2016_3";
    (input: input!(parse Triangle)) -> u16 {
        part1(&input);
        part2(&input);
    }
);

fn part1(items: &[Triangle]) -> u16 {
    let mut valid = 0u16;
    for t in items.iter() {
        valid += is_valid(t);
    }
    valid
}

fn part2(items: &[Triangle]) -> u16 {
    let mut valid = 0u16;
    match IntoIterator::into_iter(items) {
        mut iter => loop {
            match iter.next() {
                Some(l1) => {
                    let l2 = iter.next().unwrap();
                    let l3 = iter.next().unwrap();
                    valid += is_valid(&Triangle { a: l1.a, b: l2.a, c: l3.a})
                        + is_valid(&Triangle { a: l1.b, b: l2.b, c: l3.b })
                        + is_valid(&Triangle { a: l1.c, b: l2.c, c: l3.c });
                },
                None => break,
            };
        }
    };
    valid
}

#[derive(Debug)]
pub struct Triangle {
    a: u16,
    b: u16,
    c: u16
}

fn is_valid(t: &Triangle) -> u16 {
    return if t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a {
        1
    } else {
        0
    };
}

impl FromStr for Triangle {
    type Err = ParseIntError;

    fn from_str(s: &str) -> Result<Self, Self::Err> {
        let mut legs:[u16;3] = [0; 3];
        let mut i: usize = 0;
        for leg in s.split(' ') {
            if leg == "" {
                continue;
            }
            legs[i] = leg.parse()?;
            i += 1;
        }
        Ok(Self {
            a: legs[0],
            b: legs[1],
            c: legs[2],
        })
    }
}

#[cfg(test)]
mod tests {
    use super::*;
    use aoc::{Solution, SolutionExt};
    use test::Bencher;

    #[test]
    fn test_ex() {
        let input = r#"

        "#;
        let (res1, res2) = Solver::run_on(input);
        assert_eq!(res1, 0);
        assert_eq!(res2, 0);
    }

    #[test]
    fn test() {
        let (res1, res2) = Solver::run_on_input();
        assert_eq!(res1, 0);
        assert_eq!(res2, 0);
    }

    #[bench]
    fn bench_parsing(b: &mut Bencher) {
        let input = Solver::puzzle_input();
        b.bytes = input.len() as u64;
        b.iter(|| Solver::parse_input(input));
    }

    #[bench]
    fn bench_pt1(b: &mut Bencher) {
        let input = Solver::parse_input(Solver::puzzle_input());
        b.iter(|| part1(&input));
    }

    #[bench]
    fn bench_pt2(b: &mut Bencher) {
        let input = Solver::parse_input(Solver::puzzle_input());
        b.iter(|| part2(&input));
    }
}
