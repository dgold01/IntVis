void GammaCorrection_float(vector A, float gamma, out vector Out)
{	
	Out = pow(A, (1.0 / gamma));
}


