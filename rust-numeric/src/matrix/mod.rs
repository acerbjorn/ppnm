use std::ops::{Add, Div, Index, IndexMut, Mul, Sub};

pub trait Field: PartialEq + Add + Sub + Mul + Div + Sized {}
impl<T> Field for T where T: PartialEq + Add + Sub + Mul + Div + Sized {}

pub struct Matrix<T: Field, const N: usize, const M: usize> {
    pub entries: [[T; M]; N],
}

impl<T: Field, const N: usize, const M: usize> Matrix<T, N, M> {
    pub fn width(&self) -> usize {
        M
    }
    pub fn height(&self) -> usize {
        N
    }
    pub fn from_fn(fn:dyn Fn(usize,usize) -> T, height: usize, width: usize) {
        let mut entries = [[T; height]; width]
    }
}

impl<T: Field, const N: usize, const M: usize> Index<(usize, usize)> for Matrix<T, N, M> {
    type Output = T;
    fn index(&self, index: (usize, usize)) -> &Self::Output {
        &self.entries[index.0][index.1]
    }
}

impl<T: Field, const N: usize, const M: usize> IndexMut<(usize, usize)> for Matrix<T, N, M> {
    fn index_mut(&mut self, index: (usize, usize)) -> &mut Self::Output {
        &mut self.entries[index.0][index.1]
    }
}

impl<T: Field, const N: usize, const M: usize, const O: usize> Mul for Matrix<T, N, M> {
    type Output = Matrix<T, N, O>;
    fn mul(self, rhs: &Matrix<T, M, O>) -> Self::Output {}
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn matrix_creation() {
        let m = Matrix {
            entries: [[0; 4]; 4],
        };
        assert_eq!(m.width(), 4);
        assert_eq!(m.height(), 4);
        assert_eq!(m[(0, 0)], 0);
    }
}
