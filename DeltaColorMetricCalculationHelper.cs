namespace Hcb.Rnd.Pwn.Application.Common.Helpers;
public static class DeltaColorMetricCalculationHelper
{
    /// <summary>
    /// ∆E: Farbdifferenz des Begleitgewebes (vor und nach Farbechtheitsprüfung)
    /// </summary>
    /// <param name="L_ws"></param>
    /// <param name="a_ws"></param>
    /// <param name="b_ws"></param>
    /// <param name="L_uf"></param>
    /// <param name="a_uf"></param>
    /// <param name="b_uf"></param>
    /// <returns></returns>
    public static double CalculateDeltaE(double L_ws, double a_ws, double b_ws, double L_uf, double a_uf, double b_uf)
    {
        var result = 0.0;

        var d_L = L_uf - L_ws;
        var d_a = a_uf - a_ws;
        var d_b = b_uf - b_ws;

        var d_E = Math.Sqrt(Math.Pow(d_L, 2) + Math.Pow(d_a, 2) + Math.Pow(d_b, 2));

        result = Math.Round(d_E, 2);

        return result;
    }

    /// <summary>
    /// EN ISO 105-A04: 1999
    /// ∆E: Farbdifferenz des Begleitgewebes (vor und nach Farbechtheitsprüfung)
    /// ∆L: Helligkeitsdifferenz des Begleitgewebes (vor und nach Farbechtheitsprüfung)
    /// </summary>
    /// <param name="dE"></param>
    /// <param name="L"></param>
    /// <param name="iniAvgL"></param>
    /// <returns></returns>
    public static double CalculatedE_GS(double dE, double L, double iniAvgL)
    {
        var result = 0.0;
        var d_L = L - iniAvgL;

        var underSqrt = Math.Pow(dE, 2) - Math.Pow(d_L, 2);
        if (underSqrt <= 0)
            underSqrt = 0.01;

        result = dE - 0.4 * Math.Sqrt(underSqrt);
        return result;
    }

    public static double CalculateDeltaHab(double gscItemInihab, double gscItemHab, double gscItemDE, double gscItemDL, double gscItemDCab)
    {
        var m = gscItemHab - gscItemInihab;

        var p = m >= 0 ? 1.0 : -1.0;
        var q = Math.Abs(m) <= 180 ? 1.0 : -1.0;

        //UniMaus Calculation Logic
        var underSqrt = Math.Pow(gscItemDE, 2) - Math.Pow(gscItemDL, 2) - Math.Pow(gscItemDCab, 2);

        return underSqrt > 0 ? p * q * Math.Sqrt(underSqrt) : 0.1;
    }

    /// <summary>
    /// EN ISO 105-A05: 1997 => section 8
    /// </summary>
    /// <param name="deltaEf"></param>
    /// <returns></returns>
    public static double CalculateGsFinalGradeByFormula(double deltaEf)
    {
        var gsFinalGrade = 0.0;

        if (deltaEf <= 3.4)
        {
            gsFinalGrade = 5.0 - deltaEf / 1.7;
        }
        else
        {
            gsFinalGrade = 5.0 - Math.Log10(deltaEf / 0.85) / Math.Log10(2);
        }

        var result = gsFinalGrade < 1.0 ? 1.0 : gsFinalGrade;
        return Math.Round(result, 1);
    }

    /// <summary>
    /// EN ISO 105-A05: 1997 => section 7.2 
    /// </summary>
    /// <param name="hm"></param>
    /// <returns></returns>
    public static double CalculateGSC_x(double hm)
    {
        var x = 0.0;

        if (Math.Abs(hm - 280.0) <= 180.0)
            x = Math.Pow((hm - 280.0) / 30, 2);
        else if (Math.Abs(hm - 280.0) > 180.0)
            x = Math.Pow((360.0 - Math.Abs(hm - 280.0)) / 30.0, 2);
        return x;

    }

    /// <summary>
    /// EN ISO 105-A05: 1997 => section 7.2 
    /// </summary>
    /// <param name="habCur"></param>
    /// <param name="habIni"></param>
    /// <returns></returns>
    public static double CalculatehM(double habCur, double habIni)
    {
        var hm = 0.0;
        //(10)
        if (Math.Abs(habCur - habIni) <= 180.0)
            hm = (habCur + habIni) / 2.0;
        //(11)
        else if (Math.Abs(habCur - habIni) > 180.0 && Math.Abs(habCur + habIni) < 360.0)
            hm = (habCur + habIni) / 2.0 + 180.0;
        //(12)
        else if (Math.Abs(habCur - habIni) > 180.0 && Math.Abs(habCur + habIni) >= 360.0)
            hm = (habCur + habIni) / 2.0 - 180.0;

        return hm;
    }

    /// <summary>
    ///  EN ISO 105-A04: 1999 => section 6.6
    /// </summary>
    /// <param name="internalSsr"></param>
    /// <returns></returns>
    public static double SsrFinalGrade(double internalSsr)
    {
        double ssrFinalGrade = 0.0;

        if (internalSsr.IsWithinMinEqualMaxEqual(4.75, 5.0))
            ssrFinalGrade = 5.0;
        if (internalSsr.IsWithinMinEqualMaxEqual(4.25, 4.74))
            ssrFinalGrade = 4.5;
        if (internalSsr.IsWithinMinEqualMaxEqual(3.75, 4.24))
            ssrFinalGrade = 4.0;
        if (internalSsr.IsWithinMinEqualMaxEqual(3.25, 3.74))
            ssrFinalGrade = 3.5;
        if (internalSsr.IsWithinMinEqualMaxEqual(2.75, 3.24))
            ssrFinalGrade = 3.0;
        if (internalSsr.IsWithinMinEqualMaxEqual(2.25, 2.74))
            ssrFinalGrade = 2.5;
        if (internalSsr.IsWithinMinEqualMaxEqual(1.75, 2.24))
            ssrFinalGrade = 2.0;
        if (internalSsr.IsWithinMinEqualMaxEqual(1.25, 1.74))
            ssrFinalGrade = 1.5;
        if (internalSsr.IsWithinMinEqualMaxEqual(0, 1.24))
            ssrFinalGrade = 1.0;

        return ssrFinalGrade;
    }

    public static double CalculateHT(double a, double b)
    {
        double h = 0;
        if (a < 0)
            h = 360.0 / 2.0 + Math.Atan(b / a) * (360.0 / 2.0) / Math.PI;
        else if (a == 0 && b < 0)
            h = 360.0 / 4.0 * 3.0;
        else if (a == 0 && b == 0)
            h = 0;
        else if (a == 0 && b > 0)
            h = 360.0 / 4.0;
        else if (a > 0 && b < 0)
            h = 360.0 + Math.Atan(b / a) * (360.0 / 2.0) / Math.PI;
        else if (a > 0 && b >= 0)
            h = Math.Atan(b / a) * (360.0 / 2.0) / Math.PI;

        return Math.Round(h, 1);
    }

    public static bool IsWithinMinEqualMaxEqual(this double value, double minimum, double maximum) => value >= minimum && value <= maximum;
}
