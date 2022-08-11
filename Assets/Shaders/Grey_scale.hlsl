void Grey_scale_float (float R,float G,float B, out float4 RGBA)
{	
	float3 irgb = float3(R, G, B);
	const float3 W = float3(0.2125, 0.7154, 0.0721);
	float luminance = dot(irgb, W);
	RGBA = float4(luminance, luminance, luminance, 1.0);
	
}


