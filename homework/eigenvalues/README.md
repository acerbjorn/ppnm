# 10 Eigenvalues
This folder contains my implementation with eigenvalue decomposition in the evd.cs as well as tests and timing of these implementations.

## Jacobi diagonalization with cyclic sweeps
The tests are run by test.cs and the output is piped to [Out.txt].

## Hydrogen atom, s-wave radial Schrödinger equation on a grid
In this part we're interested in numerically solving the Schrödinger equation for the s-wave in the hydrogen atom. 
In units "Bohr radius" for length and "Hartree" for energy the Schrödinger equation reads,

$$\frac{-1}{2}f'' \frac{-1}{r}f = \epsilon f ,\\$$

where f(r) is the reduced radial wave-function, $f(r)=r\psi(r)$; $\epsilon$ is the energy; and primes denote the derivative over r.

We now investigate the boundary conditions of the s-waves. Reducing the general hydrogen wavefunction to $l=0,\, m=0$, an s-wave of level $n$ can be written in terms of a constant $A_n$, an exponential part, and a Laguerre potential $L_{n-1}$.

$$
f(r) = rA_n e^{\frac{-r}{na_0^{*}}}L_{n-1}(r)$$

$$
f(r \to 0) \to r\cdot A_n L_{n-1}(0)
$$

Since $L_{n-1}$ is a polynomial it follows that it does not diverge at $r=0$ so,

$$
f(r \to 0) \to 0.
$$

At $r\to\infty$ the exponential part dominates so,

$$f(r\to\infty) \to 0 .$$


