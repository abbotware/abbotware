(below works in typora, but not github)

### Expected Value - 𝜇

$$
\mathbb{E}(X) = E[X] = E(X) = \mu
$$



#### Discrete Random Variables

$$
\mathbb{E}[X] =   \sum_{i=1}^n x_i \mathbb{P}(x_i)
\\
\\ x_i \text{ is the i-th value of random variable }X
\\ \mathbb{P}(x_i) \text{ is the probabilty } X \text{ will have value }x_i
$$
#### Continuous Random Variables

$$
\mathbb{E}[X] =  \int_{-\infty}^{\infty} x f_X(x)dx
\\
\\
f_X = \text{probability density function}
$$


### Variance - 𝜎 **²**

$$
\mathbb{V}(X) = Var(X) = V(X) = \sigma^2
$$


$$
\begin{align*}
Var(X) &= E[(X - E[X])^2] \qquad  \tag*{note: $E[(X - \mu)^2]$}
\\
&= E\color{red}[\color{black}X^2 -2XE[X +E[X]^2\color{red}] \tag{$a -b^2$ = $a^2 + 2ab + b^2$}
\\
&= E\color{red}[\color{black}X^2\color{red}]\color{black} -E\color{red}[\color{black}2XE[X]\color{red}]\color{black} +E\color{red}[\color{black}E[X]^2\color{red}] \tag*{linearity of E[]}

\\
&= E[X^2] -2E\color{red}[\color{black}XE[X]\color{red}]\color{black} +E[X]^2  \tag*{$E[E[X]] = E[X]$}
\\
&= E[X^2] -2E[X]E[E[X]] +E[X]^2
\\
&= E[X^2] -2E[X]E[X] +E[X]^2
\\
&= E[X^2] -2E[X]^2 +E[X]^2
\\
&= E[X^2] -E[X]^2
\\
&= E[X^2] -\mu^2

\end{align*}
$$



alternate proof:
[19.3: Properties of Variance - Engineering LibreTexts](https://eng.libretexts.org/Bookshelves/Computer_Science/Programming_and_Computation_Fundamentals/Mathematics_for_Computer_Science_(Lehman_Leighton_and_Meyer)/04%3A_Probability/19%3A_Deviation_from_the_Mean/19.03%3A_Properties_of_Variance)



$$

$$


(below works in github, but not typora)

**The Cauchy-Schwarz Inequality**
$$\left( \sum_{k=1}^n a_k b_k \right)^2 \leq \left( \sum_{k=1}^n a_k^2 \right) \left( \sum_{k=1}^n b_k^2 \right)$$
