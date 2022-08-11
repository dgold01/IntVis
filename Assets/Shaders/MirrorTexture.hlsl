void MirrorTexture_float(bool mirrorX, bool mirrorY, float2 UV, out float2 mirroredUV)
{	
	
	mirroredUV = float2(lerp(UV.x, 1.0f - UV.x, mirrorX), lerp(UV.y, 1.0f - UV.y, mirrorY));
	
	
}


