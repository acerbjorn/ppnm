use std::f64::consts::PI;

const ERF_A: [f64; 5] = [
    0.254829592,
    -0.284496736,
    1.421413741,
    -1.453152027,
    1.061405429,
];

pub fn erf(x: f64) -> f64 {
    if x < (0.into()) {
        -erf(-x)
    } else {
        let t = 1. / (1. + 0.3275911 * x);
        let sum = t * (ERF_A[0] + t * (ERF_A[1] + t * (ERF_A[2] + t * (ERF_A[3] + t * ERF_A[4])))); /* the right thing */
        1. - sum * (-x * x).exp()
    }
}

pub fn fgamma(x: f64) -> f64 {
    // single precision gamma function (Gergo Nemes, from Wikipedia)
    if x < 0. {
        PI / (PI * x).sin() / fgamma(1. - x)
    } else if x < 9. {
        fgamma(x + 1.) / x
    } else {
        let lngamma = x * (x + 1. / (12. * x - 1. / x / 10.)).ln() - x + (2. * PI / x).ln() / 2.;
        lngamma.exp()
    }
}
