use std::collections::HashMap;
use std::collections::HashSet;
use hex_literal::hex;
type Input = String;
type Output = String;

register!(
    "../../../inputs/2016_2";
    (input: input!(parse Input)) -> Output {
        part1(&input);
        part2(&input);
    }
);

fn part1(items: &[Input]) -> Output {
    let mut button = 5u32;
    let mut code = 0u32;
    for cmd in items {
        code *= 10;
        for c in cmd.chars() {
            button = if c == 'U' && button > 3 {
                button - 3
            } else if c == 'D' && button < 7 {
                button + 3
            } else if c == 'L' && button % 3 != 1 {
                button - 1
            } else if c == 'R' && button % 3 != 0 {
                button + 1
            }
            else {
                button
            };
        }
        code += button;
    }
    code.to_string()
}

fn part2(items: &[Input]) -> Output {
    let mut moves = HashMap::new();
    moves.insert('R', (HashSet::from(hex!("02 03 05 06 07 08 0A 0B")), 1i32));
    moves.insert('L', (HashSet::from(hex!("03 04 06 07 08 09 0B 0C")), -1));
    moves.insert('U', (HashSet::from(hex!("03 06 07 08 0A 0B 0C 0D")), -2));
    moves.insert('D', (HashSet::from(hex!("01 02 03 04 06 07 08 0B")), 2));
    let mut button = 5i32;
    let mut code = 0i32;
    for cmd in items {
        code *= 16;
        for c in cmd.chars() {
            let (can_move, delta) = &moves[&c];
            if !can_move.contains(&(button as u8)) {
                continue;
            }
            if (c == 'U' && button % 10 != 3) || (c == 'D' && button % 10 != 1) {
                button += delta;
            }
            button += delta;
        }
        code += button;
    }
    return format!("{:X}", code);
}

#[cfg(test)]
mod tests {
    use super::*;
    use aoc::{Solution, SolutionExt};
    use test::Bencher;

    #[test]
    fn test_ex() {
        let input = r#"ULL
RRDDD
LURDL
UUUUD
"#;
        let (res1, res2) = Solver::run_on(input);
        assert_eq!(res1, "1985");
        assert_eq!(res2, "5DB3");
    }

    #[test]
    fn test() {
        let (res1, res2) = Solver::run_on_input();
        assert_eq!(res1, "61529");
        assert_eq!(res2, "C2C28");
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
