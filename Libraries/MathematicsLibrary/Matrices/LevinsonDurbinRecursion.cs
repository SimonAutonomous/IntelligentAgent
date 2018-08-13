using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathematicsLibrary.Vectors;

namespace MathematicsLibrary.Matrices
{
    public class LevinsonDurbinRecursion
    {
        #region Comments
        // The method carries out Levinson-Durbin recursion to solve
        // the equation r = Ra for a, where r consists of the n autocorrelations
        // r_ss(1), ... r_ss(n), and R is a Toeplitz matrix formed as
        //
        // | r_ss(0)    r_ss(1)    ...   r_ss(n-1) |
        // | r_ss(1)    r_ss(0)    ...   r_ss(n-2) |
        // |  ...        ...       ...    ...      |
        // | r_ss(n-1)  r_ss(n-2)  ...   r_ss(0)   |
        // 
        // The input vector (the correlations vector) has n+1 elements, and is supposed to
        // contain r_ss(0), r_ss(1), ... , r_ss(n).
        //
        // Reference: Jones, D.L. et al
        // "Speech Processing: Theory of LPC Analysis and Synthesis",
        // Connexions. June, 1 (2009).
        #endregion
        public static Vector Solve(Vector correlations)
        {
            // First generate the vector r and the Matrix R:
            int n = correlations.Elements.Count - 1;
            Vector r = new Vector();
            for (int ii = 0; ii < n; ii++)
            {
                r.Elements.Add(correlations.Elements[ii + 1]);
            }
            Matrix R = new Matrix();
            List<double> row = new List<double>();
            for (int ii = 0; ii < n; ii++)
            {
                row.Add(correlations.Elements[ii]);
            }
            R.Elements.Add(row);
            for (int ii = 1; ii < n; ii++)
            {
                row = new List<double>();
                int index = ii;
                Boolean increasing = false;
                for (int jj = 0; jj < n; jj++)
                {
                    if (increasing)
                    {
                        row.Add(correlations.Elements[index]);
                        index++;
                    }
                    else
                    {
                        row.Add(correlations.Elements[index]);
                        index--;
                        if (index < 0)
                        {
                            index = 1;
                            increasing = true;
                        }
                    }
                }
                R.Elements.Add(row);
            }
            // Now apply the Levison-Durbin recursion:
            double E = R.Elements[0][0];
            double k;
            Vector a = new Vector(n);
            Vector aPrevious;
            for (int i = 1; i <= n; i++)
            {
                // Step 1: Find k_i 
                k = correlations.Elements[i];
                for (int j = 0; j < (i - 1); j++)
                {
                    int index = (int)Math.Abs(i - 1 - j);
                    k -= a.Elements[j] * correlations.Elements[index];
                }
                k /= E;
                // Step 2: compute new a:
                aPrevious = a.Copy();
                a.Clear();
                for (int j = 0; j < i - 1; j++)
                {
                    a.Elements[j] = aPrevious.Elements[j] - k * aPrevious.Elements[i - 2 - j];
                }
                // Step 3: Compute a(i)
                a.Elements[i - 1] = k;
                // Step 4: Compute E_i
                E = (1 - k * k) * E;
            }
            return a;
        }
    }
}
