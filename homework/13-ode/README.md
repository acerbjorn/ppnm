# Ordinary Differential Equations
Here's my solution to the ODE homeworks. The main runge-kutta driver can be found in rk.cs, together with a linear euler step driver i've used for debugging unstable problems. The figures test_sinus_graph.svg and test_damped_graph.svg are simple test cases for the Runge Kutta 12 method, and they behave as expected.

![Solution to the differential equation u''=u](test_sinus_graph.svg)
![Solution to a damped spring problem](test_damped_graph.svg)

# Interpolant interface and relativistic orbit
The interpolant interface also works as expected. The relativistic equatorial motion is plotted in test_eq_graph.svg and shows the expected precession in the relativistic case.
![Solution to the equatorial motion of a planet in GR ](test_eq_graph.svg)

# Stable three body motion
Can be seen in test_figure_eight_graph.svg. I've moved two of the bodies up and down in the y direction, otherwise the motion of one would obscure the motion of the other completely. 


![Stable three body motion under gravity](test_figure_eight_graph.svg)
