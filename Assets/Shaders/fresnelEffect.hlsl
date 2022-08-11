void frenelEffect_float (float3 Normal, float3 ViewDir, float Power, out float Out)
{	
	Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
}


