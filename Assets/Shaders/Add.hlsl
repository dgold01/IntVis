

void Add_float (float4 v1, float4 v2, float4 v3, float4 v4, float4 v5, float4 v6, float4 v7, float4 v8, float4 v9, float4 Col , out float4 RGB)
{	
	
	float4 hr = v1*1 + v2*0 + v3*-1 + v4*2 + v5*0 + v6*-2 + v7*1 + v8*0 + v9*-1;
	float4 vt = v1 * 1 + v2 * 2 + v3 * 1 + v4 * 0 + v5 * 0 + v6 * 0 + v7 * -1 + v8 * -2 + v9 * -1;
	hr = sqrt((hr.r * hr.r) + (hr.g * hr.g) + (hr.b * hr.b));
	vt = sqrt((vt.r * vt.r) + (vt.g * vt.g) + (vt.b * vt.b));
	RGB = sqrt(hr * hr + vt * vt) * Col;
}


