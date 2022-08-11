void Sobel_float (float2 uv, out float2 uv1, out float2 uv2, out float2 uv3, out float2 uv4, out float2 uv5, out float2 uv6, out float2 uv7, out float2 uv8, out float2 uv9)
{	
	float2 delta = float2(0.002, 0.002);

	
	uv1 = uv + float2(-1.0, -1.0) * delta;
	uv2 = uv + float2(0.0, -1.0) * delta;
	uv3 = uv + float2(1.0, -1.0) * delta;
	uv4 = uv + float2(-1.0, 0.0) * delta;
	uv5 = uv + float2(0.0, 0.0) * delta;
	uv6 = uv + float2(1.0, 0.0) * delta;
	uv7 = uv + float2(-1.0, 1.0) * delta;
	uv8 = uv + float2(0.0, 1.0) * delta;
	uv9 = uv + float2(1.0, 1.0) * delta;

	
	
}


