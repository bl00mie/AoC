use std::collections::HashSet;
type Input = String;

register!(
    "../../../inputs/2016_1";
    (input: input!(Input)) -> u64 {
        part1(&input);
        part2(&input);
    }
);

fn part1(input: &[Input]) -> u64 {
    let delta: [(i64, i64); 4] = [(0,1), (1,0), (0,-1), (-1,0)];
    let mut facing: i8 = 0;
    let mut x = 0i64;
    let mut y = 0i64;
    for cmd in input[0].split(", ") {
        if cmd.chars().nth(0) == Some('R') {
            facing = (facing + 1) % 4;
        } else {
            facing = (facing + 3) % 4;
        }
        let steps: i64 = cmd[1..].parse().unwrap();
        let (dx, dy) = delta[facing as usize];
        x += steps * dx;
        y += steps * dy;
    }
    return (x.abs() + y.abs()) as u64;
}

fn part2(input: &[Input]) -> u64 {
    let delta: [(i64, i64); 4] = [(0,1), (1,0), (0,-1), (-1,0)];
    let mut facing: i8 = 0;
    let mut x = 0i64;
    let mut y = 0i64;
    let mut visited = HashSet::new();
    for cmd in input[0].split(", ") {
        if cmd.chars().nth(0) == Some('R') {
            facing = (facing + 1) % 4;
        } else {
            facing = (facing + 3) % 4;
        }
        let steps: i64 = cmd[1..].parse().unwrap();
        let (dx, dy) = delta[facing as usize];
        for step in 0..steps {
            x += dx;
            y += dy;
            if visited.contains(&(x,y)) {
                return (x.abs() + y.abs()) as u64
            }
            visited.insert((x,y));
        }
    }
    return 0
}

#[cfg(test)]
mod tests {
    use super::*;
    use aoc::SolutionExt;

    #[test]
    fn test_ex() {
        let (res1, res2) = Solver::run_on(
            "R2, L3",
        );
        assert_eq!(res1, 5);
        
        let (res1, res2) = Solver::run_on(
            "R2, R2, R2",
        );
        assert_eq!(res1, 2);

        let (res1, res2) = Solver::run_on(
            "R5, L5, R5, R3",
        );
        assert_eq!(res1, 12);

        let (res1, res2) = Solver::run_on(
            "R8, R4, R4, R8",
        );
        assert_eq!(res2, 4);
    }

    #[test]
    fn test() {
        let (res1, res2) = Solver::run_on_input();
        assert_eq!(res1, 278);
        assert_eq!(res2, 161);
    }
}
